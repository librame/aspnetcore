// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Librame.AspNetCore.IdentityServer.UI
{
    /// <summary>
    /// 设备授权输入模型。
    /// </summary>
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        /// <summary>
        /// 用户码。
        /// </summary>
        public string UserCode { get; set; }
    }
}