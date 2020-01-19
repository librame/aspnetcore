#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.IdentityServer.Web.Resources
{
    using AspNetCore.Web.Resources;

    /// <summary>
    /// 已注销视图资源。
    /// </summary>
    public class LoggedOutViewResource : AbstractViewResource
    {
        /// <summary>
        /// 返回首页。
        /// </summary>
        public string ReturnIndex { get; set; }

        /// <summary>
        /// 后置登出重定向 URL 格式。
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string PostLogoutRedirectUriFormat { get; set; }
    }
}
