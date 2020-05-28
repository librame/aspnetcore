#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.Localization;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.IdentityServer.Web.Projects
{
    using AspNetCore.Web.Projects;
    using AspNetCore.Web.Resources;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityServerProjectNavigation : ProjectNavigationWithController
    {
        public IdentityServerProjectNavigation(IHtmlLocalizer<ProjectNavigationResource> localizer)
            : base(localizer, nameof(IdentityServer))
        {
        }

    }
}
