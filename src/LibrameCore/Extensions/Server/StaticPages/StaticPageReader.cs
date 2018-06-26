#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LibrameCore.Extensions.Server.StaticPages
{
    /// <summary>
    /// 静态页读取器。
    /// </summary>
    public class StaticPageReader : IStaticPageReader
    {

        /// <summary>
        /// 构造一个 <see cref="StaticPageReader"/> 实例。
        /// </summary>
        /// <param name="executor">给定的 <see cref="ViewResultExecutor"/>。</param>
        /// <param name="viewEngine">给定的 <see cref="IRazorViewEngine"/>。</param>
        /// <param name="options">给定的 MVC 视图选项。</param>
        public StaticPageReader(IActionResultExecutor<ViewResult> executor, IRazorViewEngine viewEngine,
            IOptions<MvcViewOptions> options)
        {
            Executor = executor.NotNull(nameof(executor));
            ViewEngine = viewEngine.NotNull(nameof(viewEngine));
            Options = options.NotNull(nameof(options)).Value;
        }

        
        /// <summary>
        /// 视图结果执行器。
        /// </summary>
        public IActionResultExecutor<ViewResult> Executor { get; }

        /// <summary>
        /// 视图引擎。
        /// </summary>
        public IRazorViewEngine ViewEngine { get; }

        /// <summary>
        /// MVC 视图选项。
        /// </summary>
        public MvcViewOptions Options { get; }


        /// <summary>
        /// 模板路径。
        /// </summary>
        public string TemplatePath { get; set; }


        /// <summary>
        /// 异步呈现视图。
        /// </summary>
        /// <param name="context">给定的动作执行上下文。</param>
        /// <returns>返回一个异步操作。</returns>
        public async Task<string> RenderToStringAsync(ActionExecutedContext context)
        {
            if (context.Result is ViewResult viewResult)
            {
                if (string.IsNullOrEmpty(TemplatePath))
                    TemplatePath = viewResult.ViewName;

                var viewEngineResult = ViewEngine.FindView(context, TemplatePath, true);
                if (viewEngineResult.View == null)
                    throw new ArgumentNullException($"{TemplatePath} not found.");

                using (var sw = new StringWriter())
                {
                    var viewContext = new ViewContext(
                        context,
                        viewEngineResult.View,
                        viewResult.ViewData,
                        viewResult.TempData,
                        sw,
                        Options.HtmlHelperOptions);

                    await viewEngineResult.View.RenderAsync(viewContext);

                    return sw.ToString();
                }
            }

            return string.Empty;
        }

    }
}
