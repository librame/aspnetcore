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
    using Extensions.Core;

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
        IdentityBuilder CoreIdentityBuilder { get; }


        /// <summary>
        /// 添加身份核心。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="configureCoreIdentity">配置核心身份构建器（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        IIdentityBuilder AddIdentityCore<TUser>(Action<IdentityBuilder> configureCoreIdentity = null)
            where TUser : class;
    }
}
