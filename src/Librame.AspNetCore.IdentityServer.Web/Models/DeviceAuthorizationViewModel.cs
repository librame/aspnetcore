// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Librame.AspNetCore.IdentityServer.Web.Models
{
    /// <summary>
    /// 设备授权视图模型。
    /// </summary>
    public class DeviceAuthorizationViewModel : ConsentViewModel
    {
        /// <summary>
        /// 用户码。
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 确认用户码。
        /// </summary>
        public bool ConfirmUserCode { get; set; }
    }
}