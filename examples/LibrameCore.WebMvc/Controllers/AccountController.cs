using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LibrameCore.WebMvc.Controllers
{
    using Authentication;
    using Entities;

    public class AccountController : Controller
    {
        private readonly IAuthenticationRepository<Role, User, UserRole, int, int, int> _repository = null;
        private readonly IAuthenticationPolicy _policy = null;


        public AccountController(IAuthenticationRepository<Role, User, UserRole, int, int, int> repository,
            IAuthenticationPolicy policy)
        {
            _repository = repository.NotNull(nameof(repository));
            _policy = policy.NotNull(nameof(policy));
        }


        [LibrameAuthorize(Roles = "Never")]
        public IActionResult TestAccessDenied()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        
        public IActionResult Login(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && !returnUrl.IsRelativeVirtualPath())
            {
                if (!_policy.Options.IsHostRegistered(returnUrl))
                {
                    return new JsonResult(new { message = "未授权的主机请求" });
                }
            }

            if (!User.Identity.IsAuthenticated)
                ViewBag.ReturnUrl = returnUrl;

            // 由 TokenHandler 实现
            return View();
        }


        [LibrameAuthorize]
        public IActionResult Logout(string returnUrl)
        {
            _policy.DeleteCookieToken(HttpContext);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                if (!returnUrl.IsRelativeVirtualPath() && !_policy.Options.IsHostRegistered(returnUrl))
                {
                    return new JsonResult(new { message = "未授权的主机请求" });
                }

                return Redirect(returnUrl);
            }

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
            user.Passwd = _repository.PasswordManager.Encode(user.Passwd);

            var result = await _repository.TryCreateUserAsync(user);
            if (!result.Succeeded)
            {
                var firstError = result.Errors.FirstOrDefault();

                if (firstError is LibrameIdentityError)
                    ModelState.AddModelError((firstError as LibrameIdentityError).Key, firstError.Description);
                else
                    ModelState.AddModelError(string.Empty, firstError.Description);

                return View(user);
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
            return await _repository.ValidateUserUniquenessAsync(field, value);
        }


        [LibrameAuthorize]
        public IActionResult Validate(string token)
        {
            if (string.IsNullOrEmpty(token) && User.Identity.IsAuthenticated)
            {
                var identity = User.AsLibrameIdentity(HttpContext.RequestServices);
                token = _policy.TokenManager.Encode(identity);
            }

            ViewBag.Token = token;

            return View();
        }


        [LibrameAuthorize(Roles = "Administrator")]
        public IActionResult Admin()
        {
            return View();
        }

    }
}
