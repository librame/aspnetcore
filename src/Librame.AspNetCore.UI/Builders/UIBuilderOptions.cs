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

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    /// <summary>
    /// UI 构建器选项。
    /// </summary>
    public class UIBuilderOptions : AbstractBuilderOptions
    {
        /// <summary>
        /// 应用程序上下文类型（推荐从 <see cref="AbstractApplicationContext"/> 派生）。
        /// </summary>
        public Type ApplicationContextType { get; set; }

        /// <summary>
        /// 应用程序后置配置选项类型（推荐从 <see cref="ApplicationPostConfigureOptionsBase"/> 派生）。
        /// </summary>
        public Type ApplicationPostConfigureOptionsType { get; set; }


        /// <summary>
        /// 主题选项。
        /// </summary>
        public ThemepackOptions Themepacks { get; set; }
            = new ThemepackOptions();
    }


    /// <summary>
    /// 主题选项。
    /// </summary>
    public class ThemepackOptions
    {
        /// <summary>
        /// 默认主题信息。
        /// </summary>
        public IThemepackInfo DefaultInfo { get; set; }
    }
}
