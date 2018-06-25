#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Extensions.Platform;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LibrameCore.Abstractions
{
    /// <summary>
    /// <see cref="IExtensionBuilder"/> 平台静态扩展。
    /// </summary>
    public static class PlatformExtensionBuilderExtensions
    {

        /// <summary>
        /// 使用平台扩展。
        /// </summary>
        /// <param name="extension">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <returns>返回 <see cref="IExtensionBuilder"/>。</returns>
        public static IExtensionBuilder UsePlatformExtension(this IExtensionBuilder extension)
        {
            var schedule = extension.Builder.ApplicationServices.GetRequiredService<ISchedulePlatform>();

            // 提供容器支持
            schedule.ApplyServiceProvider(extension.Builder.ApplicationServices);

            extension.Builder.UseMiddleware<ScheduleMiddleware>();

            return extension;
        }

    }
}
