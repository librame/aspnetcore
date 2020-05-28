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

namespace Librame.AspNetCore.Web.Localizers
{
    /// <summary>
    /// 字典 HTML 定位器接口。
    /// </summary>
    /// <typeparam name="TResource">指定的资源类型。</typeparam>
    public interface IDictionaryHtmlLocalizer<TResource> : IHtmlLocalizer<TResource>
    {
    }
}
