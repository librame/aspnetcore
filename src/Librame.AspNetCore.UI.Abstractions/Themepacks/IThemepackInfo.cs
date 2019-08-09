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

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 主题包信息接口。
    /// </summary>
    public interface IThemepackInfo : IApplicationInfo
    {
        /// <summary>
        /// 获取静态文件提供程序。
        /// </summary>
        /// <remarks>
        /// 通常是包含“wwwroot”静态资源文件的嵌入程序集。
        /// </remarks>
        /// <returns>返回 <see cref="IFileProvider"/>。</returns>
        IFileProvider GetStaticFileProvider();
    }
}
