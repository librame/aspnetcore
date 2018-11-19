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
using System;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI.Pages.Account
{
    using Models.AccountViewModels;

    [AllowAnonymous]
    [InternalUIIdentity(typeof(ResetPasswordModel<>))]
    public abstract class ResetPasswordModel : PageModel
    {
        [BindProperty]
        public ResetPasswordViewModel Input { get; set; }


        public virtual IActionResult OnGet(string code = null) => throw new NotImplementedException();

        public virtual Task<IActionResult> OnPostAsync() => throw new NotImplementedException();
    }


    internal class ResetPasswordModel<TUser> : ResetPasswordModel where TUser : class
    {
        private readonly UserManager<TUser> _userManager;

        public ResetPasswordModel(UserManager<TUser> userManager)
        {
            _userManager = userManager;
        }

        public override IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new ResetPasswordViewModel
                {
                    Code = code
                };
                return Page();
            }
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
