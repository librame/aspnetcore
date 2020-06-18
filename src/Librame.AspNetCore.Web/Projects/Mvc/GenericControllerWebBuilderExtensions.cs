#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Projects
{
    using AspNetCore.Web.Applications;
    using AspNetCore.Web.Builders;
    using Extensions;

    /// <summary>
    /// 泛型控制器 Web 构建器静态扩展。
    /// </summary>
    public static class GenericControllerWebBuilderExtensions
    {
        /// <summary>
        /// 添加带用户的泛型控制器集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IWebBuilder AddGenericControllers(this IWebBuilder builder)
        {
            if (!builder.SupportedGenericController)
                builder.AddGenericControllersCore();

            return builder;
        }

        private static IWebBuilder AddGenericControllersCore(this IWebBuilder builder)
        {
            var replaceAssembly = typeof(ApplicationPartManager).Assembly;

            // 重置 MVC 默认应用模型提供程序
            var oldDefaultApplicationModelProviderType = replaceAssembly
                .GetType("Microsoft.AspNetCore.Mvc.ApplicationModels.DefaultApplicationModelProvider");

            builder.Services.TryReplaceAll(typeof(IApplicationModelProvider),
                typeof(ResetDefaultApplicationModelProvider),
                oldDefaultApplicationModelProviderType);

            builder.Services.AddSingleton<ResetApplicationModelFactory>();

            // 重置 MVC 控制器动作描述符提供程序
            var oldControllerActionDescriptorProviderType = replaceAssembly
                .GetType("Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerActionDescriptorProvider");

            builder.Services.TryReplaceAll(typeof(IActionDescriptorProvider),
                typeof(ResetControllerActionDescriptorProvider),
                oldControllerActionDescriptorProviderType);

            // 添加泛型控制器应用特征提供程序
            if (builder.Services.TryGetAll<ApplicationPartManager>(out var descriptors))
            {
                descriptors.ForEach(descriptor =>
                {
                    var manager = (ApplicationPartManager)descriptor.ImplementationInstance;
                    manager.FeatureProviders.Add(new GenericControllerApplicationFeatureProvider(builder));
                });
            }

            // 启用泛型控制器支持
            builder.EnableSupportedGenericController();

            return builder;
        }

    }
}
