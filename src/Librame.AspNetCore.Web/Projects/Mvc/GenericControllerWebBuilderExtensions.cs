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
    using AspNetCore.Web.Builders;
    using Extensions;

    /// <summary>
    /// 泛型控制器 Web 构建器静态扩展。
    /// </summary>
    public static class GenericControllerWebBuilderExtensions
    {
        /// <summary>
        /// 添加泛型控制器集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IWebBuilder AddGenericControllers(this IWebBuilder builder)
        {
            builder.NotNull(nameof(builder));

            if (builder.SupportedGenericController)
                return builder;

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

            builder.EnableSupportedGenericController();

            return builder;
        }

        /// <summary>
        /// 添加带用户的泛型控制器集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IWebBuilder AddGenericControllersWithUser(this IWebBuilder builder)
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
