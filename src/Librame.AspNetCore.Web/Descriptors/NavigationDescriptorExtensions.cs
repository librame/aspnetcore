#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Descriptors
{
    using AspNetCore.Web.Builders;
    using Extensions;

    /// <summary>
    /// <see cref="NavigationDescriptor"/> 静态扩展。
    /// </summary>
    public static class NavigationDescriptorExtensions
    {
        /// <summary>
        /// 是否激活导航（支持查找激活视图键名）。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="NavigationDescriptor"/>。</param>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public static bool IsActivated(this NavigationDescriptor navigation, ViewContext viewContext)
        {
            navigation.NotNull(nameof(navigation));
            viewContext.NotNull(nameof(viewContext));

            var viewRoute = viewContext.RouteData.Values.AsRouteDescriptor();
            if (!navigation.IsActivated(viewRoute))
            {
                var options = viewContext.HttpContext?.RequestServices
                    .GetRequiredService<IOptions<WebBuilderOptions>>().Value;

                // 支持激活视图键名
                return viewContext.ViewData.TryGetValue(options.ActiveViewKey, out object value)
                    && navigation.IsActivated(value?.ToString());
            }

            return true;
        }


        /// <summary>
        /// 格式化激活 CSS 类名或空字符串。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="NavigationDescriptor"/>。</param>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <param name="activeCssClassName">给定的激活 CSS 类名（可选；默认为“active”）。</param>
        /// <returns>返回激活 CSS 类名或空字符串。</returns>
        public static string FormatActiveCssClassNameOrEmptyString(this NavigationDescriptor navigation,
            ViewContext viewContext, string activeCssClassName = "active")
            => navigation.IsActivated(viewContext) ? activeCssClassName : string.Empty;

        /// <summary>
        /// 格式化目标键值对（如果目标不为空）或空字符串。
        /// </summary>
        /// <returns>返回键值对或空字符串。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public static string FormatTargetPairOrEmptyString(this NavigationDescriptor navigation)
        {
            navigation.NotNull(nameof(navigation));
            return navigation.Target.IsNotEmpty() ? $"target=\"{navigation.Target}\"" : string.Empty;
        }

    }
}
