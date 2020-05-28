#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Librame.AspNetCore.Identity
{
    using Extensions;

    /// <summary>
    /// 个人数据转换器。
    /// </summary>
    public class PersonalDataConverter : ValueConverter<string, string>
    {
        /// <summary>
        /// 构造一个 <see cref="PersonalDataConverter"/>。
        /// </summary>
        /// <param name="protector">给定的 <see cref="IPersonalDataProtector"/>。</param>
        public PersonalDataConverter(IPersonalDataProtector protector)
            : this(protector.NotNull(nameof(protector)), default)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="PersonalDataConverter"/>。
        /// </summary>
        /// <param name="protector">给定的 <see cref="IPersonalDataProtector"/>。</param>
        /// <param name="mappingHints">给定的 <see cref="ConverterMappingHints"/>。</param>
        public PersonalDataConverter(IPersonalDataProtector protector, ConverterMappingHints mappingHints)
            : base(s => protector.Protect(s), s => protector.Unprotect(s), mappingHints)
        {
        }

    }
}
