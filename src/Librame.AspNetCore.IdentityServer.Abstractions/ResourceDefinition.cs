// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Librame.AspNetCore.IdentityServer
{
    /// <summary>
    /// 资源定义。
    /// </summary>
    public class ResourceDefinition : ServiceDefinition
    {
        /// <summary>
        /// 范围。
        /// </summary>
        public string Scopes { get; set; }
    }
}
