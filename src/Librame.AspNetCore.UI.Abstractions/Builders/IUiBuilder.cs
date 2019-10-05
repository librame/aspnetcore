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

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    /// <summary>
    /// UI 构建器接口。
    /// </summary>
    public interface IUiBuilder : IExtensionBuilder
    {
        /// <summary>
        /// 用户类型。
        /// </summary>
        Type UserType { get; }


        /// <summary>
        /// 添加用户类型。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        IUiBuilder AddUser<TUser>()
            where TUser : class;

        /// <summary>
        /// 添加用户类型。
        /// </summary>
        /// <param name="userType">给定的用户类型。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        IUiBuilder AddUser(Type userType);
    }
}
