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

namespace Librame.AspNetCore.Web.Projects
{
    using Resources;

    internal class RootProjectNavigationWithPage : ProjectNavigationWithPage
    {
        public RootProjectNavigationWithPage(IHtmlLocalizer<ProjectNavigationResource> localizer)
            : base(localizer)
        {
        }

    }
}
