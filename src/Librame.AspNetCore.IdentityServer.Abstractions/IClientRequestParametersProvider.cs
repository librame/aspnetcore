// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Librame.AspNetCore.IdentityServer
{
    /// <summary>
    /// 为已配置的客户机生成 OAuth/OpenID 参数值。
    /// </summary>
    public interface IClientRequestParametersProvider
    {
        /// <summary>
        /// Gets parameter values for the client with client id<paramref name="clientId"/>.
        /// </summary>
        /// <param name="context">The current <see cref="HttpContext"/>.</param>
        /// <param name="clientId">The client id for the client.</param>
        /// <returns>A <see cref="IDictionary{TKey, TValue}"/> containing the client parameters and their values.</returns>
        IDictionary<string, string> GetClientParameters(HttpContext context, string clientId);
    }
}
