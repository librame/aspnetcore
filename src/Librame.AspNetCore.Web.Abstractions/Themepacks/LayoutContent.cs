#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;

namespace Librame.AspNetCore.Web.Themepacks
{
    using Extensions;

    /// <summary>
    /// 布局内容。
    /// </summary>
    /// <typeparam name="TContent">指定的内容类型。</typeparam>
    public class LayoutContent<TContent>
    {
        private readonly Lazy<List<TContent>> _header;
        private readonly Lazy<List<TContent>> _sidebar;
        private readonly Lazy<List<TContent>> _footer;
        private readonly Lazy<List<TContent>> _other;


        /// <summary>
        /// 构造一个 <see cref="LayoutContent{TContent}"/>。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="header">给定的顶栏内容 <see cref="List{TContent}"/>（可选）。</param>
        /// <param name="sidebar">给定的侧边栏内容 <see cref="List{TContent}"/>（可选）。</param>
        /// <param name="footer">给定的底栏内容 <see cref="List{TContent}"/>（可选）。</param>
        /// <param name="other">给定的其他内容 <see cref="List{TContent}"/>（可选）。</param>
        public LayoutContent(string name,
            List<TContent> header = null,
            List<TContent> sidebar = null,
            List<TContent> footer = null,
            List<TContent> other = null)
        {
            Name = name.NotEmpty(nameof(name));
            
            _header = new Lazy<List<TContent>>(() => header ?? new List<TContent>());
            _sidebar = new Lazy<List<TContent>>(() => sidebar ?? new List<TContent>());
            _footer = new Lazy<List<TContent>>(() => footer ?? new List<TContent>());
            _other = new Lazy<List<TContent>>(() => other ?? new List<TContent>());
        }


        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; }


        /// <summary>
        /// 顶栏内容列表。
        /// </summary>
        public List<TContent> Header
            => _header.Value;

        /// <summary>
        /// 侧边栏内容列表。
        /// </summary>
        public List<TContent> Sidebar
            => _sidebar.Value;

        /// <summary>
        /// 底栏内容列表。
        /// </summary>
        public List<TContent> Footer
            => _footer.Value;

        /// <summary>
        /// 其他内容列表。
        /// </summary>
        public List<TContent> Other
            => _other.Value;
    }
}
