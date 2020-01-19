#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Web.Resources
{
    using AspNetCore.Web.Resources;

    /// <summary>
    /// 登出视图资源。
    /// </summary>
    public class LogoutViewResource : AbstractViewResource
    {
        /// <summary>
        /// 返回首页。
        /// </summary>
        public string ReturnIndex { get; set; }
    }
}
