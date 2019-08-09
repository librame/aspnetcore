#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.FileProviders;
using System;
using System.Reflection;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 简约主题包信息。
    /// </summary>
    public class SimpleThemepackInfo : IThemepackInfo
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name
            => "Simple";

        /// <summary>
        /// 标题。
        /// </summary>
        public string Title
            => "简约";

        /// <summary>
        /// 作者。
        /// </summary>
        public string Author
            => nameof(Librame);

        /// <summary>
        /// 联系。
        /// </summary>
        public string Contact
            => "https://github.com/librame";

        /// <summary>
        /// 版权。
        /// </summary>
        public string Copyright
            => "Librame Pang";

        /// <summary>
        /// 版本。
        /// </summary>
        public string Version
            => "1.0.0";

        /// <summary>
        /// 程序集。
        /// </summary>
        public Assembly Assembly
            => GetType().Assembly;

        /// <summary>
        /// 程序集版本。
        /// </summary>
        public Version AssemblyVersion
            => Assembly.GetName().Version;


        /// <summary>
        /// 获取静态文件提供程序。
        /// </summary>
        /// <remarks>
        /// 通常是包含“wwwroot”静态资源文件的嵌入程序集。
        /// </remarks>
        /// <returns>返回 <see cref="IFileProvider"/>。</returns>
        public IFileProvider GetStaticFileProvider()
        {
            return new ManifestEmbeddedFileProvider(Assembly, "wwwroot");
        }

    }
}
