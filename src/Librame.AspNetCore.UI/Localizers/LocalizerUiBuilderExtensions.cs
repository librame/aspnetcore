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

namespace Librame.AspNetCore.UI
{
    static class LocalizerUiBuilderExtensions
    {
        public static IUiBuilder AddLocalizers(this IUiBuilder builder)
        {
            builder.Services.AddTransient(typeof(IExpressionHtmlLocalizer<>), typeof(ExpressionHtmlLocalizer<>));

            return builder;
        }

    }
}
