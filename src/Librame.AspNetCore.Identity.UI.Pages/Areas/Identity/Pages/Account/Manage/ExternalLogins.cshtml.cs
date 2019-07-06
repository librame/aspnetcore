#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI.Pages.Account.Manage
{
    using AspNetCore.UI;
    using Extensions.Core;

    /// <summary>
    /// �����ⲿ����ҳ��ģ�͡�
    /// </summary>
    [ThemepackTemplate(typeof(ExternalLoginsPageModel<>))]
    public abstract class AbstractExternalLoginsPageModel : PageModel
    {
        /// <summary>
        /// ��ǰ������Ϣ�б���
        /// </summary>
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        /// <summary>
        /// �������뷽���б���
        /// </summary>
        public IList<AuthenticationScheme> OtherLogins { get; set; }

        /// <summary>
        /// ��ʾ�Ƴ���ť��
        /// </summary>
        public bool ShowRemoveButton { get; set; }

        /// <summary>
        /// ״̬��Ϣ��
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }


        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <returns>����һ�� <see cref="Task{IActionResult}"/>��</returns>
        public virtual Task<IActionResult> OnGetAsync()
            => throw new NotImplementedException();

        /// <summary>
        /// �ύ�Ƴ����뷽����
        /// </summary>
        /// <param name="loginProvider">�����ĵ����ṩ����</param>
        /// <param name="providerKey">�������ṩ������Կ��</param>
        /// <returns>����һ�� <see cref="Task{IActionResult}"/>��</returns>
        public virtual Task<IActionResult> OnPostRemoveLoginAsync(string loginProvider, string providerKey)
            => throw new NotImplementedException();

        /// <summary>
        /// �ύ���ӵ��뷽����
        /// </summary>
        /// <param name="provider">�������ṩ����</param>
        /// <returns>����һ�� <see cref="Task{IActionResult}"/>��</returns>
        public virtual Task<IActionResult> OnPostLinkLoginAsync(string provider)
            => throw new NotImplementedException();

        /// <summary>
        /// ��ȡ���ӵ���ش�������
        /// </summary>
        /// <returns>����һ�� <see cref="Task{IActionResult}"/>��</returns>
        public virtual Task<IActionResult> OnGetLinkLoginCallbackAsync()
            => throw new NotImplementedException();
    }

    internal class ExternalLoginsPageModel<TUser> : AbstractExternalLoginsPageModel where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly SignInManager<TUser> _signInManager;
        private readonly IUserStore<TUser> _userStore;
        private readonly IExpressionStringLocalizer<StatusMessageResource> _statusLocalizer;

        public ExternalLoginsPageModel(
            UserManager<TUser> userManager,
            SignInManager<TUser> signInManager,
            IUserStore<TUser> userStore,
            IExpressionStringLocalizer<StatusMessageResource> statusLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
            _statusLocalizer = statusLocalizer;
        }

        public override async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            CurrentLogins = await _userManager.GetLoginsAsync(user);
            OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
                .Where(auth => CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();

            string passwordHash = null;
            if (_userStore is IUserPasswordStore<TUser> userPasswordStore)
            {
                passwordHash = await userPasswordStore.GetPasswordHashAsync(user, HttpContext.RequestAborted);
            }

            ShowRemoveButton = passwordHash != null || CurrentLogins.Count > 1;
            return Page();
        }

        public override async Task<IActionResult> OnPostRemoveLoginAsync(string loginProvider, string providerKey)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var result = await _userManager.RemoveLoginAsync(user, loginProvider, providerKey);
            if (!result.Succeeded)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                throw new InvalidOperationException($"Unexpected error occurred removing external login for user with ID '{userId}'.");
            }

            await _signInManager.RefreshSignInAsync(user);

            StatusMessage = _statusLocalizer[r => r.ExternalLoginRemoved]?.ToString();

            return RedirectToPage();
        }

        public override async Task<IActionResult> OnPostLinkLoginAsync(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Page("./ExternalLogins", pageHandler: "LinkLoginCallback");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return new ChallengeResult(provider, properties);
        }

        public override async Task<IActionResult> OnGetLinkLoginCallbackAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var info = await _signInManager.GetExternalLoginInfoAsync(userId);
            if (info == null)
            {
                throw new InvalidOperationException($"Unexpected error occurred loading external login info for user with ID '{userId}'.");
            }

            var result = await _userManager.AddLoginAsync(user, info);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred adding external login for user with ID '{userId}'.");
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            StatusMessage = _statusLocalizer[r => r.ExternalLoginAdded]?.ToString();
            return RedirectToPage();
        }
    }
}