#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    static class ValidationAttributeExtensions
    {
        public static string FormatErrorMessage(this ValidationAttribute attribute, IStringLocalizerFactory localizerFactory,
            ModelMetadata modelMetadata, params object[] args)
        {
            attribute.NotNull(nameof(attribute));
            localizerFactory.NotNull(nameof(localizerFactory));

            var localizer = localizerFactory.Create(attribute.ErrorMessageResourceType);

            return attribute.FormatErrorMessage(localizer, modelMetadata, args);
        }

        public static string FormatErrorMessage(this ValidationAttribute attribute, IStringLocalizer localizer,
            ModelMetadata modelMetadata, params object[] args)
        {
            attribute.NotNull(nameof(attribute));
            localizer.NotNull(nameof(localizer));
            modelMetadata.NotNull(nameof(modelMetadata));

            var formatErrorMessage = localizer[attribute.ErrorMessageResourceName ?? modelMetadata.Name];

            if (args.IsEmpty()) args = new object[] { modelMetadata.DisplayName };

            return string.Format(CultureInfo.CurrentCulture, formatErrorMessage, args);
        }

    }
}
