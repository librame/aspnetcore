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
    /// 启用验证器视图资源。
    /// </summary>
    public class EnableAuthenticatorViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 下载认证器。
        /// </summary>
        public string DownloadAuthenticator { get; set; }

        /// <summary>
        /// 扫描二维码。
        /// </summary>
        public string ScanQRCode { get; set; }

        /// <summary>
        /// 阅读文档。
        /// </summary>
        public string ReadDocument { get; set; }

        /// <summary>
        /// 验证代码。
        /// </summary>
        public string VerificationCode { get; set; }

        /// <summary>
        /// 验证代码信息。
        /// </summary>
        public string VerificationCodeInfo { get; set; }
    }
}
