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

namespace LibrameCore.Website
{
    using Filtration.SensitiveWord;

    //public class SensitiveWordActionFilter : IActionFilter
    //{

    //    public void OnActionExecuted(ActionExecutedContext context)
    //    {
    //    }

    //    public void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        var form = context.HttpContext.Request.Form;

    //        if (form.Count > 0)
    //        {
    //            var filter = context.HttpContext.RequestServices.GetService<ISensitiveWordFilter>();

    //            foreach (var key in form.Keys)
    //            {
    //                var content = form[key].ToString();
    //                var result = filter.Filting(content);

    //                form[key] = result.content;
    //            }
    //        }
    //    }

    //}
}
