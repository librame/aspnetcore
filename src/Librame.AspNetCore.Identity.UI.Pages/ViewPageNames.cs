#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    /// <summary>
    /// 视图页名。
    /// </summary>
    public static class ViewPageNames
    {
        /// <summary>
        /// 更改密码。
        /// </summary>
        public static readonly string ChangePassword = nameof(ChangePassword);

        /// <summary>
        /// 外部登入。
        /// </summary>
        public static readonly string ExternalLogins = nameof(ExternalLogins);

        /// <summary>
        /// 资料。
        /// </summary>
        public static readonly string Profile = nameof(Profile);

        /// <summary>
        /// 个人数据。
        /// </summary>
        public static readonly string PersonalData = nameof(PersonalData);

        /// <summary>
        /// 双因子验证。
        /// </summary>
        public static readonly string TwoFactorAuthentication = nameof(TwoFactorAuthentication);
    }
}
