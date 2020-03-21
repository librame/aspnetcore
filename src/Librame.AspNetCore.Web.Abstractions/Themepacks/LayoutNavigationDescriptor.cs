#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;

namespace Librame.AspNetCore.Web.Themepacks
{
    using Routings;

    /// <summary>
    /// 布局导航描述符。
    /// </summary>
    public class LayoutNavigationDescriptor
    {
        private readonly Lazy<List<NavigationDescriptor>> _header;
        private readonly Lazy<List<NavigationDescriptor>> _sidebar;
        private readonly Lazy<List<NavigationDescriptor>> _footer;
        private readonly Lazy<List<NavigationDescriptor>> _other;


        /// <summary>
        /// 构造一个 <see cref="LayoutNavigationDescriptor"/>。
        /// </summary>
        /// <param name="header">给定的顶栏 <see cref="List{NavigationDescriptor}"/>（可选）。</param>
        /// <param name="sidebar">给定的侧边栏 <see cref="List{NavigationDescriptor}"/>（可选）。</param>
        /// <param name="footer">给定的底栏 <see cref="List{NavigationDescriptor}"/>（可选）。</param>
        /// <param name="other">给定的其他 <see cref="List{NavigationDescriptor}"/>（可选）。</param>
        public LayoutNavigationDescriptor(List<NavigationDescriptor> header = null,
            List<NavigationDescriptor> sidebar = null,
            List<NavigationDescriptor> footer = null,
            List<NavigationDescriptor> other = null)
        {
            _header = new Lazy<List<NavigationDescriptor>>(() => header ?? new List<NavigationDescriptor>());
            _sidebar = new Lazy<List<NavigationDescriptor>>(() => sidebar ?? new List<NavigationDescriptor>());
            _footer = new Lazy<List<NavigationDescriptor>>(() => footer ?? new List<NavigationDescriptor>());
            _other = new Lazy<List<NavigationDescriptor>>(() => other ?? new List<NavigationDescriptor>());
        }


        /// <summary>
        /// 顶栏导航列表。
        /// </summary>
        public List<NavigationDescriptor> Header
            => _header.Value;

        /// <summary>
        /// 侧边栏导航列表。
        /// </summary>
        public List<NavigationDescriptor> Sidebar
            => _sidebar.Value;

        /// <summary>
        /// 底栏导航列表。
        /// </summary>
        public List<NavigationDescriptor> Footer
            => _footer.Value;

        /// <summary>
        /// 其他导航列表。
        /// </summary>
        public List<NavigationDescriptor> Other
            => _other.Value;
    }
}
