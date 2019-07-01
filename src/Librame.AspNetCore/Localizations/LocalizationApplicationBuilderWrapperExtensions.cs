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

namespace Librame.AspNetCore
{
    /// <summary>
    /// 本地化应用程序构建器包装静态扩展。
    /// </summary>
    public static class LocalizationApplicationBuilderWrapperExtensions
    {
        /// <summary>
        /// 使用本地化。
        /// </summary>
        /// <param name="wrapper">给定的 <see cref="IApplicationBuilderWrapper"/>。</param>
        /// <returns>返回 <see cref="IApplicationBuilderWrapper"/>。</returns>
        public static IApplicationBuilderWrapper UseLocalization(this IApplicationBuilderWrapper wrapper)
        {
            // var requestLocalizationOptions = app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>();
            wrapper.Builder.UseRequestLocalization(); //requestLocalizationOptions.Value

            return wrapper;
        }

    }
}
