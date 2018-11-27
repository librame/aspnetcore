#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using System;

namespace Librame.AspNetCore.Identity
{
    using Builders;

    /// <summary>
    /// 身份构建器接口。
    /// </summary>
    public interface IIdentityBuilder : IBuilder
    {
        /// <summary>
        /// 核心身份构建器。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IdentityBuilder"/>。
        /// </value>
        IdentityBuilder Core { get; }


        /// <summary>
        /// 注册核心。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="configureOptions">给定用于配置的身份选项。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        IIdentityBuilder RegisterCore<TUser>(Action<IdentityOptions> configureOptions = null)
            where TUser : class;
    }
}
