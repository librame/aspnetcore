#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 应用程序后置配置选项基类接口。
    /// </summary>
    public interface IApplicationPostConfigureOptionsBase : IApplicationPostConfigureOptions,
        IPostConfigureOptions<StaticFileOptions>,
        IPostConfigureOptions<RazorPagesOptions>,
        IPostConfigureOptions<RazorViewEngineOptions>
    {
    }
}
