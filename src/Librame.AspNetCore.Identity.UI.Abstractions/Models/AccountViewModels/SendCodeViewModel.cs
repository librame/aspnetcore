#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Librame.AspNetCore.Identity.UI.Models.AccountViewModels
{
    /// <summary>
    /// 发送代码视图模型。
    /// </summary>
    public class SendCodeViewModel
    {
        /// <summary>
        /// 选择的提供程序。
        /// </summary>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// 提供程序集合。
        /// </summary>
        public ICollection<SelectListItem> Providers { get; set; }

        /// <summary>
        /// 回调 URL。
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 记住我。
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
