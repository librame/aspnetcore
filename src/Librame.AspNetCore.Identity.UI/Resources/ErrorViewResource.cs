#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;

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
