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

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 主题包描述符。
    /// </summary>
    public class ThemepackDescriptor
    {
        /// <summary>
        /// 构造一个 <see cref="ThemepackDescriptor"/> 实例。
        /// </summary>
        /// <param name="directory">给定的目录（可选；默认为当前应用程序域目录）。</param>
        /// <param name="filterPattern">给定的过滤规则（可选；如“*.dll”）。</param>
        /// <param name="filterOption">给定的过滤选项（可选；默认为当前目录）。</param>
        /// <param name="namedPattern">给定的命名规则（可选；用于匹配文件名的正则表达式）。</param>
        /// <param name="baseInfoType">给定的基础信息类型（可选；要求从 <see cref="IThemepackInfo"/> 派生）。</param>
        public ThemepackDescriptor(string directory = null, string filterPattern = null,
            SearchOption filterOption = SearchOption.TopDirectoryOnly,
            string namedPattern = null, Type baseInfoType = null)
        {
            Directory = directory ?? AppDomain.CurrentDomain.BaseDirectory;
            FilterPattern = filterPattern ?? "*.dll";
            FilterOption = filterOption;
            NamedPattern = namedPattern ?? @"(Librame.AspNetCore.UI.)(\w+)(.dll)";

            var interfaceInfoType = typeof(IThemepackInfo);
            if (baseInfoType.IsNotNull())
            {
                if (!interfaceInfoType.IsAssignableFrom(baseInfoType))
                    throw new ArgumentException($"The \"{interfaceInfoType.ToString()}\" is not assignable from \"{baseInfoType.ToString()}\".");

                BaseInfoType = baseInfoType;
            }
            else
            {
                BaseInfoType = interfaceInfoType;
            }
        }


        /// <summary>
        /// 目录。
        /// </summary>
        public string Directory { get; }

        /// <summary>
        /// 过滤规则。
        /// </summary>
        public string FilterPattern { get; }

        /// <summary>
        /// 过滤选项。
        /// </summary>
        public SearchOption FilterOption { get; }

        /// <summary>
        /// 命名规则（用于正则表达式）。
        /// </summary>
        public string NamedPattern { get; }

        /// <summary>
        /// 基础信息类型。
        /// </summary>
        public Type BaseInfoType { get; }


        /// <summary>
        /// 当前信息实例列表。
        /// </summary>
        public IList<IThemepackInfo> CurrentInfos { get; set; }
            = new List<IThemepackInfo>();
    }
}
