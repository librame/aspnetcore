#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 抽象 Razor 页面集合选项静态扩展。
    /// </summary>
    public static class AbstractionRazorPagesOptionsExtensions
    {
        /// <summary>
        /// 使用 /Pages/Index 模式路由。
        /// </summary>
        /// <param name="options">给定的 <see cref="RazorPagesOptions"/>。</param>
        /// <returns>返回 <see cref="RazorPagesOptions"/>。</returns>
        public static RazorPagesOptions UsePagesRouteStartWithIndex(this RazorPagesOptions options)
            => options.UsePagesRoute("/Index");

        /// <summary>
        /// 使用 /Pages/Home/Index 模式路由。
        /// </summary>
        /// <param name="options">给定的 <see cref="RazorPagesOptions"/>。</param>
        /// <returns>返回 <see cref="RazorPagesOptions"/>。</returns>
        public static RazorPagesOptions UsePagesRouteStartWithHomeIndex(this RazorPagesOptions options)
            => options.UsePagesRoute("/Home/Index");

        /// <summary>
        /// 使用 /Pages/StartPage 模式路由。
        /// </summary>
        /// <param name="options">给定的 <see cref="RazorPagesOptions"/>。</param>
        /// <param name="startPage">给定的起始页面。</param>
        /// <param name="route">给定要与页面关联的路由（可选）。</param>
        /// <returns>返回 <see cref="RazorPagesOptions"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "options")]
        public static RazorPagesOptions UsePagesRoute(this RazorPagesOptions options, string startPage, string route = null)
        {
            options.NotNull(nameof(options));

            options.RootDirectory = "/Pages";
            options.Conventions.AddPageRoute(startPage, route ?? string.Empty);

            return options;
        }

    }
}
