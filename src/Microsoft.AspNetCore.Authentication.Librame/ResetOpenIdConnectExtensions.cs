#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Librame;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="ResetOpenIdConnectHandler"/> 静态扩展。
    /// </summary>
    public static class ResetOpenIdConnectExtensions
    {
        /// <summary>
        /// 添加 ResetOpenIdConnect。
        /// </summary>
        /// <param name="builder">给定的 <see cref="AuthenticationBuilder"/>。</param>
        /// <returns>返回 <see cref="AuthenticationBuilder"/>。</returns>
        public static AuthenticationBuilder AddResetOpenIdConnect(this AuthenticationBuilder builder)
            => builder.AddResetOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, _ => { });

        /// <summary>
        /// 添加 ResetOpenIdConnect。
        /// </summary>
        /// <param name="builder">给定的 <see cref="AuthenticationBuilder"/>。</param>
        /// <param name="configureOptions">给定的配置选项。</param>
        /// <returns>返回 <see cref="AuthenticationBuilder"/>。</returns>
        public static AuthenticationBuilder AddResetOpenIdConnect(this AuthenticationBuilder builder, Action<OpenIdConnectOptions> configureOptions)
            => builder.AddResetOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, configureOptions);

        /// <summary>
        /// 添加 ResetOpenIdConnect。
        /// </summary>
        /// <param name="builder">给定的 <see cref="AuthenticationBuilder"/>。</param>
        /// <param name="authenticationScheme">给定的认证方案。</param>
        /// <param name="configureOptions">给定的配置选项。</param>
        /// <returns>返回 <see cref="AuthenticationBuilder"/>。</returns>
        public static AuthenticationBuilder AddResetOpenIdConnect(this AuthenticationBuilder builder, string authenticationScheme, Action<OpenIdConnectOptions> configureOptions)
            => builder.AddResetOpenIdConnect(authenticationScheme, OpenIdConnectDefaults.DisplayName, configureOptions);

        /// <summary>
        /// 添加 ResetOpenIdConnect。
        /// </summary>
        /// <param name="builder">给定的 <see cref="AuthenticationBuilder"/>。</param>
        /// <param name="authenticationScheme">给定的认证方案。</param>
        /// <param name="displayName">给定的方案显示名称。</param>
        /// <param name="configureOptions">给定的配置选项。</param>
        /// <returns>返回 <see cref="AuthenticationBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "builder")]
        public static AuthenticationBuilder AddResetOpenIdConnect(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<OpenIdConnectOptions> configureOptions)
        {
            builder.NotNull(nameof(builder));

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<OpenIdConnectOptions>, OpenIdConnectPostConfigureOptions>());
            return builder.AddRemoteScheme<OpenIdConnectOptions, ResetOpenIdConnectHandler>(authenticationScheme, displayName, configureOptions);
        }

    }
}
