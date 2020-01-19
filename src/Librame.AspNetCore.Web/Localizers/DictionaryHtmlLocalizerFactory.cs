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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Localizers
{
    using Extensions;
    using Extensions.Core.Localizers;

    /// <summary>
    /// 字典 HTML 定位器工厂。
    /// </summary>
    public class DictionaryHtmlLocalizerFactory : IDictionaryHtmlLocalizerFactory
    {
        private readonly IDictionaryStringLocalizerFactory _factory;


        /// <summary>
        /// 构造一个 <see cref="HtmlLocalizerFactory"/>。
        /// </summary>
        /// <param name="localizerFactory">给定的 <see cref="IDictionaryStringLocalizerFactory"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "localizerFactory")]
        public DictionaryHtmlLocalizerFactory(IDictionaryStringLocalizerFactory localizerFactory)
        {
            _factory = localizerFactory.NotNull(nameof(localizerFactory));
        }


        /// <summary>
        /// 创建 HTML 定位器。
        /// </summary>
        /// <param name="resourceBaseType">给定的资源基础类型。</param>
        /// <returns>返回 <see cref="IHtmlLocalizer"/>。</returns>
        public virtual IHtmlLocalizer Create(Type resourceBaseType)
            => new HtmlLocalizer(_factory.Create(resourceBaseType));

        /// <summary>
        /// 创建 HTML 定位器。
        /// </summary>
        /// <param name="baseTypeName">给定的资源基础名。</param>
        /// <param name="assemblyLocation">给定的资源程序集定位。</param>
        /// <returns>返回 <see cref="IHtmlLocalizer"/>。</returns>
        public virtual IHtmlLocalizer Create(string baseTypeName, string assemblyLocation)
        {
            var localizer = _factory.Create(baseTypeName, assemblyLocation);
            return new HtmlLocalizer(localizer);
        }

    }
}
