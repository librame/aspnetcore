#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.UI
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 主题包注册。
    /// </summary>
    public static class ThemepackGlobalization
    {
        static ThemepackGlobalization()
        {
            CurrentInfos = CurrentInfos.EnsureSingleton(() =>
            {
                var infos = new List<IThemepackInfo>();
                var interfaceType = typeof(IThemepackInfo);

                BuilderGlobalization.RegisterTypes(type =>
                {
                    infos.Add((IThemepackInfo)type.EnsureCreate());
                },
                types => types
                    .Where(type => interfaceType.IsAssignableFrom(type) && type.IsConcreteType()),
                assems => assems
                    .Where(assem => assem.GetName().Name.StartsWith("Librame.AspNetCore.UI.")).ToArray());

                return infos;
            });
        }


        /// <summary>
        /// 当前主题包信息列表。
        /// </summary>
        public static IList<IThemepackInfo> CurrentInfos { get; }


        /// <summary>
        /// 获取主题包信息。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回 <see cref="IThemepackInfo"/>。</returns>
        public static IThemepackInfo GetInfo(string name = null)
        {
            return name.IsNullOrEmpty() ? CurrentInfos.First()
                : CurrentInfos.Single(info => info.Name == name);
        }
    }
}
