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

namespace Librame.AspNetCore.Web.Themepacks
{
    using AspNetCore.Applications;
    using Extensions;

    /// <summary>
    /// 主题包助手。
    /// </summary>
    public static class ThemepackHelper
    {
        /// <summary>
        /// 主题包信息字典。
        /// </summary>
        public static IReadOnlyDictionary<string, IThemepackInfo> ThemepackInfos
            => ApplicationHelper.GetApplicationInfos(ThemepackAssemblyPatternRegistration.All,
                type => type.EnsureCreate<IThemepackInfo>()); // 此创建方法要求类型可公共访问

    }
}
