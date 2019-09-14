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
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    ///  UI 同步字典静态扩展。
    /// </summary>
    public static class UiConcurrentDictionaryExtensions
    {
        private static readonly string KeyPrefix = typeof(UiConcurrentDictionaryExtensions).Namespace;


        #region Localization

        private static readonly string CommonLayoutLocalizerKey = $"{KeyPrefix}.CommonLayoutLocalizer";
        private static readonly string ManageLayoutLocalizerKey = $"{KeyPrefix}.ManageLayoutLocalizer";
        private static readonly string LoginLayoutLocalizerKey = $"{KeyPrefix}.LoginLayoutLocalizer";


        /// <summary>
        /// 增加或更新公共布局字符串定位器。
        /// </summary>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        public static void AddOrUpdateCommonLayoutLocalizer(this ConcurrentDictionary<string, IStringLocalizer> localizers,
            IStringLocalizer localizer)
            => localizers.AddOrUpdate(CommonLayoutLocalizerKey, localizer, (key, value) => localizer);

        /// <summary>
        /// 增加或更新管理布局字符串定位器。
        /// </summary>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        public static void AddOrUpdateManageLayoutLocalizer(this ConcurrentDictionary<string, IStringLocalizer> localizers,
            IStringLocalizer localizer)
            => localizers.AddOrUpdate(ManageLayoutLocalizerKey, localizer, (key, value) => localizer);

        /// <summary>
        /// 增加或更新登入布局字符串定位器。
        /// </summary>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        public static void AddOrUpdateLoginLayoutLocalizer(this ConcurrentDictionary<string, IStringLocalizer> localizers,
            IStringLocalizer localizer)
            => localizers.AddOrUpdate(LoginLayoutLocalizerKey, localizer, (key, value) => localizer);


        /// <summary>
        /// 获取公共布局字符串定位器。
        /// </summary>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/> 或 NULL。</returns>
        public static IStringLocalizer GetCommonLayoutLocalizer(this ConcurrentDictionary<string, IStringLocalizer> localizers)
            => localizers[CommonLayoutLocalizerKey];

        /// <summary>
        /// 获取管理布局字符串定位器。
        /// </summary>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/> 或 NULL。</returns>
        public static IStringLocalizer GetManageLayoutLocalizer(this ConcurrentDictionary<string, IStringLocalizer> localizers)
            => localizers[ManageLayoutLocalizerKey];

        /// <summary>
        /// 获取登入布局字符串定位器。
        /// </summary>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/> 或 NULL。</returns>
        public static IStringLocalizer GetLoginLayoutLocalizer(this ConcurrentDictionary<string, IStringLocalizer> localizers)
            => localizers[LoginLayoutLocalizerKey];

        #endregion


        #region Navigation

        private static readonly string CommonHeaderNavigationKey = $"{KeyPrefix}.CommonHeaderNavigation";
        private static readonly string CommonFooterNavigationKey = $"{KeyPrefix}.CommonFooterNavigation";
        private static readonly string CommonSidebarNavigationKey = $"{KeyPrefix}.CommonSidebarNavigation";
        private static readonly string ManageHeaderNavigationKey = $"{KeyPrefix}.ManageHeaderNavigation";
        private static readonly string ManageFooterNavigationKey = $"{KeyPrefix}.ManageFooterNavigation";
        private static readonly string ManageSidebarNavigationKey = $"{KeyPrefix}.ManageSidebarNavigation";
        private static readonly string LoginHeaderNavigationKey = $"{KeyPrefix}.LoginHeaderNavigation";
        private static readonly string LoginFooterNavigationKey = $"{KeyPrefix}.LoginFooterNavigation";
        private static readonly string LoginSidebarNavigationKey = $"{KeyPrefix}.LoginSidebarNavigation";


        /// <summary>
        /// 增加或更新公共头部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateCommonHeaderNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
            => navigations.AddOrUpdate(CommonHeaderNavigationKey, descriptors, (key, value) => descriptors);

        /// <summary>
        /// 增加或更新公共底部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateCommonFooterNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
            => navigations.AddOrUpdate(CommonFooterNavigationKey, descriptors, (key, value) => descriptors);

        /// <summary>
        /// 增加或更新公共侧边栏导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateCommonSidebarNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
            => navigations.AddOrUpdate(CommonSidebarNavigationKey, descriptors, (key, value) => descriptors);

        /// <summary>
        /// 增加或更新管理头部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateManageHeaderNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
            => navigations.AddOrUpdate(ManageHeaderNavigationKey, descriptors, (key, value) => descriptors);

        /// <summary>
        /// 增加或更新管理底部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateManageFooterNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
            => navigations.AddOrUpdate(ManageFooterNavigationKey, descriptors, (key, value) => descriptors);

        /// <summary>
        /// 增加或更新管理侧边栏导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateManageSidebarNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
            => navigations.AddOrUpdate(ManageSidebarNavigationKey, descriptors, (key, value) => descriptors);

        /// <summary>
        /// 增加或更新登入头部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateLoginHeaderNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
            => navigations.AddOrUpdate(LoginHeaderNavigationKey, descriptors, (key, value) => descriptors);

        /// <summary>
        /// 增加或更新登入底部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateLoginFooterNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
            => navigations.AddOrUpdate(LoginFooterNavigationKey, descriptors, (key, value) => descriptors);

        /// <summary>
        /// 增加或更新登入侧边栏导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateLoginSidebarNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
            => navigations.AddOrUpdate(LoginSidebarNavigationKey, descriptors, (key, value) => descriptors);


        /// <summary>
        /// 获取公共头部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetCommonHeaderNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
            => navigations[CommonHeaderNavigationKey];

        /// <summary>
        /// 获取公共底部导航的描述符列表。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetCommonFooterNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
            => navigations[CommonFooterNavigationKey];

        /// <summary>
        /// 获取公共侧边栏导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetCommonSidebarNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
            => navigations[CommonSidebarNavigationKey];

        /// <summary>
        /// 获取管理头部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetManageHeaderNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
            => navigations[ManageHeaderNavigationKey];

        /// <summary>
        /// 获取管理底部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetManageFooterNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
            => navigations[ManageFooterNavigationKey];

        /// <summary>
        /// 获取管理侧边栏导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetManageSidebarNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
            => navigations[ManageSidebarNavigationKey];

        /// <summary>
        /// 获取登入头部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetLoginHeaderNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
            => navigations[LoginHeaderNavigationKey];

        /// <summary>
        /// 获取登入底部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetLoginFooterNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
            => navigations[LoginFooterNavigationKey];

        /// <summary>
        /// 获取登入侧边栏导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetLoginSidebarNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
            => navigations[LoginSidebarNavigationKey];

        #endregion

    }
}
