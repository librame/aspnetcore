#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Utilities
{
    using Applications;
    using Builders;
    using Extensions;
    using Routings;
    using Themepacks;

    /// <summary>
    /// <see cref="ViewContext"/> 实用工具。
    /// </summary>
    public static class ViewContextUtility
    {
        /// <summary>
        /// 是否激活视图。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <param name="navigation">给定的 <see cref="AbstractNavigationDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static bool IsActiveView(ViewContext viewContext, AbstractNavigationDescriptor navigation)
        {
            viewContext.NotNull(nameof(viewContext));
            navigation.NotNull(nameof(navigation));

            //var factory = viewContext?.HttpContext?.RequestServices?.GetRequiredService<IUrlHelperFactory>();
            //var urlHelper = factory?.GetUrlHelper(viewContext);

            var currentView = viewContext.ActionDescriptor.RouteValues.AsRouteDescriptor();
            if (!currentView.IsView(navigation.Route))
            {
                var options = viewContext.HttpContext?.RequestServices?
                    .GetRequiredService<IOptions<WebBuilderOptions>>().Value;

                return viewContext.ViewData.TryGetValue(options.ActiveViewKey, out object value)
                    ? currentView.IsView(value?.ToString()) : false;
            }

            return true;
        }

        /// <summary>
        /// 激活视图 CSS 类名或空字符串。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <param name="navigation">给定的 <see cref="AbstractNavigationDescriptor"/>。</param>
        /// <returns>返回字符串或空字符串。</returns>
        public static string ActiveViewCssClassNameOrEmpty(ViewContext viewContext, AbstractNavigationDescriptor navigation)
            => IsActiveView(viewContext, navigation) ? "active" : string.Empty;


        /// <summary>
        /// 获取主题包登录布局。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <returns>返回字符串。</returns>
        public static string GetThemepackLoginLayout(this ViewContext viewContext)
            => viewContext.GetThemepackLayout(AbstractThemepackInfo.LoginLayoutKey);

        /// <summary>
        /// 获取主题包管理布局。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <returns>返回字符串。</returns>
        public static string GetThemepackManageLayout(this ViewContext viewContext)
            => viewContext.GetThemepackLayout(AbstractThemepackInfo.ManageLayoutKey);

        /// <summary>
        /// 获取主题包布局。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <param name="name">指定的名称（可选；通常为 Common、Login、Manage 等）。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static string GetThemepackLayout(this ViewContext viewContext, string name = null)
        {
            viewContext.NotNull(nameof(viewContext));

            var application = viewContext.HttpContext?.RequestServices?.GetRequiredService<IApplicationContext>();
            return application.CurrentThemepackInfo.Layouts[name.NotEmptyOrDefault(AbstractThemepackInfo.CommonLayoutKey)];
        }

    }
}
