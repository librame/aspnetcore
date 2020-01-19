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

namespace Librame.AspNetCore.Web.Routings
{
    /// <summary>
    /// 导航描述符可选配置信息接口。
    /// </summary>
    public interface INavigationDescriptorOptionalInfo
    {
        /// <summary>
        /// 图标。
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// 子级导航。
        /// </summary>
        List<AbstractNavigationDescriptor> Children { get; }


        /// <summary>
        /// 可见性工厂方法。
        /// </summary>
        Func<dynamic, AbstractNavigationDescriptor, bool> VisibilityFactory { get; }

        /// <summary>
        /// 激活类名工厂方法。
        /// </summary>
        Func<dynamic, AbstractNavigationDescriptor, string> ActiveCssClassNameFactory { get; }

        /// <summary>
        /// 激活样式工厂方法。
        /// </summary>
        Func<dynamic, AbstractNavigationDescriptor, string> ActiveStyleFactory { get; }


        /// <summary>
        /// 标签标识。
        /// </summary>
        /// <value>返回标签标识或名称。</value>
        string TagId { get; }

        /// <summary>
        /// 标签名称。
        /// </summary>
        string TagName { get; }

        /// <summary>
        /// 标签目标（如 _target、_self、_blank、_parent、framename 等）。
        /// </summary>
        string TagTarget { get; }

        /// <summary>
        /// 标签标题。
        /// </summary>
        string TagTitle { get; }
    }
}
