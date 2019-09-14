#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 应用 UI 构建器静态扩展。
    /// </summary>
    public static class ApplicationUiBuilderExtensions
    {
        /// <summary>
        /// 添加应用集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUiBuilder"/>。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        internal static IUiBuilder AddApplications(this IUiBuilder builder)
        {
            builder.Services.AddSingleton<IApplicationContext, ApplicationContext>();
            builder.Services.AddSingleton<IApplicationPrincipal, ApplicationPrincipal>();

            return builder;
        }


        #region AddSite

        /// <summary>
        /// 添加站点。
        /// </summary>
        /// <typeparam name="TSite">指定的站点类型。</typeparam>
        /// <typeparam name="TConfiguration">指定的站点配置类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IUiBuilder"/>。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddSite<TSite, TConfiguration>(this IUiBuilder builder)
            where TSite : class, IApplicationSite
            where TConfiguration : ApplicationSiteConfiguration
        {
            builder.Services.AddSingleton<IApplicationSite, TSite>();
            builder.Services.ConfigureOptions(typeof(TConfiguration));

            return builder;
        }

        /// <summary>
        /// 添加站点控制器集合。
        /// </summary>
        /// <typeparam name="TSite">指定的站点类型。</typeparam>
        /// <typeparam name="TConfiguration">指定的站点配置类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IUiBuilder"/>。</param>
        /// <param name="mvcBuilder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <param name="razorAssembly">给定包含页面集合的 <see cref="Assembly"/>。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddSiteControllers<TSite, TConfiguration>(this IUiBuilder builder, IMvcBuilder mvcBuilder, Assembly razorAssembly)
            where TSite : class, IApplicationSite
            where TConfiguration : ApplicationSiteConfiguration
        {
            builder.AddSite<TSite, TConfiguration>();

            // Add Assemblies Views（需引用 Microsoft.AspNetCore.Mvc 程序集才能正常被路由解析）
            var assemblies = ApplicationInfoHelper.Themepacks.Values.Select(info => info.Assembly)
                .Append(razorAssembly)
                .ToArray();

            AddRazorRelatedParts(mvcBuilder, assemblies);

            return builder.AddWithUserControllers();
        }

        /// <summary>
        /// 添加站点页面集合。
        /// </summary>
        /// <typeparam name="TSite">指定的站点类型。</typeparam>
        /// <typeparam name="TConfiguration">指定的站点配置类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IUiBuilder"/>。</param>
        /// <param name="mvcBuilder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <param name="razorAssembly">给定包含页面集合的 <see cref="Assembly"/>。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddSitePages<TSite, TConfiguration>(this IUiBuilder builder, IMvcBuilder mvcBuilder, Assembly razorAssembly)
            where TSite : class, IApplicationSite
            where TConfiguration : ApplicationSiteConfiguration
        {
            builder.AddSite<TSite, TConfiguration>();

            // Add Assemblies Pages（需引用 Microsoft.AspNetCore.Mvc 程序集才能正常被路由解析）
            var assemblies = ApplicationInfoHelper.Themepacks.Values.Select(info => info.Assembly)
                .Append(razorAssembly)
                .ToArray();

            AddRazorRelatedParts(mvcBuilder, assemblies);

            return builder;
        }


        private static IUiBuilder AddWithUserControllers(this IUiBuilder builder)
        {
            if (builder.UserType.IsNotNull()
                && builder.Services.TryGet<ApplicationPartManager>(out ServiceDescriptor serviceDescriptor)
                && serviceDescriptor.ImplementationInstance.IsNotNull())
            {
                var manager = serviceDescriptor.ImplementationInstance as ApplicationPartManager;
                if (!manager.FeatureProviders.OfType<ApplicationSiteTemplateWithUserControllerProvider>().Any())
                {
                    manager.FeatureProviders.Add(new ApplicationSiteTemplateWithUserControllerProvider(builder.UserType));
                }
            }

            return builder;
        }

        private static IMvcBuilder AddRazorRelatedParts(IMvcBuilder builder, params Assembly[] assemblies)
        {
            assemblies.NotNullOrEmpty(nameof(assemblies));

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

        #endregion

    }
}
