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
        public UiBuilder(Type applicationPostConfigureOptionsType,
            IExtensionBuilder builder)
            : base(builder)
        {
            ApplicationPostConfigureOptionsType = applicationPostConfigureOptionsType
                .NotNull(nameof(applicationPostConfigureOptionsType));

            Services.AddSingleton<IUiBuilder>(this);
        }


        public Type ApplicationPostConfigureOptionsType { get; private set; }

        public Type UserType { get; private set; }


        public IUiBuilder AddApplicationPostConfigureOptions<TAppPostConfigureOptions>()
            where TAppPostConfigureOptions : class, IApplicationPostConfigureOptions
        {
            ApplicationPostConfigureOptionsType = typeof(TAppPostConfigureOptions);
            Services.TryReplaceConfigureOptions<TAppPostConfigureOptions>();
            return this;
        }


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
