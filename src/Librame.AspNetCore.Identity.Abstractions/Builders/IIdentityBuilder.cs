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

namespace Librame.AspNetCore.Identity
{
    using Extensions.Core;

    /// <summary>
    /// 身份构建器接口。
    /// </summary>
    public interface IIdentityBuilder : IExtensionBuilder
    {
        /// <summary>
        /// 身份构建器核心。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IdentityBuilder"/>。
        /// </value>
        IdentityBuilder IdentityCore { get; }


        /// <summary>
        /// 添加身份核心。
        /// </summary>
        /// <param name="identityCore">给定的 <see cref="IdentityBuilder"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        IIdentityBuilder AddIdentityCore(IdentityBuilder identityCore);
    }
}
