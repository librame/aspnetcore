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

namespace LibrameCore.Filtration.SensitiveWord
{
    ///// <summary>
    ///// 敏感词动作过滤器。
    ///// </summary>
    //public class SensitiveWordActionFilter : IActionFilter
    //{
    //    /// <summary>
    //    /// 动作执行后。
    //    /// </summary>
    //    /// <param name="context">给定的动作执行后上下文。</param>
    //    public void OnActionExecuted(ActionExecutedContext context)
    //    {
    //    }


    //    /// <summary>
    //    /// 动作执行时。
    //    /// </summary>
    //    /// <param name="context">给定的动作执行时上下文。</param>
    //    public void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        var state = context.ModelState;

    //        //var filter = context.HttpContext.RequestServices.GetService<ISensitiveWordFilter>();
            
    //        //var result = filter.Filting(context.ModelState);
    //    }

    //}
}
