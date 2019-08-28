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

namespace Librame.AspNetCore.Api
{
    /// <summary>
    /// API <see cref="IApplicationBuilderWrapper"/> 静态扩展。
    /// </summary>
    public static class ApiApplicationBuilderWrapperExtensions
    {
        /// <summary>
        /// 使用 API 应用。
        /// </summary>
        /// <param name="builderWrapper">给定的 <see cref="IApplicationBuilderWrapper"/>。</param>
        /// <returns>返回 <see cref="IApplicationBuilderWrapper"/>。</returns>
        public static IApplicationBuilderWrapper UseApi(this IApplicationBuilderWrapper builderWrapper)
        {
            builderWrapper.RawBuilder.UseMiddleware<ApiApplicationMiddleware>();

            return builderWrapper;
        }

    }
}
