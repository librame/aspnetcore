﻿#region License

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
    /// 使用恢复码视图资源。
    /// </summary>
    public class UseRecoveryCodeViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 恢复码。
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 返回 URL。
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}