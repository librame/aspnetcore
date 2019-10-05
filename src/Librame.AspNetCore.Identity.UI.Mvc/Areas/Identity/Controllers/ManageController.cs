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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI.Controllers
{
    using AspNetCore.UI;
    using Extensions;
    using Extensions.Core;
    using Extensions.Data;
    using Extensions.Network;

    /// <summary>
    /// 管理控制器。
    /// </summary>
    [Authorize]
    [GenericApplicationModel]
    public class ManageController<TUser> : ApplicationController
        where TUser : class, IId<string>
    {
        [InjectionService]
        private ILogger<AccountController<TUser>> _logger = null;

        [InjectionService]
        private ISmsService _smsService = null;

        [InjectionService]
        private IOptions<IdentityBuilderOptions> _builderOptions = null;

        [InjectionService]
        private IOptions<IdentityOptions> _options = null;

        [InjectionService]
        private IExpressionHtmlLocalizer<RegisterViewResource> _registerLocalizer = null;

        [InjectionService]
        private IExpressionHtmlLocalizer<IndexViewResource> _indexLocalizer = null;

        [InjectionService]
        private IExpressionHtmlLocalizer<AddPhoneNumberViewResource> _addPhoneNumberLocalizer = null;

        [InjectionService]
        private IExpressionHtmlLocalizer<VerifyPhoneNumberViewResource> _verifyPhoneNumberLocalizer = null;

        [InjectionService]
        private IExpressionHtmlLocalizer<ExternalLoginsViewResource> _manageLoginsLocalizer = null;

        [InjectionService]
        private SignInManager<TUser> _signInManager = null;

        private readonly UserManager<TUser> _userManager = null;


        /// <summary>
        /// 构造一个 <see cref="ManageController{TUser}"/>。
        /// </summary>
        /// <param name="injectionService">给定的 <see cref="IInjectionService"/>。</param>
        /// <param name="application">给定的 <see cref="IApplicationContext"/>。</param>
        public ManageController(IInjectionService injectionService, IApplicationContext application)
            : base(application)
        {
            injectionService.Inject(this);

            _userManager = _signInManager.UserManager;
        }


        /// <summary>
        /// GET: /Manage/Index
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess ? _indexLocalizer[r => r.ChangePasswordSuccess].Value
                : message == ManageMessageId.SetPasswordSuccess ? _indexLocalizer[r => r.SetPasswordSuccess].Value
                : message == ManageMessageId.SetTwoFactorSuccess ? _indexLocalizer[r => r.SetTwoFactorSuccess].Value
                : message == ManageMessageId.Error ? _indexLocalizer[r => r.Error].Value
                : message == ManageMessageId.AddPhoneSuccess ? _indexLocalizer[r => r.AddPhoneSuccess].Value
                : message == ManageMessageId.RemovePhoneSuccess ? _indexLocalizer[r => r.RemovePhoneSuccess].Value
                : "";

            ViewBag.Localizer = _indexLocalizer;

            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            var model = new IndexViewModel
            {
                HasPassword = await _userManager.HasPasswordAsync(user).ConfigureAndResultAsync(),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user).ConfigureAndResultAsync(),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(user).ConfigureAndResultAsync(),
                Logins = await _userManager.GetLoginsAsync(user).ConfigureAndResultAsync(),
                BrowserRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user).ConfigureAndResultAsync(),
                AuthenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user).ConfigureAndResultAsync()
            };
            return View(model);
        }


        /// <summary>
        /// POST: /Manage/RemoveLogin
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel account)
        {
            ManageMessageId? message = ManageMessageId.Error;
            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            if (user != null)
            {
                var result = await _userManager.RemoveLoginAsync(user, account.LoginProvider, account.ProviderKey).ConfigureAndResultAsync();
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                    message = ManageMessageId.RemoveLoginSuccess;
                }
            }
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }


        /// <summary>
        /// GET: /Manage/AddPhoneNumber
        /// </summary>
        /// <returns></returns>
        public IActionResult AddPhoneNumber()
        {
            ViewBag.Localizer = _addPhoneNumberLocalizer;

            return View();
        }

        /// <summary>
        /// POST: /Manage/AddPhoneNumber
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.Phone).ConfigureAndResultAsync();
            
            await _smsService.SendAsync(model.Phone, _addPhoneNumberLocalizer[r => r.YourSecurityCodeIs].Value + code).ConfigureAndWaitAsync();
            
            return RedirectToAction(nameof(VerifyPhoneNumber), new { phoneNumber = model.Phone });
        }


        /// <summary>
        /// POST: /Manage/ResetAuthenticatorKey
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetAuthenticatorKey()
        {
            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            if (user != null)
            {
                await _userManager.ResetAuthenticatorKeyAsync(user).ConfigureAndResultAsync();
                _logger.LogInformation(1, "User reset authenticator key.");
            }
            return RedirectToAction(nameof(Index), "Manage");
        }


        /// <summary>
        /// POST: /Manage/GenerateRecoveryCode
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateRecoveryCode()
        {
            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            if (user != null)
            {
                var codes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 5).ConfigureAndResultAsync();
                _logger.LogInformation(1, "User generated new recovery code.");
                return View("DisplayRecoveryCodes", new DisplayRecoveryCodesViewModel { Codes = codes });
            }
            return View("Error");
        }


        /// <summary>
        /// POST: /Manage/EnableTwoFactorAuthentication
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true).ConfigureAndWaitAsync();
                await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                _logger.LogInformation(1, "User enabled two-factor authentication.");
            }
            return RedirectToAction(nameof(Index), "Manage");
        }


        /// <summary>
        /// POST: /Manage/DisableTwoFactorAuthentication
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, false).ConfigureAndWaitAsync();
                await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                _logger.LogInformation(2, "User disabled two-factor authentication.");
            }
            return RedirectToAction(nameof(Index), "Manage");
        }


        /// <summary>
        /// GET: /Manage/VerifyPhoneNumber
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            ViewBag.Localizer = _verifyPhoneNumberLocalizer;

            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber).ConfigureAndWaitAsync();

            // Send an SMS to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { Phone = phoneNumber });
        }

        /// <summary>
        /// POST: /Manage/VerifyPhoneNumber
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePhoneNumberAsync(user, model.Phone, model.Code).ConfigureAndResultAsync();
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.AddPhoneSuccess });
                }
            }
            // If we got this far, something failed, redisplay the form
            ModelState.AddModelError(string.Empty, _verifyPhoneNumberLocalizer[r => r.Failed].Value);
            return View(model);
        }


        /// <summary>
        /// GET: /Manage/RemovePhoneNumber
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePhoneNumber()
        {
            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            if (user != null)
            {
                var result = await _userManager.SetPhoneNumberAsync(user, null).ConfigureAndResultAsync();
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.RemovePhoneSuccess });
                }
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }


        /// <summary>
        /// GET: /Manage/ChangePassword
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.BuilderOptions = _builderOptions.Value;
            ViewBag.Options = _options.Value;
            ViewBag.RegisterLocalizer = _registerLocalizer;

            return View();
        }

        /// <summary>
        /// POST: /Manage/ChangePassword
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).ConfigureAndResultAsync();
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }


        /// <summary>
        /// GET: /Manage/SetPassword
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SetPassword()
        {
            ViewBag.BuilderOptions = _builderOptions.Value;
            ViewBag.Options = _options.Value;
            ViewBag.RegisterLocalizer = _registerLocalizer;

            return View();
        }

        /// <summary>
        /// POST: /Manage/SetPassword
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            if (user != null)
            {
                var result = await _userManager.AddPasswordAsync(user, model.NewPassword).ConfigureAndResultAsync();
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }


        /// <summary>
        /// GET: /Manage/ManageLogins
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ManageLogins(ManageMessageId? message = null)
        {
            ViewBag.Localizer = _manageLoginsLocalizer;

            ViewData["StatusMessage"] =
                message == ManageMessageId.RemoveLoginSuccess ? _manageLoginsLocalizer[r => r.RemoveLoginSuccess].Value
                : message == ManageMessageId.AddLoginSuccess ? _manageLoginsLocalizer[r => r.AddLoginSuccess].Value
                : message == ManageMessageId.Error ? _manageLoginsLocalizer[r => r.Error].Value
                : "";
            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            if (user == null)
            {
                return View("Error");
            }

            var userLogins = await _userManager.GetLoginsAsync(user).ConfigureAndResultAsync();
            var schemes = await _signInManager.GetExternalAuthenticationSchemesAsync().ConfigureAndResultAsync();
            var otherLogins = schemes.Where(auth => userLogins.All(ul => auth.Name != ul.LoginProvider)).ToList();
            ViewData["ShowRemoveButton"] = _userManager.HasPasswordAsync(user).Result || userLogins.Count > 1;

            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }


        /// <summary>
        /// POST: /Manage/LinkLogin
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action("LinkLoginCallback", "Manage");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return Challenge(properties, provider);
        }

        /// <summary>
        /// GET: /Manage/LinkLoginCallback
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> LinkLoginCallback()
        {
            var user = await GetCurrentUserAsync().ConfigureAndResultAsync();
            if (user == null)
            {
                return View("Error");
            }

            var userId = await _userManager.GetUserIdAsync(user).ConfigureAndResultAsync();
            var info = await _signInManager.GetExternalLoginInfoAsync(userId).ConfigureAndResultAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(ManageLogins), new { Message = ManageMessageId.Error });
            }

            var result = await _userManager.AddLoginAsync(user, info).ConfigureAndResultAsync();
            var message = result.Succeeded ? ManageMessageId.AddLoginSuccess : ManageMessageId.Error;
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }


        #region Helpers

        /// <summary>
        /// 管理消息标识。
        /// </summary>
        public enum ManageMessageId
        {
            /// <summary>
            /// 增加手机号成功。
            /// </summary>
            AddPhoneSuccess,

            /// <summary>
            /// 增加登入成功。
            /// </summary>
            AddLoginSuccess,

            /// <summary>
            /// 修改密码成功。
            /// </summary>
            ChangePasswordSuccess,

            /// <summary>
            /// 设置双因子成功。
            /// </summary>
            SetTwoFactorSuccess,

            /// <summary>
            /// 设置密码成功。
            /// </summary>
            SetPasswordSuccess,

            /// <summary>
            /// 移除登入成功。
            /// </summary>
            RemoveLoginSuccess,

            /// <summary>
            /// 移除手机号成功。
            /// </summary>
            RemovePhoneSuccess,

            /// <summary>
            /// 错误。
            /// </summary>
            Error
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private async Task<TUser> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User).ConfigureAndResultAsync();
        }

        #endregion

    }
}
