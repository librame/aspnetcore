#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Librame.AspNetCore.UI
{
    static class LocalizerUiBuilderExtensions
    {
        internal static IUiBuilder AddLocalizers(this IUiBuilder builder)
        {
            builder.Services.TryAddTransient(typeof(IDictionaryHtmlLocalizer<>), typeof(DictionaryHtmlLocalizer<>));
            builder.Services.TryAddSingleton<IDictionaryHtmlLocalizerFactory, DictionaryHtmlLocalizerFactory>();

            return builder;
        }

    }
}
