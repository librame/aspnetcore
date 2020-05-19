#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// <see cref="EntityTypeBuilder"/> 静态扩展。
    /// </summary>
    public static class IdentityEFCoreEntityTypeBuilderExtensions
    {
        /// <summary>
        /// 配置已标记个人数据特性的属性转换。
        /// </summary>
        /// <param name="builder">给定的 <see cref="EntityTypeBuilder"/>。</param>
        /// <param name="protector">给定的 <see cref="IPersonalDataProtector"/>。</param>
        public static void ConfigurePersonalData(this EntityTypeBuilder builder, IPersonalDataProtector protector)
            => builder.ConfigurePersonalData(new PersonalDataConverter(protector));

        /// <summary>
        /// 配置已标记个人数据特性的属性转换。
        /// </summary>
        /// <param name="builder">给定的 <see cref="EntityTypeBuilder"/>。</param>
        /// <param name="converter">给定的 <see cref="PersonalDataConverter"/>。</param>
        internal static void ConfigurePersonalData(this EntityTypeBuilder builder, PersonalDataConverter converter)
            => builder.ConfigurePropertyConversion<PersonalDataAttribute>(converter);

    }
}
