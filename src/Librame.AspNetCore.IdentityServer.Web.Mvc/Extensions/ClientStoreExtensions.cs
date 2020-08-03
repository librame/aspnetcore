// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Stores;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer
{
    using Extensions;

    internal static class ClientStoreExtensions
    {
        public static async Task<bool> IsPkceClientAsync(this IClientStore store, string clientId)
        {
            if (clientId.IsEmpty())
                return false;

            var client = await store.FindEnabledClientByIdAsync(clientId).ConfigureAwait();
            return client?.RequirePkce == true;
        }

    }
}
