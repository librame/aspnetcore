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
    using Utility;

    /// <summary>
    /// 认证适配器静态扩展。
    /// </summary>
    public static class AuthenticationAdapterExtensions
    {
        /// <summary>
        /// 增强令牌生成器。
        /// </summary>
        /// <param name="adapter">给定的认证适配器接口。</param>
        /// <param name="tokenGenerate">给定的令牌生成选项（可选）。</param>
        /// <returns>返回认证适配器接口。</returns>
        public static Authentication.IAuthenticationAdapter BuildUpTokenGenerator(this Authentication.IAuthenticationAdapter adapter,
            Authentication.TokenGenerateOptions tokenGenerate = null)
        {
            adapter.NotNull(nameof(adapter));

            // 令牌生成选项
            if (tokenGenerate == null)
                tokenGenerate = new Authentication.TokenGenerateOptions();

            // 处理签名证书
            if (tokenGenerate.SigningCredentials == null)
            {
                // 默认以授权编号为密钥
                var key = adapter.Builder.FromAuthId();
                var securityKey = new SymmetricSecurityKey(key);

                tokenGenerate.SigningCredentials = new SigningCredentials(securityKey,
                    SecurityAlgorithms.HmacSha256);
            }

            // 注入令牌生成选项
            adapter.Builder.Services.AddSingleton(tokenGenerate);

            return adapter;
        }

    }
}
