#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;

namespace Librame.AspNetCore
{
    using Extensions.Core;

    class ApplicationBuilderDecorator : AbstractDecorator<IApplicationBuilder>, IApplicationBuilderDecorator
    {
        public ApplicationBuilderDecorator(IApplicationBuilder source)
            : base(source)
        {
        }

    }
}
