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

namespace Librame.AspNetCore.Api
{
    using Extensions.Core;

    class ApiBuilder : AbstractExtensionBuilder, IApiBuilder
    {
        public ApiBuilder(IExtensionBuilder builder, ApiBuilderDependencyOptions dependencyOptions)
            : base(builder, dependencyOptions)
        {
            Services.AddSingleton<IApiBuilder>(this);
        }

    }
}
