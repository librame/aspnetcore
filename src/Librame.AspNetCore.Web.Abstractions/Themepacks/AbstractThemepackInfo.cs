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
    /// 抽象主题包信息。
    /// </summary>
    public abstract class AbstractThemepackInfo : AbstractApplicationInfo, IThemepackInfo
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractThemepackInfo"/>。
        /// </summary>
        protected AbstractThemepackInfo()
            : base()
        {
        }


        /// <summary>
        /// 布局路径集合。
        /// </summary>
        /// <value>返回包含布局名称与路径的字典集合。</value>
        public virtual IReadOnlyDictionary<string, string> LayoutPaths
            => GetLayoutProvider().GetLayouts();


        /// <summary>
        /// 公共布局路径。
        /// </summary>
        public virtual string CommonLayoutPath
            => LayoutPaths[LayoutFixedKeys.Common];

        /// <summary>
        /// 登入布局路径。
        /// </summary>
        public virtual string LoginLayoutPath
            => LayoutPaths[LayoutFixedKeys.Login];

        /// <summary>
        /// 管理布局路径。
        /// </summary>
        public virtual string ManageLayoutPath
            => LayoutPaths[LayoutFixedKeys.Manage];


        /// <summary>
        /// 获取布局提供程序。
        /// </summary>
        /// <returns>返回 <see cref="ILayoutProvider"/>。</returns>
        public abstract ILayoutProvider GetLayoutProvider();

        /// <summary>
        /// 获取静态文件提供程序。
        /// </summary>
        /// <returns>返回 <see cref="IFileProvider"/>。</returns>
        public abstract IFileProvider GetStaticFileProvider();
    }
}
