#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Librame.AspNetCore.Web.Builders
{
    using DataAnnotations;

    internal static class DataAnnotationWebBuilderExtensions
    {
        public static IWebBuilder AddDataAnnotations(this IWebBuilder builder)
        {
            builder.Services.TryReplace<IValidationAttributeAdapterProvider, ResetValidationAttributeAdapterProvider>();

            var mvcDataAnnotationsMvcOptionsSetupType = typeof(IValidationAttributeAdapterProvider).Assembly
                .GetType("Microsoft.Extensions.DependencyInjection.MvcDataAnnotationsMvcOptionsSetup");
            builder.Services.TryReplace(typeof(IConfigureOptions<MvcOptions>), mvcDataAnnotationsMvcOptionsSetupType,
                typeof(ResetMvcDataAnnotationsMvcOptionsSetup));

            return builder;
        }

    }
}
