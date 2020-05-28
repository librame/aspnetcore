#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;

namespace Librame.AspNetCore.Web.Projects
{
    using AspNetCore.Web.Resources;
    using Extensions.Core.Services;

    internal class DefaultProjectInfo : AbstractProjectInfo
    {
        public const string DefaultProjectName = "default";


        public DefaultProjectInfo(ServiceFactory serviceFactory)
            : base(serviceFactory)
        {
        }


        public override string Name
            => DefaultProjectName;

        public override IStringLocalizer Localizer
            => ServiceFactory.GetRequiredService<IStringLocalizer<DefaultProjectInfoResource>>();
    }
}
