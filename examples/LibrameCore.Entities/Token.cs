#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Entity.Descriptors;
using LibrameStandard.Authentication.Models;

namespace LibrameCore.Entities
{
    /// <summary>
    /// 令牌。
    /// </summary>
    public class Token : AbstractCreateIdDescriptor<int>, ITokenModel
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }
    }
}
