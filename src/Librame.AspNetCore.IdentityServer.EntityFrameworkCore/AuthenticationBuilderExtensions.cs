﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Librame.AspNetCore.IdentityServer
{
    /// <summary>
    /// Extension methods to configure authentication for existing APIs coexisting with an Authorization Server.
    /// </summary>
    public static class AuthenticationBuilderExtensions
    {
        private const string IdentityServerJwtNameSuffix = "API";

        private static readonly PathString DefaultIdentityUIPathPrefix =
            new PathString("/Identity");

        /// <summary>
        /// Adds an authentication handler for an API that coexists with an Authorization Server.
        /// </summary>
        /// <param name="builder">The <see cref="AuthenticationBuilder"/>.</param>
        /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
        public static AuthenticationBuilder AddIdentityServerJwt(this AuthenticationBuilder builder)
        {
            var services = builder.Services;
            services.TryAddSingleton<IIdentityServerJwtDescriptor, IdentityServerJwtDescriptor>();
            services.TryAddEnumerable(ServiceDescriptor
                .Transient<IConfigureOptions<JwtBearerOptions>, IdentityServerJwtBearerOptionsConfiguration>(JwtBearerOptionsFactory));

            services.AddAuthentication(IdentityServerJwtConstants.IdentityServerJwtScheme)
                .AddPolicyScheme(IdentityServerJwtConstants.IdentityServerJwtScheme, null, options =>
                {
                    options.ForwardDefaultSelector = new IdentityServerJwtPolicySchemeForwardSelector(
                        DefaultIdentityUIPathPrefix,
                        IdentityServerJwtConstants.IdentityServerJwtBearerScheme).SelectScheme;
                })
                .AddJwtBearer(IdentityServerJwtConstants.IdentityServerJwtBearerScheme, null, o => { });

            return builder;

            IdentityServerJwtBearerOptionsConfiguration JwtBearerOptionsFactory(IServiceProvider sp)
            {
                var schemeName = IdentityServerJwtConstants.IdentityServerJwtBearerScheme;

                var localApiDescriptor = sp.GetRequiredService<IIdentityServerJwtDescriptor>();
                var hostingEnvironment = sp.GetRequiredService<IHostingEnvironment>();
                var apiName = hostingEnvironment.ApplicationName + IdentityServerJwtNameSuffix;

                return new IdentityServerJwtBearerOptionsConfiguration(schemeName, apiName, localApiDescriptor);
            }
        }

    }
}
