#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.ComponentModel.DataAnnotations;

namespace Librame.AspNetCore.Identity.UI.Models.AccountViewModels
{
    /// <summary>
    /// 使用恢复代码视图模型。
    /// </summary>
    public class UseRecoveryCodeViewModel
    {
        /// <summary>
        /// 代码。
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 回调 URL。
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
