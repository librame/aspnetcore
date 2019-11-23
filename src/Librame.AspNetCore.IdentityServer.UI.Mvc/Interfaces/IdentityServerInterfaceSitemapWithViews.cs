#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.IdentityServer.UI
{
    using AspNetCore.UI;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityServerInterfaceSitemapWithViews : InterfaceSitemapWithViews
    {
        public IdentityServerInterfaceSitemapWithViews(IStringLocalizer<InterfaceSitemapResource> localizer)
            : base(localizer, nameof(IdentityServer))
        {
        }

    }
}
