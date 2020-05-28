#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account
{
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Stores;
    using AspNetCore.Identity.Web.Models;
    using AspNetCore.Identity.Web.Resources;
    using AspNetCore.Web;
    using Extensions;
    using Extensions.Core.Identifiers;
    using Extensions.Core.Services;
    using Extensions.Network.Services;

    /// <summary>
    /// 注册页面模型。
    /// </summary>
    [AllowAnonymous]
    [GenericApplicationModel(typeof(RegisterPageModel<>))]
    public class RegisterPageModel : PageModel
    {
        /// <summary>
        /// 构造一个 <see cref="RegisterPageModel"/> 实例。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{RegisterViewResource}"/>。</param>
        /// <param name="builderOptions">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="options">给定的 <see cref="IOptions{IdentityOptions}"/>。</param>
        protected RegisterPageModel(IHtmlLocalizer<RegisterViewResource> localizer,
            IOptions<IdentityBuilderOptions> builderOptions, IOptions<IdentityOptions> options)
        {
            Localizer = localizer.NotNull(nameof(localizer));
            BuilderOptions = builderOptions.NotNull(nameof(builderOptions)).Value;
            Options = options.NotNull(nameof(options)).Value;
        }


        /// <summary>
        /// 本地化资源。
        /// </summary>
        public IHtmlLocalizer<RegisterViewResource> Localizer { get; }

        /// <summary>
        /// 构建器选项。
        /// </summary>
        public IdentityBuilderOptions BuilderOptions { get; }

        /// <summary>
        /// 选项。
        /// </summary>
        public IdentityOptions Options { get; }

        /// <summary>
        /// 注册视图模型。
        /// </summary>
        [BindProperty]
        public RegisterViewModel Input { get; set; }

        /// <summary>
        /// 返回 URL。
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string ReturnUrl { get; set; }


        /// <summary>
        /// 获取方法。
        /// </summary>
        /// <param name="returnUrl">给定的返回 URL。</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        public virtual void OnGet(string returnUrl = null)
            => throw new NotImplementedException();

        /// <summary>
        /// 异步提交方法。
        /// </summary>
        /// <param name="returnUrl">给定的返回 URL。</param>
        /// <returns>返回一个包含 <see cref="IActionResult"/> 的异步操作。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        public virtual Task<IActionResult> OnPostAsync(string returnUrl = null)
            => throw new NotImplementedException();
    }


    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class RegisterPageModel<TUser> : RegisterPageModel
        where TUser : class, IIdentifier
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly UserManager<TUser> _userManager;
        private readonly IUserStore<TUser> _userStore;
        private readonly ILogger<LoginPageModel> _logger;
        private readonly IClockService _clockService;
        private readonly IEmailService _emailService;
        private readonly IIdentityStoreIdentifierGenerator _identifierGenerator;


        public RegisterPageModel(
            SignInManager<TUser> signInManager,
            IUserStore<TUser> userStore,
            ILogger<LoginPageModel> logger,
            IClockService clockService,
            IEmailService emailService,
            ServiceFactory serviceFactory,
            IHtmlLocalizer<RegisterViewResource> localizer,
            IOptions<IdentityBuilderOptions> builderOptions,
            IOptions<IdentityOptions> options)
            : base(localizer, builderOptions, options)
        {
            _signInManager = signInManager.NotNull(nameof(signInManager));
            _userStore = userStore.NotNull(nameof(userStore));
            _logger = logger.NotNull(nameof(logger));
            _clockService = clockService.NotNull(nameof(clockService));
            _emailService = emailService.NotNull(nameof(emailService));

            _identifierGenerator = serviceFactory.GetIdentityStoreIdentifierGeneratorByUser<TUser>();
            _userManager = signInManager.UserManager;
        }


        public override void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public override async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                var userId = await _identifierGenerator.GenerateUserIdAsync().ConfigureAndResultAsync();
                await user.SetIdAsync(userId).ConfigureAndWaitAsync();

                var result = await _userManager.CreateUserByEmail<RegisterPageModel<TUser>, TUser>(_userStore,
                    _clockService, user, Input.Email, Input.Password).ConfigureAndResultAsync();

                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAndResultAsync();

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = userId.ToString(), code },
                        protocol: Request.Scheme);

                    await _emailService.SendAsync(Input.Email,
                        Localizer.GetString(r => r.ConfirmYourEmail)?.Value,
                        Localizer.GetString(r => r.ConfirmYourEmailFormat, HtmlEncoder.Default.Encode(callbackUrl))?.Value).ConfigureAndWaitAsync();

                    await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                    _logger.LogInformation(3, "User created a new account with password.");

                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        //private async Task<IdentityResult> CreateUserByEmail(IClockService clock, TUser user, string email, string password = null,
        //    CancellationToken cancellationToken = default)
        //{
        //    await _userStore.SetUserNameAsync(user, email, cancellationToken).ConfigureAndWaitAsync();

        //    if (!_userManager.SupportsUserEmail)
        //        throw new NotSupportedException("The identity builder requires a user store with email support.");

        //    var emailStore = (IUserEmailStore<TUser>)_userStore;
        //    await emailStore.SetEmailAsync(user, email, cancellationToken).ConfigureAndWaitAsync();

        //    // Populate Creation
        //    await EntityPopulator.PopulateCreationAsync<RegisterPageModel>(clock, user, cancellationToken: cancellationToken)
        //        .ConfigureAndWaitAsync();

        //    if (password.IsNotEmpty())
        //        return await _userManager.CreateAsync(user, password).ConfigureAndResultAsync();

        //    return await _userManager.CreateAsync(user).ConfigureAndResultAsync();
        //}

        private static TUser CreateUser()
        {
            try
            {
                return typeof(TUser).EnsureCreate<TUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(TUser)}'. " +
                    $"Ensure that '{nameof(TUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in ~/Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

    }
}
