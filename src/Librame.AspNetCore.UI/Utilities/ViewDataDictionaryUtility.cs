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
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// <see cref="ViewDataDictionary"/> 实用工具。
    /// </summary>
    public static class ViewDataDictionaryUtility
    {
        /// <summary>
        /// 获取是否存在外部登入方案。
        /// </summary>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "viewContext")]
        public static bool GetHasExternalLogins(ViewContext viewContext)
        {
            viewContext.NotNull(nameof(viewContext));

            var options = viewContext.HttpContext?.RequestServices?
                .GetRequiredService<IOptions<UiBuilderOptions>>().Value;

            return GetHasExternalLogins(viewContext.ViewData,
                options.HasExternalAuthenticationSchemesKey);
        }

        /// <summary>
        /// 获取是否存在外部登入方案。
        /// </summary>
        /// <param name="viewData">给定的 <see cref="ViewDataDictionary"/>。</param>
        /// <param name="key">给定的键名。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "viewData")]
        public static bool GetHasExternalLogins(ViewDataDictionary viewData, string key)
        {
            viewData.NotNull(nameof(viewData));
            return viewData.TryGetValue(key, out object value) ? (bool)value : false;
        }

    }
}
