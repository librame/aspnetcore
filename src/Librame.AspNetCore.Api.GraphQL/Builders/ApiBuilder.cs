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

namespace Librame.AspNetCore.Api.Builders
{
    using Extensions.Core.Builders;

    internal class ApiBuilder : AbstractExtensionBuilder, IApiBuilder
    {
        public ApiBuilder(IExtensionBuilder parentBuilder, ApiBuilderDependency dependency)
            : base(parentBuilder, dependency)
        {
            Services.AddSingleton<IApiBuilder>(this);
        }

    }
}
