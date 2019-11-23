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
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 重置 MVC 数据注释集合选项安装。
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class ResetMvcDataAnnotationsMvcOptionsSetup : IConfigureOptions<MvcOptions>
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly IValidationAttributeAdapterProvider _validationAttributeAdapterProvider;
        private readonly IOptions<MvcDataAnnotationsLocalizationOptions> _dataAnnotationLocalizationOptions;


        /// <summary>
        /// 重置 MVC 数据注释。
        /// </summary>
        /// <param name="validationAttributeAdapterProvider">给定的 <see cref="IValidationAttributeAdapterProvider"/>。</param>
        /// <param name="dataAnnotationLocalizationOptions">给定的 <see cref="IOptions{MvcDataAnnotationsLocalizationOptions}"/>。</param>
        /// <param name="stringLocalizerFactory">给定的 <see cref="IStringLocalizerFactory"/>。</param>
        public ResetMvcDataAnnotationsMvcOptionsSetup(
            IValidationAttributeAdapterProvider validationAttributeAdapterProvider,
            IOptions<MvcDataAnnotationsLocalizationOptions> dataAnnotationLocalizationOptions,
            IStringLocalizerFactory stringLocalizerFactory)
            : this(validationAttributeAdapterProvider, dataAnnotationLocalizationOptions)
        {
            _stringLocalizerFactory = stringLocalizerFactory.NotNull(nameof(stringLocalizerFactory));
        }

        /// <summary>
        /// 重置 MVC 数据注释。
        /// </summary>
        /// <param name="validationAttributeAdapterProvider">给定的 <see cref="IValidationAttributeAdapterProvider"/>。</param>
        /// <param name="dataAnnotationLocalizationOptions">给定的 <see cref="IOptions{MvcDataAnnotationsLocalizationOptions}"/>。</param>
        protected ResetMvcDataAnnotationsMvcOptionsSetup(
            IValidationAttributeAdapterProvider validationAttributeAdapterProvider,
            IOptions<MvcDataAnnotationsLocalizationOptions> dataAnnotationLocalizationOptions)
        {
            _validationAttributeAdapterProvider = validationAttributeAdapterProvider
                .NotNull(nameof(validationAttributeAdapterProvider));

            _dataAnnotationLocalizationOptions = dataAnnotationLocalizationOptions
                .NotNull(nameof(dataAnnotationLocalizationOptions));
        }


        /// <summary>
        /// 配置 MVC 选项。
        /// </summary>
        /// <param name="options">给定的 <see cref="MvcOptions"/>。</param>
        public void Configure(MvcOptions options)
        {
            options.NotNull(nameof(options));

            options.ModelMetadataDetailsProviders.Add(new ResetDataAnnotationsMetadataProvider(
                _dataAnnotationLocalizationOptions,
                _stringLocalizerFactory));

            options.ModelValidatorProviders.Add(new ResetDataAnnotationsModelValidatorProvider(
                _validationAttributeAdapterProvider,
                _dataAnnotationLocalizationOptions,
                _stringLocalizerFactory));
        }

    }
}