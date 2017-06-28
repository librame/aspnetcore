using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Authorization;
using LibrameStandard.Authentication;
using LibrameStandard.Authentication.Managers;
using LibrameStandard.Authentication.Models;
using LibrameStandard.Utilities;

namespace LibrameCore.Website.Controllers
{
    using Entities;
    
    public class UserController : Controller
    {
        private readonly IAuthenticationAdapter _adapter = null;
        private readonly IUserManager<UserModel> _userManager = null;


        public UserController(IAuthenticationAdapter adapter)
        {
            _adapter = adapter.NotNull(nameof(adapter));
            _userManager = adapter.GetUserManager<UserModel>();
        }


        public IActionResult Login()
        {
            //var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            //identity.AddClaim(new Claim(ClaimTypes.Name, "userid"));
            //await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            //    new ClaimsPrincipal(identity), new AuthenticationProperties() { IsPersistent = true });

            return View();
        }
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    if (ModelState.IsValid)
        //    {
        //        // This doesn't count login failures towards account lockout
        //        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        //        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        //        if (result.Succeeded)
        //        {
        //            _logger.LogInformation(1, "User logged in.");
        //            return RedirectToLocal(returnUrl);
        //        }
        //        if (result.RequiresTwoFactor)
        //        {
        //            return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //        }
        //        if (result.IsLockedOut)
        //        {
        //            _logger.LogWarning(2, "User account locked out.");
        //            return View("Lockout");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //            return View(model);
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}


        public IActionResult Logout()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> LogOut()
        //{
        //    await _signInManager.SignOutAsync();
        //    _logger.LogInformation(4, "User logged out.");
        //    return RedirectToAction(nameof(HomeController.Index), "Home");
        //}

        
        public IActionResult Register()
        {
            return View(new User());
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var userResult = await _userManager.CreateAsync(model);

                if (userResult.IdentityResult.Succeeded)
                {

                }
            }

            //if (ModelState.IsValid)
            //{
            //    var _userManager = new Microsoft.AspNetCore.Identity.UserManager();
            //    var result = await _userManager.CreateAsync(model);

            //    if (result.Succeeded)
            //    {
            //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
            //        // Send an email with this link
            //        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //        //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            //        //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
            //        //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
            //        await _signInManager.SignInAsync(user, isPersistent: false);
            //        _logger.LogInformation(3, "User created a new account with password.");
            //        return RedirectToAction(nameof(HomeController.Index), "Home");
            //    }

            //    AddErrors(result);
            //}

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<bool> ValidateRegister(string field, string value)
        {
            return await _userManager.ValidateUniquenessAsync(field, value);
        }


        public IActionResult Validate()
        {
            return View();
        }

    }
}
