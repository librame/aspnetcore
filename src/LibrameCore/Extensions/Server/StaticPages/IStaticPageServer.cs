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

namespace LibrameCore.Extensions.Server
{
    /// <summary>
    /// 静态页服务器接口。
    /// </summary>
    public interface IStaticPageServer : IServerExtensionService
    {
        /// <summary>
        /// 读取器。
        /// </summary>
        IStaticPageReader Reader { get; }

        /// <summary>
        /// 写入器。
        /// </summary>
        IStaticPageWriter Writer { get; }

        /// <summary>
        /// 启用此功能。
        /// </summary>
        bool Enabled { get; }


        /// <summary>
        /// 内容类型。
        /// </summary>
        string ContentType { get; set; }


        /// <summary>
        /// 动作执行前。
        /// </summary>
        /// <param name="context">给定的 <see cref="ActionExecutingContext"/>。</param>
        void ActionExecuting(ActionExecutingContext context);


        /// <summary>
        /// 动作执行后。
        /// </summary>
        /// <param name="context">给定的 <see cref="ActionExecutedContext"/>。</param>
        void ActionExecuted(ActionExecutedContext context);
    }
}
