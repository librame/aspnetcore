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

namespace Librame.AspNetCore.Web.Builders
{
    using Localizers;

    internal static class LocalizerWebBuilderExtensions
    {
        internal static IWebBuilder AddLocalizers(this IWebBuilder builder)
        {
            builder.Services.TryAddTransient(typeof(IDictionaryHtmlLocalizer<>), typeof(DictionaryHtmlLocalizer<>));
            builder.Services.TryAddSingleton<IDictionaryHtmlLocalizerFactory, DictionaryHtmlLocalizerFactory>();

            return builder;
        }

    }
}
