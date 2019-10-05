#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.IdentityServer.UI
{
    using AspNetCore.UI;
    using Extensions.Core;

    class IdentityServerInterfaceSitemapWithViews : InterfaceSitemapWithViews
    {
        public IdentityServerInterfaceSitemapWithViews(IExpressionLocalizer<InterfaceSitemapResource> localizer)
            : base(localizer, nameof(IdentityServer))
        {
        }

    }
}
