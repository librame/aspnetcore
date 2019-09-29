// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Librame.AspNetCore.IdentityServer
{
    /// <summary>
    /// 身份服务器 JWT 描述符接口。
    /// </summary>
    public interface IIdentityServerJwtDescriptor
    {
        /// <summary>
        /// 获取资源定义。
        /// </summary>
        /// <returns>返回字典集合。</returns>
        IDictionary<string, ResourceDefinition> GetResourceDefinitions();
    }
}