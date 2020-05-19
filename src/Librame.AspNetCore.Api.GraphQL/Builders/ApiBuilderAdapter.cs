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

    internal class ApiBuilderAdapter : AbstractExtensionBuilderAdapter<IApiBuilder>, IApiBuilder
    {
        public ApiBuilderAdapter(IExtensionBuilder parentBuilder, IApiBuilder apiBuilder)
            : base(parentBuilder, apiBuilder)
        {
            Services.AddSingleton<IExtensionBuilderAdapter<IApiBuilder>>(this);
        }

    }
}
