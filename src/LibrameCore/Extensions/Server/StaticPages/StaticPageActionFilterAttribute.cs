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
using System;

namespace LibrameCore.Extensions.Server.StaticPages
{
    /// <summary>
    /// 静态页动作过滤器特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class StaticPageActionFilterAttribute : ActionFilterAttribute
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
        /// <param name="context">给定的动作执行上下文。</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var staticPage = context.HttpContext.RequestServices.GetRequiredService<IStaticPageServer>();
            staticPage.Reader.TemplatePath = TemplatePath;
            staticPage.Writer.SavePath = SavePath;

            staticPage.ActionExecuted(context);
        }


        /// <summary>
        /// 动作执行。
        /// </summary>
        /// <param name="context">给定的动作执行上下文。</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var staticPage = context.HttpContext.RequestServices.GetRequiredService<IStaticPageServer>();
            staticPage.Reader.TemplatePath = TemplatePath;
            staticPage.Writer.SavePath = SavePath;

            staticPage.ActionExecuting(context);
        }

    }
}
