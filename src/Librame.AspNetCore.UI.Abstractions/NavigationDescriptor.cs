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
    using Extensions.Core;

    /// <summary>
    /// 导航描述符。
    /// </summary>
    public class NavigationDescriptor : IEquatable<NavigationDescriptor>
    {
        /// <summary>
        /// 构造一个 <see cref="NavigationDescriptor"/> 实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// text is null or empty.
        /// </exception>
        /// <param name="text">给定的文本。</param>
        /// <param name="relativePath">给定的根相对路径（可选）。</param>
        /// <param name="area">给定的区域（可选）。</param>
        /// <param name="icon">给定的图标样式（可选）。</param>
        /// <param name="children">给定的子级导航（可选；默认列表不为空，长度为“0”）。</param>
        public NavigationDescriptor(string text, string relativePath = null, string area = null, string icon = null,
            IList<NavigationDescriptor> children = null)
        {
            Text = text.NotNullOrEmpty(nameof(text));
            RelativePath = relativePath;
            Area = area;
            Icon = icon ?? "glyphicon glyphicon-link";
            Children = children ?? new List<NavigationDescriptor>();
        }


        /// <summary>
        /// 文本。
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 根相对路径。
        /// </summary>
        public string RelativePath { get; set; }

        /// <summary>
        /// 区域。
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 图标。
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 子级导航。
        /// </summary>
        public IList<NavigationDescriptor> Children { get; set; }


        /// <summary>
        /// 超链接。
        /// </summary>
        public string Href
            => Area.IsNotNullOrEmpty() ? $"/{Area}{RelativePath}" : RelativePath;


        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 可见性工厂方法。
        /// </summary>
        public Func<dynamic, NavigationDescriptor, bool> VisibilityFactory { get; set; }
            = (page, nav) => true;

        /// <summary>
        /// 激活类名工厂方法。
        /// </summary>
        public Func<dynamic, NavigationDescriptor, string> ActiveClassNameFactory { get; set; }

        /// <summary>
        /// 激活样式工厂方法。
        /// </summary>
        public Func<dynamic, NavigationDescriptor, string> ActiveStyleFactory { get; set; }


        private string _id;
        /// <summary>
        /// 标识。
        /// </summary>
        /// <value>返回标识或名称。</value>
        public string Id
        {
            get => _id.NotEmptyOrDefault(Name, throwIfDefaultInvalid: false);
            set => _id = value;
        }


        private string _target;
        /// <summary>
        /// 目标。
        /// </summary>
        public string Target
        {
            get => _target.IsNotNullOrEmpty() ? $"target='{_target}'" : string.Empty;
            set => _target = value;
        }


        private string _title;
        /// <summary>
        /// 标题。
        /// </summary>
        /// <value>返回标题或文本。</value>
        public string Title
        {
            get => _title.NotEmptyOrDefault(Text);
            set => _title = value;
        }


        /// <summary>
        /// 改变当前区域。
        /// </summary>
        /// <param name="newArea">给定的新区域。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor ChangeArea(string newArea)
        {
            Area = newArea;
            return this;
        }


        /// <summary>
        /// 以新区域创建一个导航副本。
        /// </summary>
        /// <param name="newArea">给定的新区域。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor NewArea(string newArea)
            => new NavigationDescriptor(Text, RelativePath, newArea, Icon, Children)
            {
                Name = Name,
                VisibilityFactory = VisibilityFactory,
                ActiveClassNameFactory = ActiveClassNameFactory,
                ActiveStyleFactory = ActiveStyleFactory,
                Id = Id,
                Target = Target,
                Title = Title
            };


        /// <summary>
        /// 唯一索引是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="NavigationDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(NavigationDescriptor other)
            => Text == other?.Text && Href == other?.Href;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is NavigationDescriptor other) ? Equals(other) : false;


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回 32 位整数。</returns>
        public override int GetHashCode()
            => ToString().GetHashCode();


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => $"{Text},{Href}";


        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="a">给定的 <see cref="NavigationDescriptor"/>。</param>
        /// <param name="b">给定的 <see cref="NavigationDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator ==(NavigationDescriptor a, NavigationDescriptor b)
            => a.Equals(b);

        /// <summary>
        /// 是否不等。
        /// </summary>
        /// <param name="a">给定的 <see cref="FileLocator"/>。</param>
        /// <param name="b">给定的 <see cref="FileLocator"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator !=(NavigationDescriptor a, NavigationDescriptor b)
            => !a.Equals(b);


        /// <summary>
        /// 隐式转换为字符串。
        /// </summary>
        /// <param name="descriptor">给定的 <see cref="NavigationDescriptor"/>。</param>
        public static implicit operator string(NavigationDescriptor descriptor)
            => descriptor.ToString();
    }
}
