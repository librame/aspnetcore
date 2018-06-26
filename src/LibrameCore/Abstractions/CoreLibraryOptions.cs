#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace LibrameCore.Abstractions
{
    /// <summary>
    /// 核心库选项。
    /// </summary>
    public class CoreLibraryOptions : LibrameStandard.Abstractions.StandardLibraryOptions
    {
        /// <summary>
        /// 认证扩展选项配置动作。
        /// </summary>
        public Action<Extensions.Authentication.AuthenticationExtensionOptions> PostConfigureAuthentication { get; set; }

        /// <summary>
        /// 过滤扩展选项配置动作。
        /// </summary>
        public Action<Extensions.Filtration.FiltrationExtensionOptions> PostConfigureFiltration { get; set; }

        /// <summary>
        /// 服务器扩展选项配置动作。
        /// </summary>
        public Action<Extensions.Server.ServerExtensionOptions> PostConfigureServer { get; set; }
    }
}
