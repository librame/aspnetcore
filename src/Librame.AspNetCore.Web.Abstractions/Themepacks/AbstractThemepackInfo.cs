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
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Librame.AspNetCore.Web.Themepacks
{
    using AspNetCore.Applications;
    using Extensions;

    /// <summary>
    /// 抽象主题包信息。
    /// </summary>
    public abstract class AbstractThemepackInfo : AbstractApplicationInfo, IThemepackInfo
    {
        /// <summary>
        /// 公共布局键。
        /// </summary>
        public const string CommonLayoutKey = "Common";

        /// <summary>
        /// 登入布局键。
        /// </summary>
        public const string LoginLayoutKey = "Login";

        /// <summary>
        /// 管理布局键。
        /// </summary>
        public const string ManageLayoutKey = "Manage";

        private const string _searchLayoutPathPattern
            = @"/Views/Shared/(\w+)/_Layout.cshtml";

        // 匹配的分组值从索引 1 开始，索引 0 为 input
        private static Func<GroupCollection, string> _patternNameFactory
            = groups => groups[1].Value;


        /// <summary>
        /// 构造一个 <see cref="AbstractThemepackInfo"/>。
        /// </summary>
        protected AbstractThemepackInfo()
            : base()
        {
            Layouts = SearchAssembliesLayouts();

            CommonLayout = Layouts[CommonLayoutKey];
            LoginLayout = Layouts[LoginLayoutKey];
            ManageLayout = Layouts[ManageLayoutKey];
        }


        /// <summary>
        /// 布局字典集合。
        /// </summary>
        public IReadOnlyDictionary<string, string> Layouts { get; }

        /// <summary>
        /// 公共布局。
        /// </summary>
        public string CommonLayout { get; }

        /// <summary>
        /// 登入布局。
        /// </summary>
        public string LoginLayout { get; }

        /// <summary>
        /// 管理布局。
        /// </summary>
        public string ManageLayout { get; }


        private Dictionary<string, string> SearchAssembliesLayouts()
        {
            // 获取已编译的视图关联程序集
            var relatedAssemblies = RelatedAssemblyAttribute.GetRelatedAssemblies(Assembly, throwOnError: true);
            if (!relatedAssemblies.Any())
                throw new ArgumentException($"No related assemblies '{Assembly.GetDisplayName()}' of compiled razor assembly found.");

            var layouts = new Dictionary<string, string>();
            var regex = new Regex(_searchLayoutPathPattern);

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
                        var name = _patternNameFactory.Invoke(match.Groups);
                        layouts.Add(name, id);
                    }
                }
            });

            if (layouts.IsEmpty())
                throw new ArgumentException($"No available layouts '{_searchLayoutPathPattern}' is loaded.");

            return layouts;
        }


        /// <summary>
        /// 获取静态文件提供程序。
        /// </summary>
        /// <returns>返回 <see cref="IFileProvider"/>。</returns>
        public abstract IFileProvider GetStaticFileProvider();
    }
}
