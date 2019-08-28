#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Librame.AspNetCore.Identity.UI
{
    /// <summary>
    /// 显示恢复码视图模型。
    /// </summary>
    public class DisplayRecoveryCodesViewModel
    {
        /// <summary>
        /// 恢复码集合。
        /// </summary>
        [Required]
        public IEnumerable<string> Codes { get; set; }
    }
}
