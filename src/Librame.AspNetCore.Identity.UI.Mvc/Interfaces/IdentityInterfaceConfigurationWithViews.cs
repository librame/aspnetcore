#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityInterfaceConfigurationWithViews : InterfaceConfiguration
    {
        public IdentityInterfaceConfigurationWithViews(IApplicationContext context)
            : base(context, nameof(Identity))
        {
        }

    }
}
