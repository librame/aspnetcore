#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Web.Applications;
using Librame.AspNetCore.Web.Themepacks;
using Librame.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    /// <summary>
    /// <see cref="ViewContext"/> 实用工具。
    /// </summary>
    public static class AbstractionViewContextExtensions
    {
        /// <summary>
        /// 获取当前主题包公共布局路径。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <returns>返回字符串。</returns>
        public static string GetCurrentThemepackCommonLayoutPath(this ViewContext viewContext)
            => viewContext.GetCurrentThemepackInfo().CommonLayoutPath;

        /// <summary>
        /// 获取当前主题包登录布局路径。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <returns>返回字符串。</returns>
        public static string GetCurrentThemepackLoginLayoutPath(this ViewContext viewContext)
            => viewContext.GetCurrentThemepackInfo().LoginLayoutPath;

        /// <summary>
        /// 获取当前主题包管理布局路径。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <returns>返回字符串。</returns>
        public static string GetCurrentThemepackManageLayoutPath(this ViewContext viewContext)
            => viewContext.GetCurrentThemepackInfo().ManageLayoutPath;

        /// <summary>
        /// 获取当前主题包指定名称的布局路径。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <param name="name">指定的名称。</param>
        /// <returns>返回字符串。</returns>
        public static string GetCurrentThemepackLayoutPath(this ViewContext viewContext, string name)
            => viewContext.GetCurrentThemepackInfo().LayoutPaths[name];


        /// <summary>
        /// 获取当前主题包信息。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <returns>返回 <see cref="IThemepackInfo"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public static IThemepackInfo GetCurrentThemepackInfo(this ViewContext viewContext)
        {
            viewContext.NotNull(nameof(viewContext));

            var application = viewContext.HttpContext?.RequestServices?.GetRequiredService<IApplicationContext>();
            return application.CurrentThemepackInfo;
        }

    }
}
