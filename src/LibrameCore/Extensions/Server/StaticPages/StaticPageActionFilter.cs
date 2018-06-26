#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace LibrameCore.Extensions.Server.StaticPages
{
    /// <summary>
    /// 静态页动作过滤器。
    /// </summary>
    public class StaticPageActionFilter : IActionFilter
    {
        /// <summary>
        /// 保存路径。
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// 模板路径。
        /// </summary>
        public string TemplatePath { get; set; }


        /// <summary>
        /// 动作执行后。
        /// </summary>
        /// <param name="context">给定的动作执行后上下文。</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var staticPage = context.HttpContext.RequestServices.GetRequiredService<IStaticPageServer>();
            staticPage.Reader.TemplatePath = TemplatePath;
            staticPage.Writer.SavePath = SavePath;

            staticPage.ActionExecuted(context);
        }


        /// <summary>
        /// 动作执行前。
        /// </summary>
        /// <param name="context">给定的动作执行前上下文。</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var staticPage = context.HttpContext.RequestServices.GetRequiredService<IStaticPageServer>();
            staticPage.Reader.TemplatePath = TemplatePath;
            staticPage.Writer.SavePath = SavePath;

            staticPage.ActionExecuting(context);
        }

    }
}
