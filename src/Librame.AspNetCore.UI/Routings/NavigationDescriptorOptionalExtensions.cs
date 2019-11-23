#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// <see cref="INavigationDescriptorOptional"/> 静态扩展。
    /// </summary>
    public static class NavigationDescriptorOptionalExtensions
    {
        /// <summary>
        /// 改变为 Page 页面激活 CSS 类名（支持 MVC 视图页面）。
        /// </summary>
        /// <param name="optional">给定的 <see cref="INavigationDescriptorOptional"/>。</param>
        /// <returns>返回 <see cref="INavigationDescriptorOptional"/>。</returns>
        public static INavigationDescriptorOptional ChangeActiveCssClassNameForPage(this INavigationDescriptorOptional optional)
            => optional.NotNull(nameof(optional))
                .ChangeActiveCssClassNameFactory((page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav));
    }
}
