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
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Librame.AspNetCore.Identity.UI.Pages.Account
{
    /// <summary>
    /// ��������ȷ��ҳ��ģ�͡�
    /// </summary>
    [AllowAnonymous]
    public class ForgotPasswordConfirmationPageModel : PageModel
    {
        /// <summary>
        /// ��ȡ������
        /// </summary>
        public void OnGet()
        {
        }

    }
}