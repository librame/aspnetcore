#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// <see cref="IApplicationLocalization"/> 静态扩展。
    /// </summary>
    public static class AbstractionApplicationLocalizationExtensions
    {
        private const string KEY_COMMON_LAYOUT = "CommonLayout";
        private const string KEY_MANAGE_LAYOUT = "ManageLayout";


        #region AddOrUpdate

        /// <summary>
        /// 增加或更新公共布局本地化。
        /// </summary>
        /// <param name="localization">给定的 <see cref="IApplicationLocalization"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        public static void AddOrUpdateCommonLayout(this IApplicationLocalization localization, IStringLocalizer localizer)
        {
            localization.AddOrUpdate(KEY_COMMON_LAYOUT, localizer);
        }

        /// <summary>
        /// 增加或更新管理布局本地化。
        /// </summary>
        /// <param name="localization">给定的 <see cref="IApplicationLocalization"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        public static void AddOrUpdateManageLayout(this IApplicationLocalization localization, IStringLocalizer localizer)
        {
            localization.AddOrUpdate(KEY_MANAGE_LAYOUT, localizer);
        }

        #endregion


        #region GetLocalizer

        /// <summary>
        /// 获取公共布局定位器本地化。
        /// </summary>
        /// <param name="localization">给定的 <see cref="IApplicationLocalization"/>。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/> 或 NULL。</returns>
        public static IStringLocalizer GetCommonLayoutLocalizer(this IApplicationLocalization localization)
        {
            return localization[KEY_COMMON_LAYOUT];
        }

        /// <summary>
        /// 获取管理布局定位器本地化。
        /// </summary>
        /// <param name="localization">给定的 <see cref="IApplicationLocalization"/>。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/> 或 NULL。</returns>
        public static IStringLocalizer GetManageLayoutLocalizer(this IApplicationLocalization localization)
        {
            return localization[KEY_MANAGE_LAYOUT];
        }

        #endregion

    }
}
