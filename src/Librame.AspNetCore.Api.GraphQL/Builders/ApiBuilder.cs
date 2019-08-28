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
        public ApiBuilder(IExtensionBuilder builder)
            : base(builder)
        {
            Services.AddSingleton<IApiBuilder>(this);
        }

    }
}
