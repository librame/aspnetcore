#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.IdentityServer.Options
{
    /// <summary>
    /// 身份服务器存储初始化选项。
    /// </summary>
    public class IdentityServerStoreInitializationOptions
    {
        /// <summary>
        /// 默认 API 资源列表集合。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public List<ApiResource> DefaultApiResources { get; set; }
            = new List<ApiResource>();

        /// <summary>
        /// 默认客户端列表集合。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public List<Client> DefaultClients { get; set; }
            = new List<Client>();

        /// <summary>
        /// 默认身份资源列表集合。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public List<IdentityResource> DefaultIdentityResources { get; set; }
            = new List<IdentityResource>();
    }
}
