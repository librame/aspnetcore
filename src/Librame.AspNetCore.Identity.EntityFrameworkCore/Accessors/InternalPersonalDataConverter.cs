#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Librame.AspNetCore.Identity
{
    /// <summary>
    /// 内部个人数据转换器。
    /// </summary>
    internal class InternalPersonalDataConverter : ValueConverter<string, string>
    {
        /// <summary>
        /// 构造一个 <see cref="InternalPersonalDataConverter"/> 实例。
        /// </summary>
        /// <param name="protector">给定的 <see cref="IPersonalDataProtector"/>。</param>
        public InternalPersonalDataConverter(IPersonalDataProtector protector)
            : base(s => protector.Protect(s), s => protector.Unprotect(s), default)
        {
        }

    }
}
