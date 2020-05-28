#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Web.Themepacks
{
    using Extensions.Core.Localizers;
    using Extensions.Core.Resources;

    /// <summary>
    /// 主题包表达式定位器。
    /// </summary>
    /// <typeparam name="TResource">指定的资源类型。</typeparam>
    public class ThemepackExpressionLocalizer<TResource> : DictionaryStringLocalizer<TResource>
        where TResource : class, IResource
    {
        /// <summary>
        /// 构造一个 <see cref="ThemepackExpressionLocalizer{TResource}"/>。
        /// </summary>
        public ThemepackExpressionLocalizer()
            : this(new CoreResourceDictionaryStringLocalizerFactory())
        {
        }

        /// <summary>
        /// 构造一个 <see cref="ThemepackExpressionLocalizer{TResource}"/>。
        /// </summary>
        /// <param name="factory">给定的 <see cref="IDictionaryStringLocalizerFactory"/>。</param>
        public ThemepackExpressionLocalizer(IDictionaryStringLocalizerFactory factory)
            : base(factory)
        {
        }

    }
}
