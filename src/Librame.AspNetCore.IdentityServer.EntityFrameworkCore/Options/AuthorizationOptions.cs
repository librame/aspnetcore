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
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace Librame.AspNetCore.IdentityServer.Options
{
    /// <summary>
    /// 授权选项。
    /// </summary>
    public class AuthorizationOptions
    {
        /// <summary>
        /// 身份资源集合。
        /// </summary>
        public List<IdentityResource> IdentityResources { get; }
            = new List<IdentityResource>();

        /// <summary>
        /// API 资源集合。
        /// </summary>
        public List<ApiResource> ApiResources { get; }
            = new List<ApiResource>();

        /// <summary>
        /// 客户端集合。
        /// </summary>
        public List<Client> Clients { get; }
            = new List<Client>();

        /// <summary>
        /// 用于签名令牌的签名证书。
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
