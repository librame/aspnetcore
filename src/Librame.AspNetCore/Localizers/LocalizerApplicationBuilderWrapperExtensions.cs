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
    static class LocalizerApplicationBuilderWrapperExtensions
    {
        public static IApplicationBuilderWrapper UseLocalization(this IApplicationBuilderWrapper builderWrapper)
        {
            builderWrapper.RawBuilder.UseMiddleware<RequestLocalizationMiddleware>();

            return builderWrapper;
        }

    }
}
