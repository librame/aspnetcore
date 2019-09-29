// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.IdentityServer
{
    class DefaultClientRequestParametersProvider : IClientRequestParametersProvider
    {
        public DefaultClientRequestParametersProvider(
            IAbsoluteUrlFactory urlFactory,
            IOptions<IdentityServerBuilderOptions> options)
        {
            UrlFactory = urlFactory;
            Options = options.Value;
        }


        public IAbsoluteUrlFactory UrlFactory { get; }

        public IdentityServerBuilderOptions Options { get; }


        public IDictionary<string, string> GetClientParameters(HttpContext context, string clientId)
        {
            var client = Options.Authorizations.Clients[clientId];
            var authority = context.GetIdentityServerIssuerUri();
            if (!client.Properties.TryGetValue(ApplicationProfilesPropertyNames.Profile, out var type))
            {
                throw new InvalidOperationException($"Can't determine the type for the client '{clientId}'");
            }

            string responseType;
            switch (type)
            {
                case ApplicationProfiles.IdentityServerSPA:
                case ApplicationProfiles.SPA:
                    responseType = "id_token token";
                    break;
                case ApplicationProfiles.NativeApp:
                    responseType = "code";
                    break;
                //case ApplicationProfiles.WebApplication:
                //    responseType = "id_token code";
                //    break;
                default:
                    throw new InvalidOperationException($"Invalid application type '{type}' for '{clientId}'.");
            }

            return new Dictionary<string, string>
            {
                ["authority"] = authority,
                ["client_id"] = client.ClientId,
                ["redirect_uri"] = UrlFactory.GetAbsoluteUrl(context, client.RedirectUris.First()),
                ["post_logout_redirect_uri"] = UrlFactory.GetAbsoluteUrl(context, client.RedirectUris.First()),
                ["response_type"] = responseType,
                ["scope"] = string.Join(" ", client.AllowedScopes)
            };
        }
    }

}
