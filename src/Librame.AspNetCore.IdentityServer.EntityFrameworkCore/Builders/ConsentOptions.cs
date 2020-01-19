#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.IdentityServer.Builders
{
    /// <summary>
    /// 批准选项。
    /// </summary>
    public class ConsentOptions
    {
        /// <summary>
        /// 启用离线访问。
        /// </summary>
        public bool EnableOfflineAccess { get; set; }
            = true;

        /// <summary>
        /// 离线访问显示名称。
        /// </summary>
        public string OfflineAccessDisplayName { get; set; }
            = "Offline Access";

        /// <summary>
        /// 离线访问描述。
        /// </summary>
        public string OfflineAccessDescription { get; set; }
            = "Access to your applications and resources, even when you are offline";

        /// <summary>
        /// 必须选择一项的错误消息。
        /// </summary>
        public string MustChooseOneErrorMessage { get; set; }
            = "You must pick at least one permission";

        /// <summary>
        /// 无效选择的错误消息。
        /// </summary>
        public string InvalidSelectionErrorMessage { get; set; }
            = "Invalid selection";
    }
}
