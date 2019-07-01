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
    /// <summary>
    /// 双因子登入视图资源。
    /// </summary>
    public class LoginWith2faViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 使用恢复码登入描述。
        /// </summary>
        public string LoginWithRecoveryCodeDescr { get; set; }
    }
}
