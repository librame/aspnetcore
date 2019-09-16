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
    /// 验证码视图资源。
    /// </summary>
    public class VerifyCodeViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }
    }
}
