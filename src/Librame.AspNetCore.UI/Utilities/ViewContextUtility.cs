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
using System.IO;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// <see cref="ViewContext"/> 实用工具。
    /// </summary>
    public class ViewContextUtility
    {
        /// <summary>
        /// 是否激活视图。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <param name="navigation">给定的 <see cref="NavigationDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsActiveView(ViewContext viewContext, NavigationDescriptor navigation)
        {
            var activeViewName = Path.GetFileNameWithoutExtension(navigation.Href);

            var descriptor = viewContext.ActionDescriptor.RouteValues.AsRouteDescriptor();
            if (!descriptor.IsCurrentView(activeViewName))
            {
                var options = viewContext.HttpContext?.RequestServices?
                    .GetRequiredService<IOptions<UiBuilderOptions>>().Value;

                return viewContext.ViewData.TryGetValue(options.ActiveViewKey, out object value)
                    ? descriptor.IsCurrentView(value?.ToString()) : false;
            }

            return true;
        }

        /// <summary>
        /// 激活视图 CSS 类名或空字符串。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <param name="navigation">给定的 <see cref="NavigationDescriptor"/>。</param>
        /// <returns>返回字符串或空字符串。</returns>
        public static string ActiveViewCssClassNameOrEmpty(ViewContext viewContext, NavigationDescriptor navigation)
            => IsActiveView(viewContext, navigation) ? "active" : string.Empty;
    }
}
