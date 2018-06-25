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
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System.Text;
using System.Threading.Tasks;

namespace LibrameCore.Extensions.Server
{
    /// <summary>
    /// 静态页生成器接口。
    /// </summary>
    public interface IStaticPageGenerator
    {
        /// <summary>
        /// 视图结果执行器。
        /// </summary>
        ViewResultExecutor Executor { get; }

        /// <summary>
        /// MVC 视图选项。
        /// </summary>
        MvcViewOptions Options { get; }


        /// <summary>
        /// 异步呈现视图。
        /// </summary>
        /// <param name="context">给定的动作执行上下文。</param>
        /// <returns>返回一个异步操作。</returns>
        Task<StringBuilder> RenderAsync(ActionExecutedContext context);
    }
}
