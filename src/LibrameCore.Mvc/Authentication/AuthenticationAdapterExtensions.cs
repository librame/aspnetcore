#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LibrameCore
{
    using Authentication;
    using Utilities;

    /// <summary>
    /// 认证适配器静态扩展。
    /// </summary>
    public static class AuthenticationAdapterExtensions
    {
        /// <summary>
        /// 增强令牌。
        /// </summary>
        /// <param name="adapter">给定的认证适配器接口。</param>
        /// <param name="options">给定的令牌选项（可选）。</param>
        /// <returns>返回认证适配器接口。</returns>
        public static IAuthenticationAdapter BuildUpToken(this IAuthenticationAdapter adapter,
            TokenOptions options = null)
        {
            adapter.NotNull(nameof(adapter));

            // 令牌生成选项
            if (options == null)
                options = new TokenOptions();

            // 处理签名证书
            if (options.SigningCredentials == null)
            {
                // 默认以授权编号为密钥
                var key = adapter.Builder.FromAuthId();
                var securityKey = new SymmetricSecurityKey(key);

                options.SigningCredentials = new SigningCredentials(securityKey,
                    SecurityAlgorithms.HmacSha256);
            }

            // 注入令牌生成选项
            adapter.Builder.Services.AddSingleton(options);

            return adapter;
        }

    }
}
