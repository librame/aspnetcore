#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Microsoft.AspNetCore.Identity
{
    /// <summary>
    /// Librame 身份错误。
    /// </summary>
    public class LibrameIdentityError : IdentityError
    {
        /// <summary>
        /// 获取或设置键名。
        /// </summary>
        public string Key { get; set; }
    }
}
