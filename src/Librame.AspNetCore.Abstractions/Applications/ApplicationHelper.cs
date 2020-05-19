#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Librame.AspNetCore.Applications
{
    using Extensions;
    using Extensions.Core.Utilities;

    /// <summary>
    /// 应用助手。
    /// </summary>
    public static class ApplicationHelper
    {
        /// <summary>
        /// 从程序集集合中获取指定的应用信息字典集合。
        /// </summary>
        /// <typeparam name="TInfo">指定的应用信息类型。</typeparam>
        /// <param name="searchPatterns">给定的查找模式集合。</param>
        /// <param name="createInfoFactory">给定创建信息实例的工厂方法。</param>
        /// <param name="allAssemblies">给定的所有程序集（可选；默认使用 <see cref="AssemblyUtility.CurrentAssembliesWithoutSystem"/>）。</param>
        /// <returns>返回加载的字典集合。</returns>
        public static Dictionary<string, TInfo> GetApplicationInfos<TInfo>(IEnumerable<string> searchPatterns,
            Func<Type, TInfo> createInfoFactory, IEnumerable<Assembly> allAssemblies = null)
            where TInfo : IApplicationInfo
        {
            createInfoFactory.NotNull(nameof(createInfoFactory));

            var appInfoAssemblies = SearchAssemblies(searchPatterns, allAssemblies);

            var appInfoType = typeof(TInfo);
            if (appInfoAssemblies.IsEmpty())
                throw new Exception($"Available application information '{appInfoType.FullName}' that matches the assembly name patterns '{searchPatterns}' could not be found.");

            var infos = new Dictionary<string, TInfo>();
            appInfoAssemblies.InvokeTypes(type =>
            {
                var info = createInfoFactory.Invoke(type);
                infos.Add(info.Name, info);
            },
            types => types.Where(type => appInfoType.IsAssignableFrom(type) && type.IsConcreteType()));

            return infos;
        }


        /// <summary>
        /// 查找符合指定模式列表的程序集集合。
        /// </summary>
        /// <param name="searchPatterns">给定的查找模式集合。</param>
        /// <param name="allAssemblies">给定的所有程序集（可选；默认使用 <see cref="AssemblyUtility.CurrentAssembliesWithoutSystem"/>）。</param>
        /// <returns>返回 <see cref="List{Assembly}"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static List<Assembly> SearchAssemblies(IEnumerable<string> searchPatterns, IEnumerable<Assembly> allAssemblies = null)
        {
            searchPatterns.NotEmpty(nameof(searchPatterns));

            if (allAssemblies.IsNull())
                allAssemblies = AssemblyUtility.CurrentAssembliesWithoutSystem;

            var resultAssemblies = new List<Assembly>();

            foreach (var pattern in searchPatterns)
            {
                var regex = new Regex(pattern);

                var filterAssemblies = allAssemblies
                    .Where(assembly => regex.IsMatch(assembly.GetDisplayName()));

                resultAssemblies.AddRange(filterAssemblies);
            }

            return resultAssemblies;
        }

    }
}
