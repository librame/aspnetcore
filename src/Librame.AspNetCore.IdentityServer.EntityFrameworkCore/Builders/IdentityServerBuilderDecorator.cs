#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.IdentityServer.Builders
{
    using Extensions;
    using Extensions.Core.Builders;

    internal class IdentityServerBuilderDecorator : AbstractExtensionBuilderDecorator<IIdentityServerBuilder>, IIdentityServerBuilderDecorator
    {
        public IdentityServerBuilderDecorator(Type userType, IIdentityServerBuilder sourceBuilder, IExtensionBuilder parentBuilder,
            IdentityServerBuilderDependency dependency)
            : base(sourceBuilder, parentBuilder, dependency)
        {
            UserType = userType.NotNull(nameof(userType));

            Services.AddSingleton<IIdentityServerBuilderDecorator>(this);
        }


        public Type UserType { get; }
    }
}
