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

namespace Librame.Builders
{
    using AspNetCore.Identity;

    /// <summary>
    /// 抽象身份构建器静态扩展。
    /// </summary>
    public static class AbstractIdentityBuilderExtensions
    {

        /// <summary>
        /// 配置核心身份构建器。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityBuilder"/>。</param>
        /// <param name="configureAction">给定的 <see cref="Action{IdentityBuilder}"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder ConfigureCore(this IIdentityBuilder builder, Action<IdentityBuilder> configureAction)
        {
            configureAction?.Invoke(builder.Core);

            return builder;
        }

    }
}
