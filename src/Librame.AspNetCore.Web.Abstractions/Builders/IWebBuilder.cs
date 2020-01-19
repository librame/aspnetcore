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

namespace Librame.AspNetCore.Web.Builders
{
    using Extensions.Core.Builders;
    using Themepacks;

    /// <summary>
    /// Web 构建器接口。
    /// </summary>
    public interface IWebBuilder : IExtensionBuilder
    {
        /// <summary>
        /// 支持泛型控制器。
        /// </summary>
        bool SupportedGenericController { get; }

        /// <summary>
        /// 主题包信息列表。
        /// </summary>
        IReadOnlyDictionary<string, IThemepackInfo> ThemepackInfos { get; }

        /// <summary>
        /// 用户类型。
        /// </summary>
        Type UserType { get; }


        /// <summary>
        /// 添加用户类型。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        IWebBuilder AddUser<TUser>()
            where TUser : class;

        /// <summary>
        /// 添加用户类型。
        /// </summary>
        /// <param name="userType">给定的用户类型。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        IWebBuilder AddUser(Type userType);
    }
}
