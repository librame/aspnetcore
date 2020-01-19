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

namespace Librame.AspNetCore.Localizers
{
    using Applications;

    internal static class LocalizerApplicationBuilderDecoratorExtensions
    {
        public static IApplicationBuilderDecorator UseLocalization(this IApplicationBuilderDecorator decorator)
        {
            decorator.Source.UseMiddleware<RequestLocalizationMiddleware>();

            return decorator;
        }

    }
}
