#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Api
{
    using Extensions;

    /// <summary>
    /// API 首选项。
    /// </summary>
    public static class ApiSettings
    {
        private static IApiPreferenceSetting _preference;

        /// <summary>
        /// 当前偏好设置。
        /// </summary>
        public static IApiPreferenceSetting Preference
        {
            get => _preference.EnsureSingleton(() => new ApiPreferenceSetting());
            set => _preference = value.NotNull(nameof(value));
        }

    }
}
