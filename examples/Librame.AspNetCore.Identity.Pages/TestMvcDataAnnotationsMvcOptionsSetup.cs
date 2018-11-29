// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Librame.AspNetCore.Identity.Pages
{
    /// <summary>
    /// Sets up default options for <see cref="MvcOptions"/>.
    /// </summary>
    public class TestMvcDataAnnotationsMvcOptionsSetup : IConfigureOptions<MvcOptions>
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly IValidationAttributeAdapterProvider _validationAttributeAdapterProvider;
        private readonly IOptions<MvcDataAnnotationsLocalizationOptions> _dataAnnotationLocalizationOptions;

        public TestMvcDataAnnotationsMvcOptionsSetup(
            IValidationAttributeAdapterProvider validationAttributeAdapterProvider,
            IOptions<MvcDataAnnotationsLocalizationOptions> dataAnnotationLocalizationOptions)
        {
            if (validationAttributeAdapterProvider == null)
            {
                throw new ArgumentNullException(nameof(validationAttributeAdapterProvider));
            }

            if (dataAnnotationLocalizationOptions == null)
            {
                throw new ArgumentNullException(nameof(dataAnnotationLocalizationOptions));
            }

            _validationAttributeAdapterProvider = validationAttributeAdapterProvider;
            _dataAnnotationLocalizationOptions = dataAnnotationLocalizationOptions;
        }

        public TestMvcDataAnnotationsMvcOptionsSetup(
            IValidationAttributeAdapterProvider validationAttributeAdapterProvider,
            IOptions<MvcDataAnnotationsLocalizationOptions> dataAnnotationLocalizationOptions,
            IStringLocalizerFactory stringLocalizerFactory)
            : this(validationAttributeAdapterProvider, dataAnnotationLocalizationOptions)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        public void Configure(MvcOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.ModelMetadataDetailsProviders.Add(new TestDataAnnotationsMetadataProvider(
                _dataAnnotationLocalizationOptions,
                _stringLocalizerFactory));

            options.ModelValidatorProviders.Add(new DataAnnotationsModelValidatorProvider(
                _validationAttributeAdapterProvider,
                _dataAnnotationLocalizationOptions,
                _stringLocalizerFactory));
        }
    }
}