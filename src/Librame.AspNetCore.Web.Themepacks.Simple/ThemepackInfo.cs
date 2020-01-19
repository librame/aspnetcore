﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;

namespace Librame.AspNetCore.Web.Themepacks.Simple
{
    /// <summary>
    /// 主题包信息。
    /// </summary>
    public class ThemepackInfo : AbstractThemepackInfo
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public override string Name
            => nameof(Simple);

        /// <summary>
        /// 本地化定位器。
        /// </summary>
        public override IStringLocalizer Localizer
            => new ThemepackExpressionLocalizer<ThemepackInfoResource>();


        /// <summary>
        /// 获取静态文件提供程序。
        /// </summary>
        /// <remarks>
        /// 通常是包含“wwwroot”静态资源文件的嵌入程序集。
        /// </remarks>
        /// <returns>返回 <see cref="IFileProvider"/>。</returns>
        public override IFileProvider GetStaticFileProvider()
            => new ManifestEmbeddedFileProvider(Assembly, "wwwroot");
    }
}