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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Options;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LibrameCore.Filtration.StaticalHtml
{
    /// <summary>
    /// 视图结果静态化器。
    /// </summary>
    public class ViewResultStaticizer : IViewResultStaticizer
    {

        /// <summary>
        /// 构造一个视图结果静态化器实例。
        /// </summary>
        /// <param name="executor">给定的视图结果执行器。</param>
        /// <param name="options">给定的 MVC 视图选项。</param>
        public ViewResultStaticizer(ViewResultExecutor executor,
            IOptions<MvcViewOptions> options)
        {
            Executor = executor.NotNull(nameof(executor));
            Options = options.NotNull(nameof(options)).Value;
        }

        
        /// <summary>
        /// 视图结果执行器。
        /// </summary>
        public ViewResultExecutor Executor { get; }

        /// <summary>
        /// MVC 视图选项。
        /// </summary>
        public MvcViewOptions Options { get; }


        /// <summary>
        /// 异步呈现视图。
        /// </summary>
        /// <param name="context">给定的动作执行上下文。</param>
        /// <returns>返回一个异步操作。</returns>
        public async Task<StringBuilder> RenderAsync(ActionExecutedContext context)
        {
            var viewResult = context.Result as ViewResult;
            var result = Executor.FindView(context, viewResult);

            // 确保成功执行请求视图
            result.EnsureSuccessful(originalLocations: null);
            
            var builder = new StringBuilder();

            using (var sw = new StringWriter(builder))
            {
                var viewContext = new ViewContext(
                    context,
                    result.View,
                    viewResult.ViewData,
                    viewResult.TempData,
                    sw,
                    Options.HtmlHelperOptions);

                // 异步呈现视图
                await result.View.RenderAsync(viewContext);

                sw.Flush();
            }

            return builder;
        }

    }
}
