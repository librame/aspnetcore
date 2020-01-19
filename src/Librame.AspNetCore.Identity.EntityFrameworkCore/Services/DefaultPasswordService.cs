#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Services
{
    using Extensions.Core.Services;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class DefaultPasswordService : AbstractService, IDefaultPasswordService
    {
        public DefaultPasswordService(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }


        public string GetDefaultPassword(object user)
            => "Password!123456";
    }
}
