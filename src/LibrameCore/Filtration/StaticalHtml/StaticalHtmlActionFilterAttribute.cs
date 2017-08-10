#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Algorithm;
using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;

namespace LibrameCore.Filtration.StaticalHtml
{
    /// <summary>
    /// 静态 HTML 动作过滤器属性特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,
        AllowMultiple = false, Inherited = false)]
    public class StaticalHtmlActionFilterAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// 动作执行后。
        /// </summary>
        /// <param name="context">给定的动作执行上下文。</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ViewResult)
            {
                var serviceProvider = context.HttpContext.RequestServices;
                var options = serviceProvider.GetRequiredService<IOptions<FiltrationOptions>>().Value;

                if (!options.StaticalHtml.Enabled)
                    return;

                var routeInfo = context.RouteData.AsRouteInfo();
                var logger = serviceProvider.GetRequiredService<ILogger<StaticalHtmlActionFilterAttribute>>();
                
                try
                {
                    var htmlBuilder = serviceProvider.GetRequiredService<IHtmlBuilder>();
                    var staticizer = serviceProvider.GetRequiredService<IViewResultStaticizer>();
                    
                    // 取得呈现视图的内容
                    var strBuilder = staticizer.RenderAsync(context)
                        .GetAwaiter().GetResult();

                    // 可格式化内容
                    var content = FormatContent(strBuilder);

                    // 建立 HTML 文件
                    var filename = htmlBuilder.BuildAsync(content, routeInfo)
                        .GetAwaiter().GetResult();

                    var result = new ContentResult();
                    result.Content = content;
                    result.ContentType = "text/html";

                    context.Result = result;

                    logger.LogDebug(filename);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.AsInnerMessage());
                }
            }
        }

        /// <summary>
        /// 格式化 HTML 内容。
        /// </summary>
        /// <param name="builder">给定的字符串构建器。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string FormatContent(StringBuilder builder)
        {
            return builder.ToString();
        }


        /// <summary>
        /// 动作执行。
        /// </summary>
        /// <param name="context">给定的动作执行上下文。</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var routeInfo = context.RouteData.AsRouteInfo();
            var serviceProvider = context.HttpContext.RequestServices;

            try
            {
                var htmlBuilder = serviceProvider.GetRequiredService<IHtmlBuilder>();
                var filename = htmlBuilder.BuildFullFilename(routeInfo);

                //判断文件是否存在
                if (File.Exists(filename))
                {
                    var options = serviceProvider.GetRequiredService<IOptions<AlgorithmOptions>>().Value;
                    var encoding = options.Encoding.AsEncoding();

                    using (var fs = File.Open(filename, FileMode.Open))
                    {
                        using (var sr = new StreamReader(fs, encoding))
                        {
                            var result = new ContentResult();
                            result.Content = sr.ReadToEnd();
                            result.ContentType = "text/html";

                            context.Result = result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<StaticalHtmlActionFilterAttribute>>();

                logger.LogError(ex.AsInnerMessage());
            }
        }

    }
}
