#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Web.Resources
{
    using AspNetCore.Web.Resources;

    /// <summary>
    /// 错误视图资源。
    /// </summary>
    public class ErrorViewResource : AbstractViewResource
    {
        /// <summary>
        /// 请求标识。
        /// </summary>
        public string RequestId { get; set; }
    }
}
