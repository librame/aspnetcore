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

namespace Librame.AspNetCore.Identity.Web.Projects
{
    using AspNetCore.Web.Applications;
    using AspNetCore.Web.Projects;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityProjectConfiguration : ProjectConfigurationBase
    {
        public IdentityProjectConfiguration(IApplicationContext context)
            : base(context, nameof(Identity))
        {
        }

    }
}
