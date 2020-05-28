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
        /// 添加手机号码。
        /// </summary>
        public string AddPhoneNumber { get; set; }
    }
}
