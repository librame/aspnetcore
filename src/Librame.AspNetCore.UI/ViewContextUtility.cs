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
using System;
using System.IO;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// <see cref="ViewContext"/> 实用工具。
    /// </summary>
    public static class ViewContextUtility
    {
        /// <summary>
        /// 是否激活视图。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <param name="activeViewName">给定的激活视图名称。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsActiveView(ViewContext viewContext, string activeViewName)
        {
            var options = viewContext.HttpContext?.RequestServices?
                .GetRequiredService<IOptions<UiBuilderOptions>>().Value;

            string currentViewName;

            if (viewContext.ViewData.TryGetValue(options.ActiveViewKey, out object value))
                currentViewName = value?.ToString();
            else
                currentViewName = Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);

            return currentViewName.Equals(activeViewName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 获取激活视图 CSS 类名或空字符串。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <param name="activeViewName">给定的激活视图名称。</param>
        /// <returns>返回字符串或空字符串。</returns>
        public static string GetActiveViewCssClassNameOrEmpty(ViewContext viewContext, string activeViewName)
        {
            return IsActiveView(viewContext, activeViewName) ? "active" : string.Empty;
        }

    }
}
