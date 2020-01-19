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

namespace Librame.AspNetCore.Web.Projects
{
    using Extensions.Core.Services;
    using Resources;
    using Routings;

    internal class DefaultProjectInfo : AbstractProjectInfo
    {
        public DefaultProjectInfo(ServiceFactory serviceFactory)
            : base(serviceFactory)
        {
        }


        public override string Name
            => RouteDescriptor.DefaultRouteName;

        public override IStringLocalizer Localizer
            => ServiceFactory.GetRequiredService<IStringLocalizer<DefaultProjectInfoResource>>();
    }
}
