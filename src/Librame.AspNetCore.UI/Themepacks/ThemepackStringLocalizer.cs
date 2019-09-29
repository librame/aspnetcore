#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;
using System.Globalization;

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    /// <summary>
    /// 主题包字符串定位器。
    /// </summary>
    /// <typeparam name="TResource">指定的资源类型。</typeparam>
    public class ThemepackStringLocalizer<TResource> : AbstractResourceDictionaryStringLocalizer<TResource>
        where TResource : class, IResource
    {
        private static readonly IResourceDictionary _defaultResourceDictionary
            = AbstractResourceDictionary.GetResourceDictionary<TResource>(CultureInfo.CurrentUICulture);


        /// <summary>
        /// 构造一个 <see cref="ThemepackStringLocalizer{T}"/>。
        /// </summary>
        public ThemepackStringLocalizer()
            : this(_defaultResourceDictionary)
        {
        }

        private ThemepackStringLocalizer(IResourceDictionary resourceDictionary)
            : base(resourceDictionary)
        {
        }


        /// <summary>
        /// 带文化信息的字符串定位器。
        /// </summary>
        /// <param name="cultureInfo">给定的 <see cref="CultureInfo"/>。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/>。</returns>
        public override IStringLocalizer WithCulture(CultureInfo cultureInfo)
        {
            var resourceDictionary = AbstractResourceDictionary.GetResourceDictionary<TResource>(cultureInfo);
            return new ThemepackStringLocalizer<TResource>(resourceDictionary);
        }
    }
}
