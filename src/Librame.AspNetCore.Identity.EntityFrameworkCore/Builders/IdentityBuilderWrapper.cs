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

    class IdentityBuilderWrapper : AbstractExtensionBuilderWrapper<IdentityBuilder>, IIdentityBuilderWrapper
    {
        public IdentityBuilderWrapper(IdentityBuilder rawBuilder, IExtensionBuilder builder,
            IdentityBuilderDependencyOptions dependencyOptions)
            : base(rawBuilder, builder, dependencyOptions)
        {
            Services.AddSingleton<IIdentityBuilderWrapper>(this);
        }

    }
}
