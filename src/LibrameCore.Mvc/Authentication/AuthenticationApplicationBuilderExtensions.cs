#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;

namespace LibrameCore.Authentication
{
    using Utilities;

    /// <summary>
    /// 认证应用构建器静态扩展。
    /// </summary>
    public static class AuthenticationApplicationBuilderExtensions
    {

        /// <summary>
        /// 使用 Librame 认证令牌。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        /// <param name="options">给定的令牌选项（可选）。</param>
        /// <param name="builder">给定的 Librame 构建器（可选）。</param>
        public static void UseLibrameAuthenticationToken(this IApplicationBuilder app,
            TokenOptions options = null, ILibrameBuilder builder = null)
        {
            app.NotNull(nameof(app));

            if (builder == null)
                builder = app.GetLibrameBuilder();

            // 运行认证适配器
            var adapter = builder.GetAuthenticationAdapter(options);

            var tokenOptions = adapter.TokenGenerator.Options;
            app.Map(tokenOptions.Path, adapter.TokenHandler.OnHandling);
        }

    }
}
