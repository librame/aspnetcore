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
using System.Reflection;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 主题包信息。
    /// </summary>
    public class ThemepackInfo : AbstractApplicationInfo, IThemepackInfo
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public override string Name => "Simple";

        /// <summary>
        /// 标题。
        /// </summary>
        public override string Title => "简约";

        /// <summary>
        /// 联系。
        /// </summary>
        public override string Contact => "https://github.com/librame";

        /// <summary>
        /// 版权。
        /// </summary>
        public override string Copyright => "Librame Pang";

        /// <summary>
        /// 版本。
        /// </summary>
        public override string Version => "1.0.0";

        /// <summary>
        /// 程序集。
        /// </summary>
        public override Assembly Assembly => GetType().Assembly;


        /// <summary>
        /// 作者。
        /// </summary>
        public string Author => nameof(Librame);


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
