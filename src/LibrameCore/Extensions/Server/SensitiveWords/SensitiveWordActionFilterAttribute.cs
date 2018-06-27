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

namespace LibrameCore.Extensions.Server.SensitiveWords
{
    /// <summary>
    /// 敏感词动作过滤器特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class SensitiveWordActionFilterAttribute : Attribute, IActionFilter
    {

        /// <summary>
        /// 动作执行后。
        /// </summary>
        /// <param name="context">给定的动作执行后上下文。</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }


        /// <summary>
        /// 动作执行前。
        /// </summary>
        /// <param name="context">给定的动作执行前上下文。</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var filter = context.HttpContext.RequestServices.GetService<ISensitiveWordServer>();

            filter.Filting(context);
        }

    }
}
