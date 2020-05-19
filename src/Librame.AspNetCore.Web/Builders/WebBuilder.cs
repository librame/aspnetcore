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

namespace Librame.AspNetCore.Web.Builders
{
    using Extensions.Core.Builders;

    internal class WebBuilder : AbstractExtensionBuilder, IWebBuilder
    {
        public WebBuilder(IExtensionBuilder parentBuilder, WebBuilderDependency dependency)
            : base(parentBuilder, dependency)
        {
            Services.AddSingleton<IWebBuilder>(this);
        }


        public bool SupportedGenericController { get; internal set; }

        public Type UserType { get; internal set; }
    }
}
