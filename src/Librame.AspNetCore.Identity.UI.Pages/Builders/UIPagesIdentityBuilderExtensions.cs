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

namespace Librame.Builders
{
    using AspNetCore.Identity;
    using AspNetCore.Identity.UI.Pages;

    /// <summary>
    /// UI 页集合身份构建器静态扩展。
    /// </summary>
    public static class UIPagesIdentityBuilderExtensions
    {

        /// <summary>
        /// 添加 UI 页集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityBuilder"/>。</param>
        /// <param name="mvcBuilder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AddUIPages(this IIdentityBuilder builder, IMvcBuilder mvcBuilder)
        {
            AddRelatedParts(mvcBuilder);

            var configureType = typeof(InternalUIIdentityOptions<>).MakeGenericType(builder.Core.UserType);
            builder.Services.ConfigureOptions(configureType);

            return builder;
        }

        private static void AddRelatedParts(IMvcBuilder mvcBuilder)
        {
            // For preview1, we don't have a good mechanism to plug in additional parts.
            // We need to provide API surface to allow libraries to plug in existing parts
            // that were optionally added.
            // Challenges here are:
            // * Discovery of the parts.
            // * Ordering of the parts.
            // * Loading of the assembly in memory.

            mvcBuilder.ConfigureApplicationPartManager(partManager =>
            {
                var thisAssembly = typeof(UIPagesIdentityBuilderExtensions).Assembly;
                var relatedAssemblies = RelatedAssemblyAttribute.GetRelatedAssemblies(thisAssembly, throwOnError: true);
                var relatedParts = relatedAssemblies.SelectMany(CompiledRazorAssemblyApplicationPartFactory.GetDefaultApplicationParts);

                foreach (var part in relatedParts)
                {
                    partManager.ApplicationParts.Add(part);
                }
            });
        }

    }
}
