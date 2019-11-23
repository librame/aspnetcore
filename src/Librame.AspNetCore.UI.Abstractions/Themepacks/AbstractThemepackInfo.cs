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
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 抽象主题包信息。
    /// </summary>
    public abstract class AbstractThemepackInfo : AbstractApplicationInfo, IThemepackInfo
    {
        private ConcurrentDictionary<string, string> _layouts = null;


        /// <summary>
        /// 获取布局。
        /// </summary>
        /// <param name="name">指定的名称（可选；通常为 Common、Login、Manage 等）。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Microsoft.Design", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
        public virtual string GetLayout(string name = null)
        {
            if (_layouts.IsNull())
            {
                // "/Views/Shared/Common/_Layout.cshtml"（匹配的分组值从索引 1 开始，索引 0 为 input）
                _layouts = GetLayouts(Assembly, @"/Views/Shared/(\w+)/_Layout.cshtml", groups => groups[1].Value);
                if (_layouts.IsEmpty())
                    throw new ArgumentException($"No available layouts is loaded.");
            }

            return _layouts[name.NotEmptyOrDefault("Common")];
        }

        /// <summary>
        /// 获取布局字典集合。
        /// </summary>
        /// <param name="themepackAssembly">给定的主题包程序集。</param>
        /// <param name="pattern">给定的布局模式。</param>
        /// <param name="nameFactory">给定提取名称的工厂方法。</param>
        /// <returns>返回 <see cref="ConcurrentDictionary{TKey, TValue}"/>。</returns>
        protected static ConcurrentDictionary<string, string> GetLayouts(Assembly themepackAssembly, string pattern,
            Func<GroupCollection, string> nameFactory)
        {
            nameFactory.NotNull(nameof(nameFactory));

            // 获取已编译的视图关联程序集
            var relatedAssemblies = RelatedAssemblyAttribute.GetRelatedAssemblies(themepackAssembly, throwOnError: true);
            if (!relatedAssemblies.Any())
                throw new ArgumentException($"No related assemblies '{themepackAssembly.GetSimpleName()}' of compiled razor assembly found.");

            var regex = new Regex(pattern);
            var layouts = new ConcurrentDictionary<string, string>();

            relatedAssemblies.ForEach(assembly =>
            {
                var allAttributes = new List<RazorSourceChecksumAttribute>();

                foreach (var type in assembly.DefinedTypes.Where(p => p.IsDefined<RazorSourceChecksumAttribute>()))
                {
                    if (type.TryGetCustomAttributes(out IEnumerable<RazorSourceChecksumAttribute> attributes))
                        allAttributes.AddRange(attributes);
                }

                foreach (var id in allAttributes.Select(attrib => attrib.Identifier).Distinct())
                {
                    var match = regex.Match(id);
                    if (match.Success)
                    {
                        var name = nameFactory.Invoke(match.Groups);
                        layouts.AddOrUpdate(name, value => id, (key, value) => id);
                    }
                }
            });

            return layouts;
        }


        /// <summary>
        /// 获取静态文件提供程序。
        /// </summary>
        /// <returns>返回 <see cref="IFileProvider"/>。</returns>
        public abstract IFileProvider GetStaticFileProvider();
    }
}
