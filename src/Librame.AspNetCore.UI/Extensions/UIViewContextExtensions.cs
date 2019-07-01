#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.IO;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    /// <summary>
    /// <see cref="ViewContext"/> 静态扩展。
    /// </summary>
    public static class UIViewContextExtensions
    {
        private const string DEFAULT_ACTIVE_VIEW_KEY = "ActivePage";


        /// <summary>
        /// 是否激活视图。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <param name="activeViewName">给定的激活视图名称。</param>
        /// <param name="activeViewKey">给定的激活视图键名（可选）。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsActiveView(this ViewContext viewContext, string activeViewName,
            string activeViewKey = DEFAULT_ACTIVE_VIEW_KEY)
        {
            var viewName = string.Empty;

            if (!string.IsNullOrEmpty(activeViewKey) && viewContext.ViewData.TryGetValue(activeViewKey, out object value))
                viewName = value?.ToString();
            else
                viewName = Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);

            return viewName.Equals(activeViewName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 激活 CSS 类名。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <param name="activeViewName">给定的激活视图名称。</param>
        /// <param name="activeViewKey">给定的激活视图键名（可选）。</param>
        /// <returns>返回字符串或空。</returns>
        public static string ActiveCssClassNameOrEmpty(this ViewContext viewContext, string activeViewName,
            string activeViewKey = DEFAULT_ACTIVE_VIEW_KEY)
        {
            return viewContext.IsActiveView(activeViewName, activeViewKey) ? "active" : string.Empty;
        }

    }
}
