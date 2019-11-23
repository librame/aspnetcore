#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 泛型控制器 UI 构建器静态扩展。
    /// </summary>
    public static class GenericControllerUiBuilderExtensions
    {
        /// <summary>
        /// 添加泛型控制器集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUiBuilder"/>。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "builder")]
        public static IUiBuilder AddGenericControllers(this IUiBuilder builder)
        {
            builder.NotNull(nameof(builder));

            var replaceAssembly = typeof(ApplicationPartManager).Assembly;

            var defaultApplicationModelProviderType = replaceAssembly
                .GetType("Microsoft.AspNetCore.Mvc.ApplicationModels.DefaultApplicationModelProvider");
            builder.Services.TryReplace(typeof(IApplicationModelProvider), defaultApplicationModelProviderType,
                typeof(ResetDefaultApplicationModelProvider));

            builder.Services.AddSingleton<ResetApplicationModelFactory>();

            var controllerActionDescriptorProviderType = replaceAssembly
                .GetType("Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerActionDescriptorProvider");
            builder.Services.TryReplace(typeof(IActionDescriptorProvider), controllerActionDescriptorProviderType,
                typeof(ResetControllerActionDescriptorProvider));

            return builder;
        }

        /// <summary>
        /// 添加带用户的泛型控制器集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUiBuilder"/>。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "builder")]
        public static IUiBuilder AddGenericControllersWithUser(this IUiBuilder builder)
        {
            builder.AddGenericControllers();

            if (builder.Services.TryGet<ApplicationPartManager>(out ServiceDescriptor descriptor))
            {
                var manager = (ApplicationPartManager)descriptor.ImplementationInstance;
                manager.FeatureProviders.Add(new GenericControllerModelProviderWithUser(builder.UserType));
            }

            return builder;
        }

    }
}
