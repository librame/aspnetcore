#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Platform;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LibrameCore
{
    /// <summary>
    /// <see cref="ILibrameModuleBuilder"/> 静态扩展。
    /// </summary>
    public static class PlatformLibrameModuleBuilderExtensions
    {

        /// <summary>
        /// 使用平台模块。
        /// </summary>
        /// <param name="modules">给定的 <see cref="ILibrameModuleBuilder"/>。</param>
        /// <returns>返回 <see cref="ILibrameModuleBuilder"/>。</returns>
        public static ILibrameModuleBuilder UsePlatform(this ILibrameModuleBuilder modules)
        {
            var schedule = modules.Builder.ApplicationServices.GetRequiredService<ISchedulePlatform>();
            // 提供容器支持
            schedule.ApplyServiceProvider(modules.Builder.ApplicationServices);

            modules.Builder.UseMiddleware<ScheduleMiddleware>();

            return modules;
        }

    }
}
