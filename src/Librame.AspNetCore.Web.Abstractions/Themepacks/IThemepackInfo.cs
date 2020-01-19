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
        /// 布局字典集合。
        /// </summary>
        IReadOnlyDictionary<string, string> Layouts { get; }

        /// <summary>
        /// 公共布局。
        /// </summary>
        string CommonLayout { get; }

        /// <summary>
        /// 登入布局。
        /// </summary>
        string LoginLayout { get; }

        /// <summary>
        /// 管理布局。
        /// </summary>
        string ManageLayout { get; }


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
