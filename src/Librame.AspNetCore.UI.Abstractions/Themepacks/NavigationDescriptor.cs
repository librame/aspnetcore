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

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 导航描述符。
    /// </summary>
    public class NavigationDescriptor
    {
        /// <summary>
        /// 构造一个 <see cref="NavigationDescriptor"/> 实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// text is null or empty.
        /// </exception>
        /// <param name="text">给定的文本。</param>
        /// <param name="href">给定的超链接（可选；默认为“#”）。</param>
        /// <param name="children">给定的子级导航（可选；默认列表不为空，长度为“0”）。</param>
        public NavigationDescriptor(string text, string href = null,
            IList<NavigationDescriptor> children = null)
        {
            Text = text.NotNullOrEmpty(nameof(text));
            Href = href ?? "#";
            Children = children ?? new List<NavigationDescriptor>();
        }


        /// <summary>
        /// 文本。
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// 超链接。
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 子级导航。
        /// </summary>
        public IList<NavigationDescriptor> Children { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标。
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 可见性工厂方法。
        /// </summary>
        public Func<dynamic, bool> VisibilityFactory { get; set; }
            = page => true;

        /// <summary>
        /// 激活类名工厂方法。
        /// </summary>
        public Func<dynamic, string> ActiveClassNameFactory { get; set; }

        /// <summary>
        /// 激活样式工厂方法。
        /// </summary>
        public Func<dynamic, string> ActiveStyleFactory { get; set; }


        private string _id;
        /// <summary>
        /// 标识。
        /// </summary>
        /// <value>返回标识或名称。</value>
        public string Id
        {
            get { return _id.IsNullOrEmpty() ? Name : _id; }
            set { _id = value; }
        }


        private string _target;
        /// <summary>
        /// 目标。
        /// </summary>
        public string Target
        {
            get { return _target.IsNullOrEmpty() ? string.Empty : $"target='{_target}'"; }
            set { _target = value; }
        }


        private string _title;
        /// <summary>
        /// 标题。
        /// </summary>
        /// <value>返回标题或文本。</value>
        public string Title
        {
            get { return _title.IsNullOrEmpty() ? Text : _title; }
            set { _title = value; }
        }
    }
}
