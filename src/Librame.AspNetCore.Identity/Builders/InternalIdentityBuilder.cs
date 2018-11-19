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
    using Builders;
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// 内部身份构建器。
    /// </summary>
    internal class InternalIdentityBuilder : DefaultBuilder, IIdentityBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="InternalIdentityBuilder"/> 实例。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IDataBuilder"/>。</param>
        public InternalIdentityBuilder(IDataBuilder builder)
            : base(builder)
        {
            Data = builder;

            Services.AddSingleton<IIdentityBuilder>(this);
        }


        /// <summary>
        /// 数据构建器。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IDataBuilder"/>。
        /// </value>
        public IDataBuilder Data { get; private set; }

        /// <summary>
        /// 核心身份构建器。
        /// </summary>
        /// <value>返回 <see cref="IdentityBuilder"/>。</value>
        public IdentityBuilder Core { get; private set; }


        /// <summary>
        /// 注册核心。
        /// </summary>
        /// <param name="configureOptions">给定用于配置的身份选项。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public IIdentityBuilder RegisterCore<TUser>(Action<IdentityOptions> configureOptions = null)
            where TUser : class
        {
            if (configureOptions.IsDefault())
                Core = Services.AddIdentityCore<TUser>();
            else
                Core = Services.AddIdentityCore<TUser>(configureOptions);

            return this;
        }

    }
}
