#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.IdentityServer.Options
{
    using Extensions.Data.Options;

    /// <summary>
    /// 身份服务器存储选项。
    /// </summary>
    public class IdentityServerStoreOptions : AbstractStoreOptions
    {
        /// <summary>
        /// 初始化选项。
        /// </summary>
        public IdentityServerStoreInitializationOptions Initialization { get; set; }
            = new IdentityServerStoreInitializationOptions();
    }
}
