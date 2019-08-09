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
    using Extensions;

    /// <summary>
    /// 用户界面同步字典静态扩展。
    /// </summary>
    public static class UserInterfaceConcurrentDictionaryExtensions
    {

        #region Localization

        private const string LOCALIZER_LAYOUT_COMMON = "CommonLayoutLocalizer";
        private const string LOCALIZER_LAYOUT_MANAGE = "ManageLayoutLocalizer";
        private const string LOCALIZER_LAYOUT_LOGIN = "LoginLayoutLocalizer";


        /// <summary>
        /// 增加或更新公共布局字符串定位器。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        public static void AddOrUpdateCommonLayoutLocalizer<TResource>(this ConcurrentDictionary<string, IStringLocalizer> localizers,
            IStringLocalizer<TResource> localizer)
        {
            var localizerKey = GenerateLocalizerKey<TResource>(LOCALIZER_LAYOUT_COMMON);

            localizers.AddOrUpdate(localizerKey, localizer, (key, value) => localizer);
        }

        /// <summary>
        /// 增加或更新管理布局字符串定位器。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        public static void AddOrUpdateManageLayoutLocalizer<TResource>(this ConcurrentDictionary<string, IStringLocalizer> localizers,
            IStringLocalizer<TResource> localizer)
        {
            var localizerKey = GenerateLocalizerKey<TResource>(LOCALIZER_LAYOUT_MANAGE);

            localizers.AddOrUpdate(localizerKey, localizer, (key, value) => localizer);
        }

        /// <summary>
        /// 增加或更新登入布局字符串定位器。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        public static void AddOrUpdateLoginLayoutLocalizer<TResource>(this ConcurrentDictionary<string, IStringLocalizer> localizers,
            IStringLocalizer<TResource> localizer)
        {
            var localizerKey = GenerateLocalizerKey<TResource>(LOCALIZER_LAYOUT_LOGIN);

            localizers.AddOrUpdate(localizerKey, localizer, (key, value) => localizer);
        }


        /// <summary>
        /// 获取公共布局字符串定位器。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/> 或 NULL。</returns>
        public static IStringLocalizer GetCommonLayoutLocalizer<TResource>(this ConcurrentDictionary<string, IStringLocalizer> localizers)
        {
            var localizerKey = GenerateLocalizerKey<TResource>(LOCALIZER_LAYOUT_COMMON);

            return localizers[localizerKey];
        }

        /// <summary>
        /// 获取管理布局字符串定位器。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/> 或 NULL。</returns>
        public static IStringLocalizer GetManageLayoutLocalizer<TResource>(this ConcurrentDictionary<string, IStringLocalizer> localizers)
        {
            var localizerKey = GenerateLocalizerKey<TResource>(LOCALIZER_LAYOUT_MANAGE);

            return localizers[localizerKey];
        }

        /// <summary>
        /// 获取登入布局字符串定位器。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/> 或 NULL。</returns>
        public static IStringLocalizer GetLoginLayoutLocalizer<TResource>(this ConcurrentDictionary<string, IStringLocalizer> localizers)
        {
            var localizerKey = GenerateLocalizerKey<TResource>(LOCALIZER_LAYOUT_LOGIN);

            return localizers[localizerKey];
        }


        private static string GenerateLocalizerKey<TResource>(string prefix)
        {
            var resourceName = typeof(TResource).GetFullName();

            return $"{prefix}:{resourceName}";
        }

        #endregion


        #region Navigation

        private const string NAVIGATION_COMMON_HEADER = "CommonHeaderNavigation";
        private const string NAVIGATION_COMMON_FOOTER = "CommonFooterNavigation";
        private const string NAVIGATION_COMMON_SIDEBAR = "CommonSidebarNavigation";
        private const string NAVIGATION_MANAGE_HEADER = "ManageHeaderNavigation";
        private const string NAVIGATION_MANAGE_FOOTER = "ManageFooterNavigation";
        private const string NAVIGATION_MANAGE_SIDEBAR = "ManageSidebarNavigation";
        private const string NAVIGATION_LOGIN_HEADER = "LoginHeaderNavigation";
        private const string NAVIGATION_LOGIN_FOOTER = "LoginFooterNavigation";
        private const string NAVIGATION_LOGIN_SIDEBAR = "LoginSidebarNavigation";


        /// <summary>
        /// 增加或更新公共头部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateCommonHeaderNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
        {
            navigations.AddOrUpdate(NAVIGATION_COMMON_HEADER, descriptors, (key, value) => descriptors);
        }

        /// <summary>
        /// 增加或更新公共底部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateCommonFooterNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
        {
            navigations.AddOrUpdate(NAVIGATION_COMMON_FOOTER, descriptors, (key, value) => descriptors);
        }

        /// <summary>
        /// 增加或更新公共侧边栏导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateCommonSidebarNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
        {
            navigations.AddOrUpdate(NAVIGATION_COMMON_SIDEBAR, descriptors, (key, value) => descriptors);
        }

        /// <summary>
        /// 增加或更新管理头部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateManageHeaderNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
        {
            navigations.AddOrUpdate(NAVIGATION_MANAGE_HEADER, descriptors, (key, value) => descriptors);
        }

        /// <summary>
        /// 增加或更新管理底部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateManageFooterNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
        {
            navigations.AddOrUpdate(NAVIGATION_MANAGE_FOOTER, descriptors, (key, value) => descriptors);
        }

        /// <summary>
        /// 增加或更新管理侧边栏导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateManageSidebarNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
        {
            navigations.AddOrUpdate(NAVIGATION_MANAGE_SIDEBAR, descriptors, (key, value) => descriptors);
        }

        /// <summary>
        /// 增加或更新登入头部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateLoginHeaderNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
        {
            navigations.AddOrUpdate(NAVIGATION_LOGIN_HEADER, descriptors, (key, value) => descriptors);
        }

        /// <summary>
        /// 增加或更新登入底部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateLoginFooterNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
        {
            navigations.AddOrUpdate(NAVIGATION_LOGIN_FOOTER, descriptors, (key, value) => descriptors);
        }

        /// <summary>
        /// 增加或更新登入侧边栏导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="descriptors">给定的 <see cref="List{NavigationDescriptor}"/>。</param>
        public static void AddOrUpdateLoginSidebarNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations,
            List<NavigationDescriptor> descriptors)
        {
            navigations.AddOrUpdate(NAVIGATION_LOGIN_SIDEBAR, descriptors, (key, value) => descriptors);
        }


        /// <summary>
        /// 获取公共头部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetCommonHeaderNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
        {
            return navigations[NAVIGATION_COMMON_HEADER];
        }

        /// <summary>
        /// 获取公共底部导航的描述符列表。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetCommonFooterNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
        {
            return navigations[NAVIGATION_COMMON_FOOTER];
        }

        /// <summary>
        /// 获取公共侧边栏导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetCommonSidebarNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
        {
            return navigations[NAVIGATION_COMMON_SIDEBAR];
        }

        /// <summary>
        /// 获取管理头部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetManageHeaderNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
        {
            return navigations[NAVIGATION_MANAGE_HEADER];
        }

        /// <summary>
        /// 获取管理底部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetManageFooterNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
        {
            return navigations[NAVIGATION_MANAGE_FOOTER];
        }

        /// <summary>
        /// 获取管理侧边栏导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetManageSidebarNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
        {
            return navigations[NAVIGATION_MANAGE_SIDEBAR];
        }

        /// <summary>
        /// 获取登入头部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetLoginHeaderNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
        {
            return navigations[NAVIGATION_LOGIN_HEADER];
        }

        /// <summary>
        /// 获取登入底部导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetLoginFooterNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
        {
            return navigations[NAVIGATION_LOGIN_FOOTER];
        }

        /// <summary>
        /// 获取登入侧边栏导航。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static List<NavigationDescriptor> GetLoginSidebarNavigation(this ConcurrentDictionary<string, List<NavigationDescriptor>> navigations)
        {
            return navigations[NAVIGATION_LOGIN_SIDEBAR];
        }

        #endregion

    }
}
