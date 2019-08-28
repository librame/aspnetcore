#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.IdentityServer.UI
{
    using AspNetCore.UI;

    /// <summary>
    /// 布局管理视图资源。
    /// </summary>
    public class LayoutViewResource : AbstractViewResource
    {
        /// <summary>
        /// 资料。
        /// </summary>
        public string Profile { get; set; }

        /// <summary>
        /// 修改密码。
        /// </summary>
        public string ChangePassword { get; set; }

        /// <summary>
        /// 外部登入。
        /// </summary>
        public string ExternalLogins { get; set; }

        /// <summary>
        /// 双因子验证。
        /// </summary>
        public string TwoFactorAuthentication { get; set; }

        /// <summary>
        /// 个人数据。
        /// </summary>
        public string PersonalData { get; set; }

        /// <summary>
        /// 搜索框。
        /// </summary>
        public string SearchBox { get; set; }

        /// <summary>
        /// 项目库。
        /// </summary>
        public string Repository { get; set; }

        /// <summary>
        /// 反馈。
        /// </summary>
        public string Issues { get; set; }

        /// <summary>
        /// 授权。
        /// </summary>
        public string Licenses { get; set; }

        /// <summary>
        /// 管理。
        /// </summary>
        public string Manage { get; set; }

        /// <summary>
        /// 登出。
        /// </summary>
        public string Logout { get; set; }
    }
}
