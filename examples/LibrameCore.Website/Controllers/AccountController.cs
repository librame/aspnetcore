using LibrameCore.Authentication;
using LibrameCore.Authentication.Managers;
using LibrameStandard;
using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace LibrameCore.Website.Controllers
{
    using Entities;

    public class AccountController : Controller
    {
        private readonly IUserManager<User> _userManager = null;


        public AccountController(IUserManager<User> userManager)
        {
            _userManager = userManager.NotNull(nameof(userManager));
        }


        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.Options = HttpContext.RequestServices.GetOptions<AuthenticationOptions>();
            else
                ViewBag.ReturnUrl = returnUrl;

            // 由 TokenHandler 实现
            return View();
        }


        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.Options = HttpContext.RequestServices.GetOptions<AuthenticationOptions>();

            return View(new User());
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(User model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // 编码存储
            model.Passwd = _userManager.PasswordManager.Encode(model.Passwd);

            var user = await _userManager.CreateAsync(model);

            if (!user.identity.Succeeded)
            {
                var firstError = user.identity.Errors.FirstOrDefault();

                if (firstError is LibrameIdentityError)
                    ModelState.AddModelError((firstError as LibrameIdentityError).Key, firstError.Description);
                else
                    ModelState.AddModelError(string.Empty, firstError.Description);

                return View(model);
            }

            //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
            //        // Send an email with this link
            //        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //        //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            //        //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
            //        //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
            //        await _signInManager.SignInAsync(user, isPersistent: false);
            //        _logger.LogInformation(3, "User created a new account with password.");
            //        return RedirectToAction(nameof(HomeController.Index), "Home");

            return RedirectToAction(nameof(AccountController.Login));
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<bool> ValidateRegister(string field, string value)
        {
            return await _userManager.ValidateUniquenessAsync(field, value);
        }


        [LibrameAuthorize]
        public IActionResult Validate(string token)
        {
            if (string.IsNullOrEmpty(token) && User.Identity.IsAuthenticated)
            {
                var identity = User.AsLibrameIdentity(HttpContext.RequestServices);
                var manager = HttpContext.RequestServices.GetService<ITokenManager>();
                token = manager.Encode(identity);
            }

            ViewBag.Token = token;

            return View();
        }


        [LibrameAuthorize(Roles = "Administrator")]
        public IActionResult Admin()
        {
            var options = HttpContext.RequestServices.GetOptions<AuthenticationOptions>();

            return View(options);
        }


        [LibrameAuthorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}
