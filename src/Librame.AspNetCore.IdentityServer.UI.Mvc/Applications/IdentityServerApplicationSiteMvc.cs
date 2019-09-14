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

    class IdentityServerApplicationSiteMvc : AbstractApplicationSite
    {
        public IdentityServerApplicationSiteMvc(IExpressionStringLocalizer<ApplicationSiteMapResource> localizer)
            : base(new ApplicationSiteNavigationViews(localizer, "IdentityServer"))
        {
        }

    }
}
