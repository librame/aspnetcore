#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Librame.AspNetCore.Identity.Builders
{
    using Extensions.Core.Builders;

    internal class IdentityBuilderDecorator : AbstractExtensionBuilderDecorator<IdentityBuilder>, IIdentityBuilderDecorator
    {
        public IdentityBuilderDecorator(IdentityBuilder sourceBuilder, IExtensionBuilder parentBuilder, IdentityBuilderDependency dependency)
            : base(sourceBuilder, parentBuilder, dependency)
        {
            Services.AddSingleton<IIdentityBuilderDecorator>(this);
        }

    }
}
