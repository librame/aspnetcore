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
    /// 个人数据转换器。
    /// </summary>
    public class PersonalDataConverter : ValueConverter<string, string>
    {
        /// <summary>
        /// 构造一个 <see cref="PersonalDataConverter"/> 实例。
        /// </summary>
        /// <param name="protector">给定的 <see cref="IPersonalDataProtector"/>。</param>
        public PersonalDataConverter(IPersonalDataProtector protector)
            : base(s => protector.Protect(s), s => protector.Unprotect(s), default)
        {
        }

    }
}
