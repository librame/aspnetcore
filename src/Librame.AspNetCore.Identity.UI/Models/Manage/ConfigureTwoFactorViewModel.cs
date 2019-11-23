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

namespace Librame.AspNetCore.Identity.UI
{
    /// <summary>
    /// 配置双因子视图模型。
    /// </summary>
    public class ConfigureTwoFactorViewModel
    {
        /// <summary>
        /// 已选择的提供程序。
        /// </summary>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// 提供程序集合。
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<SelectListItem> Providers { get; set; }
    }
}
