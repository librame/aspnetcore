#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// <see cref="IApplicationNavigation"/> 静态扩展。
    /// </summary>
    public static class AbstractionApplicationNavigationExtensions
    {
        private const string KEY_COMMON_HEADER = "CommonHeader";
        private const string KEY_COMMON_FOOTER = "CommonFooter";
        private const string KEY_COMMON_SIDEBAR = "CommonSidebar";
        private const string KEY_MANAGE_HEADER = "ManageHeader";
        private const string KEY_MANAGE_FOOTER = "ManageFooter";
        private const string KEY_MANAGE_SIDEBAR = "ManageSidebar";


        #region AddOrUpdate

        /// <summary>
        /// 增加或更新公共顶部导航。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <param name="descriptors">给定的描述符集合。</param>
        public static void AddOrUpdateCommonHeader(this IApplicationNavigation navigation, IList<NavigationDescriptor> descriptors)
        {
            navigation.AddOrUpdate(KEY_COMMON_HEADER, descriptors);
        }

        /// <summary>
        /// 增加或更新公共底部导航。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <param name="descriptors">给定的描述符集合。</param>
        public static void AddOrUpdateCommonFooter(this IApplicationNavigation navigation, IList<NavigationDescriptor> descriptors)
        {
            navigation.AddOrUpdate(KEY_COMMON_FOOTER, descriptors);
        }

        /// <summary>
        /// 增加或更新公共侧边栏导航。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <param name="descriptors">给定的描述符集合。</param>
        public static void AddOrUpdateCommonSidebar(this IApplicationNavigation navigation, IList<NavigationDescriptor> descriptors)
        {
            navigation.AddOrUpdate(KEY_COMMON_SIDEBAR, descriptors);
        }

        /// <summary>
        /// 增加或更新管理顶部导航。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <param name="descriptors">给定的描述符集合。</param>
        public static void AddOrUpdateManageHeader(this IApplicationNavigation navigation, IList<NavigationDescriptor> descriptors)
        {
            navigation.AddOrUpdate(KEY_MANAGE_HEADER, descriptors);
        }

        /// <summary>
        /// 增加或更新管理底部导航。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <param name="descriptors">给定的描述符集合。</param>
        public static void AddOrUpdateManageFooter(this IApplicationNavigation navigation, IList<NavigationDescriptor> descriptors)
        {
            navigation.AddOrUpdate(KEY_MANAGE_FOOTER, descriptors);
        }

        /// <summary>
        /// 增加或更新管理侧边栏导航。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <param name="descriptors">给定的描述符集合。</param>
        public static void AddOrUpdateManageSidebar(this IApplicationNavigation navigation, IList<NavigationDescriptor> descriptors)
        {
            navigation.AddOrUpdate(KEY_MANAGE_SIDEBAR, descriptors);
        }

        #endregion


        #region GetDescriptors

        /// <summary>
        /// 获取公共顶部导航的描述符列表。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static IList<NavigationDescriptor> GetCommonHeaderDescriptors(this IApplicationNavigation navigation)
        {
            return navigation[KEY_COMMON_HEADER];
        }

        /// <summary>
        /// 获取公共底部导航的描述符列表。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static IList<NavigationDescriptor> GetCommonFooterDescriptors(this IApplicationNavigation navigation)
        {
            return navigation[KEY_COMMON_FOOTER];
        }

        /// <summary>
        /// 获取公共侧边栏导航的描述符列表。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static IList<NavigationDescriptor> GetCommonSidebarDescriptors(this IApplicationNavigation navigation)
        {
            return navigation[KEY_COMMON_SIDEBAR];
        }

        /// <summary>
        /// 获取管理顶部导航的描述符列表。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static IList<NavigationDescriptor> GetManageHeaderDescriptors(this IApplicationNavigation navigation)
        {
            return navigation[KEY_MANAGE_HEADER];
        }

        /// <summary>
        /// 获取管理底部导航的描述符列表。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static IList<NavigationDescriptor> GetManageFooterDescriptors(this IApplicationNavigation navigation)
        {
            return navigation[KEY_MANAGE_FOOTER];
        }

        /// <summary>
        /// 获取管理侧边栏导航的描述符列表。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public static IList<NavigationDescriptor> GetManageSidebarDescriptors(this IApplicationNavigation navigation)
        {
            return navigation[KEY_MANAGE_SIDEBAR];
        }

        #endregion

    }
}
