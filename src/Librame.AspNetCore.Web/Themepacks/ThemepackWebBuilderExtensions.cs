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
    using Themepacks;

    internal static class ThemepackWebBuilderExtensions
    {
        internal static IWebBuilder AddThemepacks(this IWebBuilder builder)
        {
            builder.Services.TryAddSingleton<IThemepackContext, ThemepackContext>();

            return builder;
        }

    }
}
