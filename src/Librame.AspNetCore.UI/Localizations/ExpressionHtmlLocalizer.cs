#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.Localization;
using System;
using System.Linq.Expressions;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 表达式 HTML 定位器。
    /// </summary>
    /// <typeparam name="TResource">指定的资源类型。</typeparam>
    public class ExpressionHtmlLocalizer<TResource> : HtmlLocalizer<TResource>, IExpressionHtmlLocalizer<TResource>
        where TResource : class
    {
        /// <summary>
        /// 构造一个 <see cref="ExpressionHtmlLocalizer{TResource}"/> 实例。
        /// </summary>
        /// <param name="factory">给定的 <see cref="IHtmlLocalizerFactory"/>。</param>
        public ExpressionHtmlLocalizer(IHtmlLocalizerFactory factory)
            : base(factory)
        {
        }


        /// <summary>
        /// 获取字符串属性的本地化字符串。
        /// </summary>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <returns>返回 <see cref="LocalizedHtmlString"/>。</returns>
        public virtual LocalizedHtmlString this[Expression<Func<TResource, string>> propertyExpression]
        {
            get { return this.GetString(propertyExpression); }
        }

        /// <summary>
        /// 获取字符串属性的本地化字符串。
        /// </summary>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <param name="arguments">给定的参数数组。</param>
        /// <returns>返回 <see cref="LocalizedHtmlString"/>。</returns>
        public virtual LocalizedHtmlString this[Expression<Func<TResource, string>> propertyExpression, params object[] arguments]
        {
            get { return this.GetString(propertyExpression, arguments); }
        }
        
    }
}
