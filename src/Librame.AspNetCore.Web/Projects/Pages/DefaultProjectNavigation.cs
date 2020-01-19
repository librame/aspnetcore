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

namespace Librame.AspNetCore.Web.Projects
{
    using Resources;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class DefaultProjectNavigation : AbstractProjectNavigation
    {
        public DefaultProjectNavigation(IStringLocalizer<ProjectNavigationResource> localizer)
            : base(localizer) // 默认项目导航的区域必须为空
        {
        }

    }
}
