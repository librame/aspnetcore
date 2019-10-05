// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Stores;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer
{
    using Extensions;

    /// <summary>
    /// 抽象客户端存储静态扩展。
    /// </summary>
    public static class ClientStoreExtensions
    {
        /// <summary>
        /// 确定客户端是否配置为使用 PKCE。
        /// </summary>
        /// <param name="store">给定的 <see cref="IClientStore"/>。</param>
        /// <param name="clientId">给定的客户端标识。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        public static async Task<bool> IsPkceClientAsync(this IClientStore store, string clientId)
        {
            if (clientId.IsEmpty())
                return false;

            var client = await store.FindEnabledClientByIdAsync(clientId).ConfigureAndResultAsync();
            return client?.RequirePkce == true;
        }

    }
}
