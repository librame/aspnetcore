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

namespace Librame.AspNetCore.IdentityServer.Web.Projects
{
    using AspNetCore.Web.Applications;
    using AspNetCore.Web.Projects;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityServerProjectConfiguration : ProjectConfigurationBase
    {
        public IdentityServerProjectConfiguration(IApplicationContext context)
            : base(context, nameof(IdentityServer))
        {
        }

    }
}
