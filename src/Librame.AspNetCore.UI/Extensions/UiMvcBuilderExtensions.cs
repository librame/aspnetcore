#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="IMvcBuilder"/> 静态扩展。
    /// </summary>
    public static class UiMvcBuilderExtensions
    {
        /// <summary>
        /// 添加 Razor 关系部分页面。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <param name="assemblies">给定的程序集数组。</param>
        /// <returns>返回 <see cref="IMvcBuilder"/>。</returns>
        public static IMvcBuilder AddRazorRelatedParts(this IMvcBuilder builder, params Assembly[] assemblies)
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

    }
}
