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
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI.Pages.Account
{
    using Extensions.Network;
    using Models.AccountViewModels;

    [AllowAnonymous]
    [InternalUIIdentity(typeof(RegisterModel<>))]
    public abstract class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public string ReturnUrl { get; set; }
        

        public virtual void OnGet(string returnUrl = null) => throw new NotImplementedException();

        public virtual Task<IActionResult> OnPostAsync(string returnUrl = null) => throw new NotImplementedException();
    }


    internal class RegisterModel<TUser> : RegisterModel where TUser : class
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly UserManager<TUser> _userManager;
        private readonly IUserStore<TUser> _userStore;
        private readonly IUserEmailStore<TUser> _emailStore;
        private readonly ILogger<AbstractLoginModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<TUser> userManager,
            IUserStore<TUser> userStore,
            SignInManager<TUser> signInManager,
            ILogger<AbstractLoginModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
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

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId, code },
                        protocol: Request.Scheme);

                    await _emailSender.SendAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
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

        private TUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<TUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(TUser)}'. " +
                    $"Ensure that '{nameof(TUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<TUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<TUser>)_userStore;
        }
    }
}
