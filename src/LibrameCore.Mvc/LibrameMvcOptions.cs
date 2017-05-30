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

namespace LibrameCore
{
    using Utilities;

    /// <summary>
    /// Librame MVC 选项接口。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class LibrameMvcOptions : LibrameOptions
    {
        private Authentication.AuthenticationOptions _authentication;
        /// <summary>
        /// 认证选项。
        /// </summary>
        public Authentication.AuthenticationOptions Authentication
        {
            get
            {
                if (_authentication == null)
                    _authentication = new Authentication.AuthenticationOptions();

                return _authentication;
            }
            set
            {
                _authentication = value.NotNull(nameof(value));
            }
        }

    }
}
