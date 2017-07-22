using LibrameCore.Authentication;
using LibrameCore.Authentication.Managers;
using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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


        [LibrameAuthorize]
        public IActionResult Admin()
        {
            return View();
        }


        public IActionResult Login()
        {
            // 由 TokenHandler 实现
            return View();
        }


        public IActionResult Logout()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.Authentication.SignOutAsync(AuthenticationOptions.DEFAULT_SCHEME);
            
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        public IActionResult Register()
        {
            return View(new User());
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            // 编码存储
            user.Passwd = _userManager.PasswordManager.Encode(user.Passwd);

            var userResult = await _userManager.CreateAsync(user);

            if (!userResult.IdentityResult.Succeeded)
            {
                var firstError = userResult.IdentityResult.Errors.FirstOrDefault();

                if (firstError is LibrameIdentityError)
                    ModelState.AddModelError((firstError as LibrameIdentityError).Key, firstError.Description);
                else
                    ModelState.AddModelError(string.Empty, firstError.Description);

                return View(user);
            }

            return RedirectToAction(nameof(AccountController.Login));

            //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
            //        // Send an email with this link
            //        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //        //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            //        //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
            //        //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
            //        await _signInManager.SignInAsync(user, isPersistent: false);
            //        _logger.LogInformation(3, "User created a new account with password.");
            //        return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<bool> ValidateRegister(string field, string value)
        {
            return await _userManager.ValidateUniquenessAsync(field, value);
        }


        public IActionResult Validate(string token)
        {
            ViewBag.Token = token;

            return View();
        }

    }
}
