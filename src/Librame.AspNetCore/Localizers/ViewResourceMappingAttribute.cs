//#region License

/////* **************************************************************************************
// * Copyright (c) Librame Pang All rights reserved.
// * 
// * http://librame.net
// * 
// * You must not remove this notice, or any other, from this software.
// * **************************************************************************************/

//#endregion

//using Microsoft.Extensions.Localization;
//using System;
//using System.Globalization;
//using System.IO;
//using System.Reflection;

//namespace Librame.AspNetCore
//{
//    using Extensions;

//    /// <summary>
//    /// 视图资源映射特性。
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
//    public class ViewResourceMappingAttribute : Attribute
//    {
//        /// <summary>
//        /// 构造一个 <see cref="ViewResourceMappingAttribute"/> 实例。
//        /// </summary>
//        /// <param name="viewName">给定的视图名称。</param>
//        public ViewResourceMappingAttribute(string viewName)
//        {
//            ViewName = viewName.NotEmpty(nameof(viewName));
//        }


//        /// <summary>
//        /// 视图名称。
//        /// </summary>
//        public string ViewName { get; }

//        /// <summary>
//        /// 视图位置。
//        /// </summary>
//        public string ViewLocation { get; set; }


//        /// <summary>
//        /// 获取资源基础名称。
//        /// </summary>
//        /// <param name="viewResourceType">给定的视图资源类型。</param>
//        /// <returns>返回字符串。</returns>
//        public virtual string GetResourceBaseName(Type viewResourceType)
//        {
//            viewResourceType.NotNull(nameof(viewResourceType));

//            if (ViewLocation.IsEmpty())
//                return $"{viewResourceType.Namespace}.{ViewName}";

//            if (ViewLocation.Contains(Path.AltDirectorySeparatorChar.ToString(CultureInfo.CurrentCulture), StringComparison.OrdinalIgnoreCase) ||
//                ViewLocation.Contains(Path.DirectorySeparatorChar.ToString(CultureInfo.CurrentCulture), StringComparison.OrdinalIgnoreCase))
//            {
//                // 格式化为点分隔符（如：Resources.）
//                ViewLocation = ViewLocation
//                    .Replace(Path.AltDirectorySeparatorChar, '.')
//                    .Replace(Path.DirectorySeparatorChar, '.') + ".";
//            }

//            var rootNamespace = GetRootNamespace(viewResourceType);
//            return $"{rootNamespace}.{ViewLocation}{viewResourceType.Name}";
//        }

//        /// <summary>
//        /// 获取根命名空间。
//        /// </summary>
//        /// <param name="sourceViewType">给定的源视图类型。</param>
//        /// <returns>返回字符串。</returns>
//        protected virtual string GetRootNamespace(Type sourceViewType)
//        {
//            sourceViewType.NotNull(nameof(sourceViewType));

//            if (sourceViewType.Assembly.TryGetCustomAttribute(out RootNamespaceAttribute rootNamespace))
//                return rootNamespace.RootNamespace;

//            if (sourceViewType.Assembly.TryGetCustomAttribute(out AbstractionRootNamespaceAttribute abstractionRootNamespace))
//                return abstractionRootNamespace.RootNamespace;

//            return new AssemblyName(sourceViewType.Assembly.FullName).Name;
//        }

//    }
//}
