#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Web.Builders;
using Librame.AspNetCore.Web.Projects;
using Librame.AspNetCore.Web.Themepacks;
using Librame.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 项目 Web 构建器静态扩展。
    /// </summary>
    public static class ProjectWebBuilderExtensions
    {

        #region AddProject

        /// <summary>
        /// 添加区域核心。
        /// </summary>
        /// <typeparam name="TConfiguration">指定的配置类型。</typeparam>
        /// <typeparam name="TNavigation">指定的导航类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IWebBuilder AddProjectCore<TConfiguration, TNavigation>(this IWebBuilder builder)
            where TConfiguration : ProjectConfigurationBase
            where TNavigation : class, IProjectNavigation
        {
            builder.NotNull(nameof(builder));

            builder.AddService<IProjectNavigation, TNavigation>();
            builder.Services.ConfigureOptions(typeof(TConfiguration));

            return builder;
        }

        /// <summary>
        /// 添加区域控制器。
        /// </summary>
        /// <typeparam name="TConfiguration">指定的配置类型。</typeparam>
        /// <typeparam name="TNavigation">指定的导航类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <param name="mvcBuilder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <param name="razorAssembly">给定包含页面集合的 <see cref="Assembly"/>。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IWebBuilder AddProjectController<TConfiguration, TNavigation>(this IWebBuilder builder, IMvcBuilder mvcBuilder, Assembly razorAssembly)
            where TConfiguration : ProjectConfigurationBase
            where TNavigation : ProjectNavigationWithController
        {
            builder.AddProjectCore<TConfiguration, TNavigation>();

            if (!builder.Services.TryGetAll<IProjectNavigation, RootProjectNavigationWithController>(out _))
                builder.Services.AddSingleton<IProjectNavigation, RootProjectNavigationWithController>();

            var assemblies = ThemepackHelper.ThemepackInfos.Values.Select(info => info.Assembly)
                .Append(razorAssembly)
                .ToArray();

            AddRazorRelatedParts(mvcBuilder, assemblies);

            return builder;
        }

        /// <summary>
        /// 添加区域页面。
        /// </summary>
        /// <typeparam name="TConfiguration">指定的配置类型。</typeparam>
        /// <typeparam name="TNavigation">指定的导航类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <param name="mvcBuilder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <param name="razorAssembly">给定包含页面集合的 <see cref="Assembly"/>。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IWebBuilder AddProjectPage<TConfiguration, TNavigation>(this IWebBuilder builder, IMvcBuilder mvcBuilder, Assembly razorAssembly)
            where TConfiguration : ProjectConfigurationBaseWithRazorPages
            where TNavigation : ProjectNavigationWithPage
        {
            builder.AddProjectCore<TConfiguration, TNavigation>();

            if (!builder.Services.TryGetAll<IProjectNavigation, RootProjectNavigationWithPage>(out _))
                builder.Services.AddSingleton<IProjectNavigation, RootProjectNavigationWithPage>();

            var assemblies = ThemepackHelper.ThemepackInfos.Values.Select(info => info.Assembly)
                .Append(razorAssembly)
                .ToArray();
            
            AddRazorRelatedParts(mvcBuilder, assemblies);

            return builder;
        }

        private static IMvcBuilder AddRazorRelatedParts(IMvcBuilder builder, params Assembly[] assemblies)
        {
            assemblies.NotEmpty(nameof(assemblies));

            // For preview1, we don't have a good mechanism to plug in additional parts.
            // We need to provide API surface to allow libraries to plug in existing parts
            // that were optionally added.
            // Challenges here are:
            // * Discovery of the parts.
            // * Ordering of the parts.
            // * Loading of the assembly in memory.

            builder.ConfigureApplicationPartManager(manager =>
            {
                foreach (var assem in assemblies)
                {
                    var relatedAssemblies = RelatedAssemblyAttribute.GetRelatedAssemblies(assem, throwOnError: true);
                    var relatedParts = relatedAssemblies.SelectMany(CompiledRazorAssemblyApplicationPartFactory.GetDefaultApplicationParts);

                    if (relatedParts.Any())
                    {
                        foreach (var part in relatedParts)
                            manager.ApplicationParts.Add(part);
                    }
                }
            });

            return builder;
        }

        //private static IWebBuilder AddWithUserControllers(this IWebBuilder builder)
        //{
        //    if (builder.UserType.IsNotNull()
        //        && builder.Services.TryGet<ApplicationPartManager>(out ServiceDescriptor serviceDescriptor)
        //        && serviceDescriptor.ImplementationInstance.IsNotNull())
        //    {
        //        var manager = serviceDescriptor.ImplementationInstance as ApplicationPartManager;
        //        if (!manager.FeatureProviders.OfType<GenericControllerModelProviderWithUser>().Any())
        //        {
        //            manager.FeatureProviders.Add(new GenericControllerModelProviderWithUser(builder.UserType));
        //        }
        //    }

        //    return builder;
        //}

        #endregion

    }
}
