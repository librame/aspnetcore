#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Runtime.InteropServices;

namespace LibrameStandard
{
    using Authentication;

    /// <summary>
    /// Librame 核心选项接口。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class LibrameCoreOptions : LibrameOptions
    {

        /// <summary>
        /// 认证选项。
        /// </summary>
        public AuthenticationAdapterSettings Authentication { get; set; }
            = new AuthenticationAdapterSettings();

    }
}
