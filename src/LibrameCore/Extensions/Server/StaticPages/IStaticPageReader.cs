#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Threading.Tasks;

namespace LibrameCore.Extensions.Server
{
    /// <summary>
    /// 静态页读取器接口。
    /// </summary>
    public interface IStaticPageReader
    {
        /// <summary>
        /// 视图结果执行器。
        /// </summary>
        IActionResultExecutor<ViewResult> Executor { get; }

        /// <summary>
        /// 视图引擎。
        /// </summary>
        IRazorViewEngine ViewEngine { get; }

        /// <summary>
        /// MVC 视图选项。
        /// </summary>
        MvcViewOptions Options { get; }


        /// <summary>
        /// 模板路径。
        /// </summary>
        string TemplatePath { get; set; }


        /// <summary>
        /// 异步呈现视图。
        /// </summary>
        /// <param name="context">给定的动作执行上下文。</param>
        /// <returns>返回一个异步操作。</returns>
        Task<string> RenderToStringAsync(ActionExecutedContext context);
    }
}
