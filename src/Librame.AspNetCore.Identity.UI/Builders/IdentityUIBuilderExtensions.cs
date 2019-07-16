#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Configuration;
using System;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;

    /// <summary>
    /// 身份 UI 构建器静态扩展。
    /// </summary>
    public static class IdentityUIBuilderExtensions
    {
        /// <summary>
        /// 添加身份 UI 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityBuilder"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{UIBuilderOptions}"/>（可选；高优先级）。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选；次优先级）。</param>
        /// <param name="configureBinderOptions">给定的配置绑定器选项动作（可选）。</param>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public static IUIBuilder AddIdentityUI(this IIdentityBuilder builder,
            Action<UIBuilderOptions> configureOptions = null,
            IConfiguration configuration = null,
            Action<BinderOptions> configureBinderOptions = null)
        {
            var applicationContextType = typeof(IdentityApplicationContext<>)
                .MakeGenericType(builder.CoreIdentityBuilder.UserType);
            var applicationPostConfigureOptionsType = typeof(IdentityApplicationPostConfigureOptions<>)
                .MakeGenericType(builder.CoreIdentityBuilder.UserType);

            Action<UIBuilderOptions> _configureOptions = options =>
            {
                options.ApplicationContextType = applicationContextType;
                options.ApplicationPostConfigureOptionsType = applicationPostConfigureOptionsType;

                configureOptions?.Invoke(options);
            };

            var uiBuilder = builder.AddUI(_configureOptions,
                configuration, configureBinderOptions);

            return uiBuilder;
        }

    }
}
