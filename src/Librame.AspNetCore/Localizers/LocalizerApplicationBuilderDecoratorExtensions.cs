#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
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
