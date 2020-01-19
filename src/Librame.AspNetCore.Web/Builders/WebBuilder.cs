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
using System.Collections.Generic;

namespace Librame.AspNetCore.Web.Builders
{
    using AspNetCore.Applications;
    using Extensions;
    using Extensions.Core.Builders;
    using Themepacks;

    internal class WebBuilder : AbstractExtensionBuilder, IWebBuilder
    {
        public WebBuilder(IExtensionBuilder parentBuilder, WebBuilderDependency dependency)
            : base(parentBuilder, dependency)
        {
            Services.AddSingleton<IWebBuilder>(this);
        }


        public bool SupportedGenericController { get; internal set; }

        public IReadOnlyDictionary<string, IThemepackInfo> ThemepackInfos { get; }
            = ApplicationHelper.GetApplicationInfos(ThemepackAssemblyPatternRegistration.All,
                type => type.EnsureCreate<IThemepackInfo>()); // 此创建方法要求类型可公共访问

        public Type UserType { get; private set; }


        public IWebBuilder AddUser<TUser>()
            where TUser : class
        {
            UserType = typeof(TUser);
            return this;
        }

        public IWebBuilder AddUser(Type userType)
        {
            UserType = userType.NotNull(nameof(userType));
            return this;
        }

    }
}
