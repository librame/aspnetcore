#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;

namespace Librame.AspNetCore.Web.Themepacks
{
    using AspNetCore.Applications;

    /// <summary>
    /// 主题包信息接口。
    /// </summary>
    public interface IThemepackInfo : IApplicationInfo
    {
        /// <summary>
        /// 布局路径集合。
        /// </summary>
        /// <value>返回包含布局名称与路径的字典集合。</value>
        IReadOnlyDictionary<string, string> LayoutPaths { get; }

        /// <summary>
        /// 公共布局路径。
        /// </summary>
        string CommonLayoutPath { get; }

        /// <summary>
        /// 登入布局路径。
        /// </summary>
        string LoginLayoutPath { get; }

        /// <summary>
        /// 管理布局路径。
        /// </summary>
        string ManageLayoutPath { get; }


        /// <summary>
        /// 获取布局提供程序。
        /// </summary>
        /// <returns>返回 <see cref="ILayoutProvider"/>。</returns>
        ILayoutProvider GetLayoutProvider();

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
