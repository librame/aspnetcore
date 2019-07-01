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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 主题包助手。
    /// </summary>
    public class ThemepackHelper
    {
        /// <summary>
        /// 当前信息列表。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IList{IThemepackInfo}"/> 列表。
        /// </value>
        public static IList<IThemepackInfo> CurrentInfos
        {
            get
            {
                var descriptors = new List<ThemepackDescriptor>()
                {
                    new ThemepackDescriptor()
                };

                MatchAssemblies(descriptors);

                return descriptors.First().CurrentInfos;
            }
        }


        /// <summary>
        /// 获取指定名称的信息。
        /// </summary>
        /// <param name="name">给定的主题包名称。</param>
        /// <returns>返回 <see cref="IThemepackInfo"/>。</returns>
        public static IThemepackInfo GetInfo(string name)
        {
            if (name.IsNullOrEmpty()) return CurrentInfos.First();

            return CurrentInfos.First(info => info.Name == name);
        }

        /// <summary>
        /// 获取指定名称信息的程序集。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回程序集或 NULL。</returns>
        public static Assembly GetInfoAssembly(string name = null)
        {
            return GetInfo(name).Assembly;
        }


        /// <summary>
        /// 匹配程序集集合。
        /// </summary>
        /// <param name="descriptors">给定的 <see cref="IEnumerable{ThemepackDescriptor}"/>。</param>
        public static void MatchAssemblies(IEnumerable<ThemepackDescriptor> descriptors)
        {
            descriptors.NotNullOrEmpty(nameof(descriptors));

            foreach (var descr in descriptors)
            {
                try
                {
                    var regex = new Regex(descr.NamedPattern);
                    var assemblies = Directory.GetFiles(descr.Directory,
                        descr.FilterPattern, descr.FilterOption).Where(file =>
                    {
                        var fileName = Path.GetFileName(file);
                        return regex.IsMatch(fileName);
                    });

                    if (!assemblies.Any()) continue;

                    // 绑定信息列表
                    descr.CurrentInfos = assemblies.Select(assem =>
                    {
                        // 限制单个程序集只能存在一个信息类
                        var infoType = Assembly.LoadFrom(assem).GetTypes().FirstOrDefault(type =>
                        {
                            return !type.IsInterface && !type.IsAbstract;
                        });

                        return Activator.CreateInstance(infoType) as IThemepackInfo;
                    })
                    .ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
