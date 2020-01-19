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

namespace Librame.AspNetCore.IdentityServer.Web.Projects
{
    using AspNetCore.Web.Projects;
    using AspNetCore.Web.Resources;
    using Extensions;
    using Resources;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityServerProjectNavigation : ProjectNavigationWithController
    {
        private readonly IStringLocalizer<LayoutViewResource> _layoutLocalizer;


        public IdentityServerProjectNavigation(IStringLocalizer<ProjectNavigationResource> localizer,
            IStringLocalizer<LayoutViewResource> layoutLocalizer)
            : base(localizer, nameof(IdentityServer))
        {
            _layoutLocalizer = layoutLocalizer.NotNull(nameof(layoutLocalizer));
        }

    }
}
