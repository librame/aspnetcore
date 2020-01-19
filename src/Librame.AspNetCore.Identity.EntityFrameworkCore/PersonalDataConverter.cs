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
    internal class PersonalDataConverter : ValueConverter<string, string>
    {
        public PersonalDataConverter(IPersonalDataProtector protector)
            : base(s => protector.Protect(s), s => protector.Unprotect(s), default)
        {
        }

    }
}
