#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrameCore.Extensions.Authentication
{
    /// <summary>
    /// 认证扩展选项。
    /// </summary>
    public class AuthenticationExtensionOptions : AuthenticationSchemeOptions, IExtensionOptions
    {
        /// <summary>
        /// 默认认证方案。
        /// </summary>
        public const string DEFAULT_SCHEME = "LibrameAuthentication";

        /// <summary>
        /// 默认 Cookie。
        /// </summary>
        private const string DEFAULT_COOKIE = "LibrameCookie";

        
        /// <summary>
        /// 构造一个 Librame 认证选项。
        /// </summary>
        public AuthenticationExtensionOptions()
            : base()
        {
            ClaimsIssuer = IPEndPointDescriptor.DEFAULT_DOMAIN;
        }


        /// <summary>
        /// 受保护用户名集合。
        /// </summary>
        public string ProtectedUsernames { get; set; } = "librame,admin";

        /// <summary>
        /// 返回 URL 参数。
        /// </summary>
        public string ReturnUrlParameter { get; set; } = "ReturnUrl";

        /// <summary>
        /// Cookie 构建器。
        /// </summary>
        public CookieBuilder Cookie { get; set; } = new CookieBuilder
        {
            Name = DEFAULT_COOKIE,
            Path = "/",
            Domain = IPEndPointDescriptor.DEFAULT_DOMAIN,
            HttpOnly = true,
            Expiration = TimeSpan.FromDays(14)
        };
        
        /// <summary>
        /// 本机选项。
        /// </summary>
        public LocalOptions Local { get; set; } = new LocalOptions();

        /// <summary>
        /// 客户端集合。
        /// </summary>
        public IList<ClientOptions> Clients { get; set; } = new List<ClientOptions>();


        /// <summary>
        /// 断定 URL 包含的主机名是否已注册。
        /// </summary>
        /// <param name="url">给定的 URL。</param>
        /// <returns>返回布尔值。</returns>
        public bool IsHostRegistered(string url)
        {
            Uri uri = null;

            try
            {
                uri = new Uri(url);
            }
            catch
            {
                return false;
            }

            // 如果客户端集合未配置（表示未启用客户端鉴权），或者为本机
            if (Clients.Count < 1 || Local.EndPoint.ToString() == uri.Authority)
                return true;

            // 查询客户端集合
            var exist = Clients.FirstOrDefault(c => c.EndPoint.ToString() == uri.Authority);
            return (exist != null);
        }


        #region Extensions

        /// <summary>
        /// 本机选项。
        /// </summary>
        public class LocalOptions
        {
            /// <summary>
            /// IP 端点。
            /// </summary>
            public IPEndPointDescriptor EndPoint { get; set; } = new IPEndPointDescriptor();

            /// <summary>
            /// 拒绝访问路径。
            /// </summary>
            public string AccessDeniedPath { get; set; } = "/Account/AccessDenied";

            /// <summary>
            /// 登入路径。
            /// </summary>
            public string LoginPath { get; set; } = "/Account/Login";

            /// <summary>
            /// 登出路径。
            /// </summary>
            public string LogoutPath { get; set; } = "/Account/Logout";

            /// <summary>
            /// 认证令牌路径。
            /// </summary>
            public string TokenPath { get; set; } = "/token";

            /// <summary>
            /// 签名证书。
            /// </summary>
            public SigningCredentials Credentials { get; set; }
        }


        /// <summary>
        /// 客户端选项。
        /// </summary>
        public class ClientOptions
        {
            /// <summary>
            /// IP 端点。
            /// </summary>
            public IPEndPointDescriptor EndPoint { get; set; } = new IPEndPointDescriptor();
        }

        #endregion

    }
}
