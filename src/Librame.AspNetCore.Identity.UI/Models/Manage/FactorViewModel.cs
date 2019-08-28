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

namespace Librame.AspNetCore.Identity.UI
{
    /// <summary>
    /// 因子视图模型。
    /// </summary>
    public class FactorViewModel
    {
        /// <summary>
        /// 用途。
        /// </summary>
        public string Purpose { get; set; }
    }
}
