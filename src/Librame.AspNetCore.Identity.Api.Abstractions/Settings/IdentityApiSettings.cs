#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api
{
    using Extensions;

    /// <summary>
    /// 身份 API 首选项。
    /// </summary>
    public static class IdentityApiSettings
    {
        private static IIdentityApiPreferenceSetting _preference;

        /// <summary>
        /// 当前偏好设置。
        /// </summary>
        public static IIdentityApiPreferenceSetting Preference
        {
            get => _preference.EnsureSingleton(() => new IdentityApiPreferenceSetting());
            set => _preference = value.NotNull(nameof(value));
        }

    }
}
