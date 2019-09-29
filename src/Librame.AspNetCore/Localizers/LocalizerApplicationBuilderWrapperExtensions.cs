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
using System;

namespace Librame.AspNetCore
{
    static class LocalizerApplicationBuilderWrapperExtensions
    {
        public static IApplicationBuilderWrapper UseLocalization(this IApplicationBuilderWrapper builderWrapper,
            Action<RequestLocalizationOptions> optionsAction = null)
        {
            //builderWrapper.RawBuilder.UseRequestLocalization(optionsAction ?? (_ => { }));
            builderWrapper.RawBuilder.UseMiddleware<RequestLocalizationMiddleware>();

            return builderWrapper;
        }

    }
}
