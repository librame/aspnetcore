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

namespace Librame.AspNetCore.IdentityServer
{
    using Extensions;
    using Extensions.Core;

    class IdentityServerBuilderWrapper : AbstractExtensionBuilderWrapper<IIdentityServerBuilder>, IIdentityServerBuilderWrapper
    {
        public IdentityServerBuilderWrapper(Type userType, IExtensionBuilder builder, IIdentityServerBuilder rawBuilder)
            : base(builder, rawBuilder)
        {
            UserType = userType.NotNull(nameof(userType));

            Services.AddSingleton<IIdentityServerBuilderWrapper>(this);
        }


        public Type UserType { get; }
    }
}
