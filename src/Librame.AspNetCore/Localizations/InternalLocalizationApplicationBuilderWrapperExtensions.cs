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
using System;

namespace Librame.AspNetCore
{
    /// <summary>
    /// 内部本地化 <see cref="IApplicationBuilderWrapper"/> 静态扩展。
    /// </summary>
    internal static class InternalLocalizationApplicationBuilderWrapperExtensions
    {
        /// <summary>
        /// 使用本地化应用。
        /// </summary>
        /// <param name="builderWrapper">给定的 <see cref="IApplicationBuilderWrapper"/>。</param>
        /// <param name="optionsAction">给定的请求本地化选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IApplicationBuilderWrapper"/>。</returns>
        public static IApplicationBuilderWrapper UseLocalization(this IApplicationBuilderWrapper builderWrapper,
            Action<RequestLocalizationOptions> optionsAction = null)
        {
            builderWrapper.RawBuilder.UseRequestLocalization(optionsAction ?? (_ => { }));

            return builderWrapper;
        }

    }
}
