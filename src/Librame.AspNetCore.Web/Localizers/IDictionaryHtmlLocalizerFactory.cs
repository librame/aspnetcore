#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.Localization;
using System;

namespace Librame.AspNetCore.Web.Localizers
{
    /// <summary>
    /// 字典 HTML 定位器工厂接口。
    /// </summary>
    public interface IDictionaryHtmlLocalizerFactory
    {
        /// <summary>
        /// 创建 HTML 定位器。
        /// </summary>
        /// <param name="resourceBaseType">给定的资源基础类型。</param>
        /// <returns>返回 <see cref="IHtmlLocalizer"/>。</returns>
        IHtmlLocalizer Create(Type resourceBaseType);

        /// <summary>
        /// 创建 HTML 定位器。
        /// </summary>
        /// <param name="baseTypeName">给定的资源基础名。</param>
        /// <param name="assemblyLocation">给定的资源程序集定位。</param>
        /// <returns>返回 <see cref="IHtmlLocalizer"/>。</returns>
        IHtmlLocalizer Create(string baseTypeName, string assemblyLocation);
    }
}
