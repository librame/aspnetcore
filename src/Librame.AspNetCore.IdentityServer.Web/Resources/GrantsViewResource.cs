#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.IdentityServer.Web.Resources
{
    using AspNetCore.Web.Resources;

    /// <summary>
    /// 授予视图资源。
    /// </summary>
    public class GrantsViewResource : AbstractViewResource
    {
        /// <summary>
        /// 没有授予访问权限。
        /// </summary>
        public string NotGivenAccess { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        public string CreatedTime { get; set; }

        /// <summary>
        /// 过期时间。
        /// </summary>
        public string ExpiresTime { get; set; }

        /// <summary>
        /// 身份授权。
        /// </summary>
        public string IdentityGrants { get; set; }

        /// <summary>
        /// API 授权。
        /// </summary>
        public string ApiGrants { get; set; }

        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }
    }
}
