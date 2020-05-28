#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Web.Resources
{
    using AspNetCore.Web.Resources;

    /// <summary>
    /// 显示恢复码视图资源。
    /// </summary>
    public class ShowRecoveryCodesViewResource : AbstractViewResource
    {
        /// <summary>
        /// 信息。
        /// </summary>
        public string Info { get; set; }
    }
}
