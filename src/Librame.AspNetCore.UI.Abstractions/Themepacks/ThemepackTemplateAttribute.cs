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

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 主题包模板特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ThemepackTemplateAttribute : Attribute
    {
        /// <summary>
        /// 构造一个 <see cref="ThemepackTemplateAttribute"/> 实例。
        /// </summary>
        /// <param name="pageType">给定的页面类型。</param>
        public ThemepackTemplateAttribute(Type pageType)
        {
            PageType = pageType.NotNull(nameof(pageType));
        }


        /// <summary>
        /// 页面类型。
        /// </summary>
        public Type PageType { get; }
    }
}
