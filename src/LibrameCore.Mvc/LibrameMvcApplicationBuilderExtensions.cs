#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameCore;
using LibrameCore.Authentication;
using LibrameCore.Utility;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Librame MVC 应用构建器静态扩展。
    /// </summary>
    public static class LibrameMvcApplicationBuilderExtensions
    {
        /// <summary>
        /// 获取 Librame 构建器接口。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder GetLibrameBuilder(this IApplicationBuilder app)
        {
            var builder = app.ApplicationServices.GetService(typeof(ILibrameBuilder));

            return (builder.NotNull(nameof(builder)) as ILibrameBuilder);
        }


        /// <summary>
        /// 使用 Librame 应用。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        /// <param name="tokenGenerate">给定的令牌生成选项（可选）。</param>
        /// <returns>返回应用构建器接口。</returns>
        public static IApplicationBuilder UseLibrameMvc(this IApplicationBuilder app,
            TokenGenerateOptions tokenGenerate = null)
        {
            app.NotNull(nameof(app));

            // 取得 Librame 构建器
            var builder = app.GetLibrameBuilder();

            // 运行认证适配器
            var authAdapter = builder.GetAuthenticationAdapter();
            var tokenOptions = authAdapter.TokenGenerator.Options;
            app.Map(tokenOptions.Path, authAdapter.TokenGenerator.Generate);
            app.Run(async context =>
            {
                await context.Response.WriteAsync("This Service only use for authentication! ");
            });

            return app;
        }

    }
}
