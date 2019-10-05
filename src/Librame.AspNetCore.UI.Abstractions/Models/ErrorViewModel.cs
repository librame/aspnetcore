#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 错误视图模型。
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// 请求标识。
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 显示请求标识。
        /// </summary>
        public bool ShowRequestId => RequestId.IsNotEmpty();
    }
}
