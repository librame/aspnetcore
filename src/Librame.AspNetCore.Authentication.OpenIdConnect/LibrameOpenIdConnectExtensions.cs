#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using Microsoft.AspNetCore.Authentication;
using Librame.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="LibrameOpenIdConnectHandler"/> 静态扩展。
    /// </summary>
    public static class LibrameOpenIdConnectExtensions
    {
        /// <summary>
        /// 添加 LibrameOpenIdConnect。
        /// </summary>
        /// <param name="builder">给定的 <see cref="AuthenticationBuilder"/>。</param>
        /// <returns>返回 <see cref="AuthenticationBuilder"/>。</returns>
        public static AuthenticationBuilder AddLibrameOpenIdConnect(this AuthenticationBuilder builder)
            => builder.AddLibrameOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, _ => { });

        /// <summary>
        /// 添加 LibrameOpenIdConnect。
        /// </summary>
        /// <param name="builder">给定的 <see cref="AuthenticationBuilder"/>。</param>
        /// <param name="configureOptions">给定的配置选项。</param>
        /// <returns>返回 <see cref="AuthenticationBuilder"/>。</returns>
        public static AuthenticationBuilder AddLibrameOpenIdConnect(this AuthenticationBuilder builder, Action<OpenIdConnectOptions> configureOptions)
            => builder.AddLibrameOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, configureOptions);

        /// <summary>
        /// 添加 LibrameOpenIdConnect。
        /// </summary>
        /// <param name="builder">给定的 <see cref="AuthenticationBuilder"/>。</param>
        /// <param name="authenticationScheme">给定的认证方案。</param>
        /// <param name="configureOptions">给定的配置选项。</param>
        /// <returns>返回 <see cref="AuthenticationBuilder"/>。</returns>
        public static AuthenticationBuilder AddLibrameOpenIdConnect(this AuthenticationBuilder builder, string authenticationScheme, Action<OpenIdConnectOptions> configureOptions)
            => builder.AddLibrameOpenIdConnect(authenticationScheme, OpenIdConnectDefaults.DisplayName, configureOptions);

        /// <summary>
        /// 添加 LibrameOpenIdConnect。
        /// </summary>
        /// <param name="builder">给定的 <see cref="AuthenticationBuilder"/>。</param>
        /// <param name="authenticationScheme">给定的认证方案。</param>
        /// <param name="displayName">给定的方案显示名称。</param>
        /// <param name="configureOptions">给定的配置选项。</param>
        /// <returns>返回 <see cref="AuthenticationBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static AuthenticationBuilder AddLibrameOpenIdConnect(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<OpenIdConnectOptions> configureOptions)
        {
            builder.NotNull(nameof(builder));

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<OpenIdConnectOptions>, OpenIdConnectPostConfigureOptions>());
            return builder.AddRemoteScheme<OpenIdConnectOptions, LibrameOpenIdConnectHandler>(authenticationScheme, displayName, configureOptions);
        }

    }
}
