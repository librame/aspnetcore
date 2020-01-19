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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Web.Models
{
    /// <summary>
    /// 发送码视图模型。
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
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<SelectListItem> Providers { get; set; }

        /// <summary>
        /// 返回 URL。
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 记住我。
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
