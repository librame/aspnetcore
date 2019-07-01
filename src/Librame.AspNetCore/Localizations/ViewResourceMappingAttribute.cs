#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;
using System;
using System.IO;
using System.Reflection;

namespace Librame.AspNetCore
{
    using Extensions;

    /// <summary>
    /// 视图资源映射特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ViewResourceMappingAttribute : Attribute
    {
        /// <summary>
        /// 构造一个 <see cref="ViewResourceMappingAttribute"/> 实例。
        /// </summary>
        /// <param name="viewName">给定的视图名称。</param>
        public ViewResourceMappingAttribute(string viewName)
        {
            ViewName = viewName.NotNullOrEmpty(nameof(viewName));
        }


        /// <summary>
        /// 视图名称。
        /// </summary>
        public string ViewName { get; }

        /// <summary>
        /// 视图位置。
        /// </summary>
        public string ViewLocation { get; set; }


        /// <summary>
        /// 获取资源基础名称。
        /// </summary>
        /// <param name="sourceViewType">给定的源视图类型。</param>
        /// <returns>返回字符串。</returns>
        public virtual string GetResourceBaseName(Type sourceViewType)
        {
            if (ViewLocation.IsNullOrEmpty())
                return $"{sourceViewType.Namespace}.{ViewName}";

            if (ViewLocation.Contains(Path.AltDirectorySeparatorChar.ToString()) ||
                ViewLocation.Contains(Path.DirectorySeparatorChar.ToString()))
            {
                ViewLocation = ViewLocation.Replace(Path.AltDirectorySeparatorChar, '.')
                    .Replace(Path.DirectorySeparatorChar, '.') + ".";
            }

            var baseNamespace = GetBaseNamespace(sourceViewType);
            return $"{baseNamespace}.{ViewLocation}{sourceViewType.Name}"; // 已格式化为点分隔符（如：Resources.）
        }

        /// <summary>
        /// 获取基础命名空间。
        /// </summary>
        /// <param name="sourceViewType">给定的源视图类型。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string GetBaseNamespace(Type sourceViewType)
        {
            if (sourceViewType.Assembly.TryGetCustomAttribute(out RootNamespaceAttribute rootNamespaceAttribute))
                return rootNamespaceAttribute.RootNamespace;

            return new AssemblyName(sourceViewType.Assembly.FullName).Name;
        }

    }
}
