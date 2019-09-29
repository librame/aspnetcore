#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.IdentityServer.UI
{
    using Extensions.Core;

    /// <summary>
    /// 身份服务器界面信息资源。
    /// </summary>
    public class IdentityServerInterfaceInfoResource : IResource
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }
    }
}
