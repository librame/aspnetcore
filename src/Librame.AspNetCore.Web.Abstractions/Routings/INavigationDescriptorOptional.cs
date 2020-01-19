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
    /// 导航描述符可选配置接口。
    /// </summary>
    public interface INavigationDescriptorOptional : INavigationDescriptorOptionalInfo
    {
        /// <summary>
        /// 改变图标。
        /// </summary>
        /// <param name="newIcon">给定的新图标。</param>
        /// <returns>返回 <see cref="INavigationDescriptorOptional"/>。</returns>
        INavigationDescriptorOptional ChangeIcon(string newIcon);

        /// <summary>
        /// 改变子级。
        /// </summary>
        /// <param name="newChildren">给定的新子级。</param>
        /// <returns>返回 <see cref="INavigationDescriptorOptional"/>。</returns>
        INavigationDescriptorOptional ChangeChildren(List<AbstractNavigationDescriptor> newChildren);


        /// <summary>
        /// 改变可见性工厂方法。
        /// </summary>
        /// <param name="newVisibilityFactory">给定的新可见性工厂方法。</param>
        /// <returns>返回 <see cref="INavigationDescriptorOptional"/>。</returns>
        INavigationDescriptorOptional ChangeVisibilityFactory(Func<dynamic, AbstractNavigationDescriptor, bool> newVisibilityFactory);

        /// <summary>
        /// 改变激活类名工厂方法。
        /// </summary>
        /// <param name="newActiveCssClassNameFactory">给定的新激活类名工厂方法。</param>
        /// <returns>返回 <see cref="INavigationDescriptorOptional"/>。</returns>
        INavigationDescriptorOptional ChangeActiveCssClassNameFactory(Func<dynamic, AbstractNavigationDescriptor, string> newActiveCssClassNameFactory);

        /// <summary>
        /// 改变激活样式工厂方法。
        /// </summary>
        /// <param name="newActiveStyleFactory">给定的新激活样式工厂方法。</param>
        /// <returns>返回 <see cref="INavigationDescriptorOptional"/>。</returns>
        INavigationDescriptorOptional ChangeActiveStyleFactory(Func<dynamic, AbstractNavigationDescriptor, string> newActiveStyleFactory);


        /// <summary>
        /// 改变标签标识。
        /// </summary>
        /// <param name="newTagId">给定的新标签标识。</param>
        /// <returns>返回 <see cref="INavigationDescriptorOptional"/>。</returns>
        INavigationDescriptorOptional ChangeTagId(string newTagId);

        /// <summary>
        /// 改变标签名称。
        /// </summary>
        /// <param name="newTagName">给定的新标签名称。</param>
        /// <returns>返回 <see cref="INavigationDescriptorOptional"/>。</returns>
        INavigationDescriptorOptional ChangeTagName(string newTagName);

        /// <summary>
        /// 改变标签目标。
        /// </summary>
        /// <param name="newTagTarget">给定的新标签目标。</param>
        /// <returns>返回 <see cref="INavigationDescriptorOptional"/>。</returns>
        INavigationDescriptorOptional ChangeTagTarget(string newTagTarget);

        /// <summary>
        /// 改变标签标题。
        /// </summary>
        /// <param name="newTagTitle">给定的新标签标题。</param>
        /// <returns>返回 <see cref="INavigationDescriptorOptional"/>。</returns>
        INavigationDescriptorOptional ChangeTagTitle(string newTagTitle);
    }
}
