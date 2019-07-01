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
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Identity
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 内部身份构建器。
    /// </summary>
    internal class InternalIdentityBuilder : AbstractBuilder<IdentityBuilderOptions>, IIdentityBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="InternalIdentityBuilder"/> 实例。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="IdentityBuilderOptions"/>。</param>
        public InternalIdentityBuilder(IBuilder builder, IdentityBuilderOptions options)
            : base(builder, options)
        {
            Services.AddSingleton<IIdentityBuilder>(this);
        }


        /// <summary>
        /// 核心身份构建器。
        /// </summary>
        /// <value>返回 <see cref="IdentityBuilder"/>。</value>
        public IdentityBuilder CoreIdentityBuilder { get; private set; }


        /// <summary>
        /// 添加身份核心。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="configureCoreIdentity">配置核心身份构建器（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public IIdentityBuilder AddIdentityCore<TUser>(Action<IdentityBuilder> configureCoreIdentity = null)
            where TUser : class
        {
            Action<IdentityOptions> configureCoreOptions = null;

            if (Options is IdentityBuilderOptions options)
                configureCoreOptions = options.ConfigureCoreIdentity;

            if (configureCoreOptions.IsNull())
                CoreIdentityBuilder = Services.AddIdentityCore<TUser>();
            else
                CoreIdentityBuilder = Services.AddIdentityCore<TUser>(configureCoreOptions);

            configureCoreIdentity?.Invoke(CoreIdentityBuilder);

            return this;
        }

    }
}
