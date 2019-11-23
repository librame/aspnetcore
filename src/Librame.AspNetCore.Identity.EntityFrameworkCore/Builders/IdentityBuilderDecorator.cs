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

namespace Librame.AspNetCore.Identity
{
    using Extensions.Core;

    class IdentityBuilderDecorator : AbstractExtensionBuilderDecorator<IdentityBuilder>, IIdentityBuilderDecorator
    {
        public IdentityBuilderDecorator(IdentityBuilder source, IExtensionBuilder builder,
            IdentityBuilderDependencyOptions dependencyOptions)
            : base(source, builder, dependencyOptions)
        {
            Services.AddSingleton<IIdentityBuilderDecorator>(this);
        }

    }
}
