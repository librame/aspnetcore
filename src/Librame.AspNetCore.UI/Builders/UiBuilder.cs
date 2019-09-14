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

namespace Librame.AspNetCore.UI
{
    using Extensions;
    using Extensions.Core;

    class UiBuilder : AbstractExtensionBuilder, IUiBuilder
    {
        public UiBuilder(IExtensionBuilder builder)
            : base(builder)
        {
            Services.AddSingleton<IUiBuilder>(this);
        }


        public Type UserType { get; private set; }


        public IUiBuilder AddUser<TUser>()
            where TUser : class
        {
            UserType = typeof(TUser);
            return this;
        }

        public IUiBuilder AddUser(Type userType)
        {
            UserType = userType.NotNull(nameof(userType));
            return this;
        }

    }
}
