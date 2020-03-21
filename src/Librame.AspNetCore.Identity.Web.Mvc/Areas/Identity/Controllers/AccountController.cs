#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Controllers
{
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Stores;
    using AspNetCore.Identity.Web.Models;
    using AspNetCore.Identity.Web.Resources;
    using AspNetCore.Web;
    using AspNetCore.Web.Applications;
    using Extensions;
    using Extensions.Core.Services;
    using Extensions.Data.Stores;
    using Extensions.Network.Services;

    /// <summary>
    /// 用户控制器。
    /// </summary>
    [Authorize]
    [GenericApplicationModel]
    [Area(IdentityRouteBuilderExtensions.AreaName)]
    [Route(IdentityRouteBuilderExtensions.Template)]
    public class AccountController<TUser> : ApplicationController
        where TUser : class, IId<string>
    {
        [InjectionService]
        private ILogger<AccountController<TUser>> _logger = null;

        [InjectionService]
        private IEmailService _emailService = null;

        [InjectionService]
        private ISmsService _smsService = null;

        [InjectionService]
        private IOptions<IdentityBuilderOptions> _builderOptions = null;

        [InjectionService]
        private IOptions<IdentityOptions> _options = null;

        [InjectionService]
        private IHtmlLocalizer<RegisterViewResource> _registerLocalizer = null;

        [InjectionService]
        private IHtmlLocalizer<ErrorMessageResource> _errorMessageLocalizer = null;

        [InjectionService]
        private IHtmlLocalizer<SendCodeViewResource> _sendCodeLocalizer = null;

        [InjectionService]
        private IdentityStoreIdentifier _storeIdentifier = null;

        [InjectionService]
        private IUserStore<TUser> _userStore = null;

        [InjectionService]
        private SignInManager<TUser> _signInManager = null;

        [InjectionService]
        private IClockService _clockService = null;

        private readonly UserManager<TUser> _userManager = null;


        /// <summary>
        /// 构造一个 <see cref="AccountController{TUser}"/>。
        /// </summary>
        /// <param name="injectionService">给定的 <see cref="IInjectionService"/>。</param>
        /// <param name="application">给定的 <see cref="IApplicationContext"/>。</param>
        public AccountController(IInjectionService injectionService, IApplicationContext application)
            : base(application)
        {
            injectionService.NotNull(nameof(injectionService)).Inject(this);

            _userManager = _signInManager.UserManager;
        }


        /// <summary>
        /// GET: /Account/Login
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["ExternalSchemes"] = _signInManager.GetExternalAuthenticationSchemesAsync().Result;

            ViewBag.BuilderOptions = _builderOptions.Value;
            ViewBag.Options = _options.Value;
            ViewBag.Localizer = _registerLocalizer;

            return View();
        }

        /// <summary>
        /// POST: /Account/Login
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "model")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            model.NotNull(nameof(model));

            ViewData["ReturnUrl"] = returnUrl;
            ViewData["ExternalSchemes"] = _signInManager.GetExternalAuthenticationSchemesAsync().Result;

            ViewBag.BuilderOptions = _builderOptions.Value;
            ViewBag.Options = _options.Value;
            ViewBag.Localizer = _registerLocalizer;

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email,
                        model.Password, model.RememberMe, lockoutOnFailure: false).ConfigureAndResultAsync();

                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return this.RedirectToLocalUrlOrDefaultPath(returnUrl, _builderOptions.Value.LoginCallbackPath);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, _errorMessageLocalizer.GetString(r => r.InvalidLoginAttempt).Value);
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        /// <summary>
        /// GET: /Account/Register
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            ViewBag.BuilderOptions = _builderOptions.Value;
            ViewBag.Options = _options.Value;
            ViewBag.Localizer = _registerLocalizer;

            return View();
        }

        /// <summary>
        /// POST: /Account/Register
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "model")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            ViewBag.BuilderOptions = _builderOptions.Value;
            ViewBag.Options = _options.Value;
            ViewBag.Localizer = _registerLocalizer;

            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.Id = await _storeIdentifier.GetUserIdAsync().ConfigureAndResultAsync();

                var result = await CreateUserByEmail(_clockService, user, model.Email, model.Password).ConfigureAndResultAsync();
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAndResultAsync();

                    var callbackUrl = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, code },
                        protocol: HttpContext.Request.Scheme);

                    await _emailService.SendAsync(model.Email,
                        _registerLocalizer.GetString(r => r.ConfirmYourEmail)?.Value,
                        _registerLocalizer.GetString(r => r.ConfirmYourEmailFormat, HtmlEncoder.Default.Encode(callbackUrl))?.Value).ConfigureAndWaitAsync();

                    await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();

                    _logger.LogInformation(3, "User created a new account with password.");
                    return this.RedirectToLocalUrlOrDefaultPath(returnUrl, _builderOptions.Value.RegisterCallbackPath);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        /// <summary>
        /// POST: /Account/LogOff
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public async Task<IActionResult> LogOff(string returnUrl = null)
        {
            await _signInManager.SignOutAsync().ConfigureAndWaitAsync();
            _logger.LogInformation(4, "User logged out.");
            return this.RedirectToLocalUrlOrDefaultPath(returnUrl, _builderOptions.Value.LogoutCallbackPath);
        }


        /// <summary>
        /// POST: /Account/ExternalLogin
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        /// <summary>
        /// GET: /Account/ExternalLoginCallback
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="remoteError"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, _errorMessageLocalizer.GetString(r => r.FromExternalProvider, remoteError).Value);
                return View(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync().ConfigureAndResultAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false)
                .ConfigureAndResultAsync();

            if (result.Succeeded)
            {
                // Update any authentication tokens if login succeeded
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info).ConfigureAndWaitAsync();

                _logger.LogInformation(5, "User logged in with {Name} provider.", (string)info.LoginProvider);
                return this.RedirectToLocalUrlOrDefaultPath(returnUrl, _builderOptions.Value.LoginCallbackPath);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;

                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
            }
        }


        /// <summary>
        /// POST: /Account/ExternalLoginConfirmation
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "model")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync().ConfigureAndResultAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }

                var user = CreateUser();
                user.Id = await _storeIdentifier.GetUserIdAsync().ConfigureAndResultAsync();

                var result = await CreateUserByEmail(_clockService, user, model.Email).ConfigureAndResultAsync();
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info).ConfigureAndResultAsync();
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                        _logger.LogInformation(6, "User created an account using {Name} provider.", (string)info.LoginProvider);

                        // Update any authentication tokens as well
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info).ConfigureAndWaitAsync();

                        return this.RedirectToLocalUrlOrDefaultPath(returnUrl, _builderOptions.Value.LoginCallbackPath);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }


        /// <summary>
        /// GET: /Account/ConfirmEmail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId).ConfigureAndResultAsync();
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code).ConfigureAndResultAsync();
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }


        /// <summary>
        /// GET: /Account/ForgotPassword
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// POST: /Account/ForgotPassword
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "model")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAndResultAsync();
                if (user == null || !await _userManager.IsEmailConfirmedAsync(user).ConfigureAndResultAsync())
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                //   "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                //return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// GET: /Account/ForgotPasswordConfirmation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        /// <summary>
        /// GET: /Account/ResetPassword
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code.IsNotEmpty())
                return View("Error");

            ViewBag.BuilderOptions = _builderOptions.Value;
            ViewBag.Options = _options.Value;
            ViewBag.RegisterLocalizer = _registerLocalizer;

            return View();
        }

        /// <summary>
        /// POST: /Account/ResetPassword
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "model")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            ViewBag.BuilderOptions = _builderOptions.Value;
            ViewBag.Options = _options.Value;
            ViewBag.RegisterLocalizer = _registerLocalizer;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAndResultAsync();
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation), "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password).ConfigureAndResultAsync();
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation), "Account");
            }
            AddErrors(result);
            return View();
        }


        /// <summary>
        /// GET: /Account/ResetPasswordConfirmation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        /// <summary>
        /// GET: /Account/SendCode
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        {
            ViewBag.Localizer = _sendCodeLocalizer;

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAndResultAsync();
            if (user == null)
            {
                return View("Error");
            }

            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user).ConfigureAndResultAsync();
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        /// <summary>
        /// GET: /Account/SendCode
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "model")]
        public async Task<IActionResult> SendCode(SendCodeViewModel model)
        {
            ViewBag.Localizer = _sendCodeLocalizer;

            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAndResultAsync();
            if (user == null)
            {
                return View("Error");
            }

            if (model.SelectedProvider == "Authenticator")
            {
                return RedirectToAction(nameof(VerifyAuthenticatorCode), new { model.ReturnUrl, model.RememberMe });
            }

            // Generate the token and send it
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider).ConfigureAndResultAsync();
            if (string.IsNullOrWhiteSpace(code))
            {
                return View("Error");
            }

            var message = _sendCodeLocalizer.GetString(r => r.YourSecurityCodeIs).Value + code;
            if (model.SelectedProvider == "Email")
            {
                await _emailService.SendAsync(await _userManager.GetEmailAsync(user).ConfigureAndResultAsync(), _sendCodeLocalizer.GetString(r => r.SecurityCode).Value, message).ConfigureAndWaitAsync();
            }
            else if (model.SelectedProvider == "Phone")
            {
                await _smsService.SendAsync(await _userManager.GetPhoneNumberAsync(user).ConfigureAndResultAsync(), message).ConfigureAndWaitAsync();
            }

            return RedirectToAction(nameof(VerifyCode), new { Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe });
        }


        /// <summary>
        /// GET: /Account/VerifyCode
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="rememberMe"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        {
            // Require that the user has already logged in via username/password or external login
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAndResultAsync();
            if (user == null)
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        /// <summary>
        /// POST: /Account/VerifyCode
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "model")]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result = await _signInManager.TwoFactorSignInAsync(model.Provider,
                model.Code, model.RememberMe, model.RememberBrowser).ConfigureAndResultAsync();

            if (result.Succeeded)
            {
                return this.RedirectToLocalUrlOrDefaultPath(model.ReturnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning(7, "User account locked out.");
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, _errorMessageLocalizer.GetString(r => r.InvalidAuthenticatorCode).Value);
                return View(model);
            }
        }


        /// <summary>
        /// GET: /Account/VerifyAuthenticatorCode
        /// </summary>
        /// <param name="rememberMe"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public async Task<IActionResult> VerifyAuthenticatorCode(bool rememberMe, string returnUrl = null)
        {
            // Require that the user has already logged in via username/password or external login
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAndResultAsync();
            if (user == null)
            {
                return View("Error");
            }
            return View(new VerifyAuthenticatorCodeViewModel { ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        /// <summary>
        /// POST: /Account/VerifyAuthenticatorCode
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "model")]
        public async Task<IActionResult> VerifyAuthenticatorCode(VerifyAuthenticatorCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(model.Code,
                model.RememberMe, model.RememberBrowser).ConfigureAndResultAsync();

            if (result.Succeeded)
            {
                return this.RedirectToLocalUrlOrDefaultPath(model.ReturnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning(7, "User account locked out.");
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, _errorMessageLocalizer.GetString(r => r.InvalidAuthenticatorCode).Value);
                return View(model);
            }
        }


        /// <summary>
        /// GET: /Account/UseRecoveryCode
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public async Task<IActionResult> UseRecoveryCode(string returnUrl = null)
        {
            // Require that the user has already logged in via username/password or external login
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAndResultAsync();
            if (user == null)
            {
                return View("Error");
            }
            return View(new UseRecoveryCodeViewModel { ReturnUrl = returnUrl });
        }

        /// <summary>
        /// POST: /Account/UseRecoveryCode
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "model")]
        public async Task<IActionResult> UseRecoveryCode(UseRecoveryCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(model.Code).ConfigureAndResultAsync();
            if (result.Succeeded)
            {
                return this.RedirectToLocalUrlOrDefaultPath(model.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, _errorMessageLocalizer.GetString(r => r.InvalidRecoveryCode).Value);
                return View(model);
            }
        }


        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private async Task<IdentityResult> CreateUserByEmail(IClockService clock, TUser user, string email, string password = null,
            CancellationToken cancellationToken = default)
        {
            await _userStore.SetUserNameAsync(user, email, cancellationToken).ConfigureAndWaitAsync();

            if (!_userManager.SupportsUserEmail)
                throw new NotSupportedException("The identity builder requires a user store with email support.");

            var emailStore = (IUserEmailStore<TUser>)_userStore;
            await emailStore.SetEmailAsync(user, email, cancellationToken).ConfigureAndWaitAsync();

            // Populate Creation
            await EntityPopulator.PopulateCreationAsync<AccountController<TUser>>(clock, user, cancellationToken: cancellationToken)
                .ConfigureAndWaitAsync();

            if (password.IsNotEmpty())
                return await _userManager.CreateAsync(user, password).ConfigureAndResultAsync();

            return await _userManager.CreateAsync(user).ConfigureAndResultAsync();
        }

        private TUser CreateUser()
        {
            try
            {
                return typeof(TUser).EnsureCreate<TUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(TUser)}'. " +
                    $"Ensure that '{nameof(TUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register view in ~/Areas/Identity/Views/Account/Register.cshtml");
            }
        }

        #endregion

    }
}
