#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Razor.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Librame.AspNetCore.Web.Themepacks
{
    using Extensions;

    /// <summary>
    /// 程序集布局提供程序。
    /// </summary>
    public class AssemblyLayoutProvider : ILayoutProvider
    {
        private const string _defaultPathPattern
            = @"/Views/Shared/(\w+)/_Layout.cshtml";

        // 匹配的分组值从索引 1 开始，索引 0 为 input
        private readonly Func<GroupCollection, string> _defaultNameSelector
            = groups => groups[1].Value;

        private readonly Assembly _assembly;


        /// <summary>
        /// 构造一个 <see cref="AssemblyLayoutProvider"/>。
        /// </summary>
        /// <param name="assembly">给定的 <see cref="Assembly"/>。</param>
        public AssemblyLayoutProvider(Assembly assembly)
        {
            _assembly = assembly.NotNull(nameof(assembly));
        }


        /// <summary>
        /// 获取布局集合。
        /// </summary>
        /// <param name="pathPattern">给定布局查找的路径模式（可选）。</param>
        /// <param name="nameSelector">给定符合布局查找路径模式的名称选择器（可选）。</param>
        /// <returns>返回包含布局名称与路径的字典集合。</returns>
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递")]
        public Dictionary<string, string> GetLayouts(string pathPattern = null,
            Func<GroupCollection, string> nameSelector = null)
        {
            // 获取已编译的视图关联程序集
            var relatedAssemblies = RelatedAssemblyAttribute.GetRelatedAssemblies(_assembly, throwOnError: true);
            if (!relatedAssemblies.Any())
                throw new ArgumentException($"No related assemblies '{_assembly.GetDisplayName()}' of compiled razor assembly found.");

            var layouts = new Dictionary<string, string>();
            var regex = new Regex(pathPattern ?? _defaultPathPattern);

            if (nameSelector.IsNull())
                nameSelector = _defaultNameSelector;

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
                        var name = nameSelector.Invoke(match.Groups);
                        layouts.Add(name, id);
                    }
                }
            });

            if (layouts.IsEmpty())
                throw new ArgumentException($"No available layouts '{_defaultPathPattern}' is loaded.");

            return layouts;
        }

    }
}
