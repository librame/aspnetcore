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

namespace Librame.AspNetCore.IdentityServer
{
    /// <summary>
    /// 身份 <see cref="IApplicationBuilderWrapper"/> 静态扩展。
    /// </summary>
    public static class IdentityServerApplicationBuilderWrapperExtensions
    {
        /// <summary>
        /// 使用身份服务器应用。
        /// </summary>
        /// <param name="builderWrapper">给定的 <see cref="IApplicationBuilderWrapper"/>。</param>
        /// <param name="options">给定的 <see cref="IdentityServerMiddlewareOptions"/>。</param>
        /// <returns>返回 <see cref="IApplicationBuilderWrapper"/>。</returns>
        public static IApplicationBuilderWrapper UseIdentityServer(this IApplicationBuilderWrapper builderWrapper,
            IdentityServerMiddlewareOptions options = null)
        {
            builderWrapper.RawBuilder.UseIdentityServer(options);

            return builderWrapper;
        }

    }
}
