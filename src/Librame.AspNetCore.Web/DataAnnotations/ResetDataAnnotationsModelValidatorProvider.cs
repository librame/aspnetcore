#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.DataAnnotations
{
    using Extensions;

    /// <summary>
    /// 重置数据注释集合模型验证器提供程序。
    /// </summary>
    internal class ResetDataAnnotationsModelValidatorProvider : IMetadataBasedModelValidatorProvider
    {
        private readonly IValidationAttributeAdapterProvider _validationAttributeAdapterProvider;
        private readonly IOptions<MvcDataAnnotationsLocalizationOptions> _options;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;


        /// <summary>
        /// 创建一个 <see cref="ResetDataAnnotationsModelValidatorProvider"/> 实例。
        /// </summary>
        /// <param name="validationAttributeAdapterProvider">给定的 <see cref="IValidationAttributeAdapterProvider"/>。</param>
        /// <param name="options">给定的 <see cref="IOptions{MvcDataAnnotationsLocalizationOptions}"/>。</param>
        /// <param name="stringLocalizerFactory">给定的 <see cref="IStringLocalizerFactory"/>。</param>
        public ResetDataAnnotationsModelValidatorProvider(
            IValidationAttributeAdapterProvider validationAttributeAdapterProvider,
            IOptions<MvcDataAnnotationsLocalizationOptions> options,
            IStringLocalizerFactory stringLocalizerFactory)
        {
            _validationAttributeAdapterProvider = validationAttributeAdapterProvider
                .NotNull(nameof(validationAttributeAdapterProvider));

            _options = options.NotNull(nameof(options));
            _stringLocalizerFactory = stringLocalizerFactory;
        }


        /// <summary>
        /// 创建验证器集合。
        /// </summary>
        /// <param name="context">给定的 <see cref="ModelValidatorProviderContext"/>。</param>
        public void CreateValidators(ModelValidatorProviderContext context)
        {
            IStringLocalizer stringLocalizer = null;
            if (_stringLocalizerFactory != null && _options.Value.DataAnnotationLocalizerProvider != null)
            {
                stringLocalizer = _options.Value.DataAnnotationLocalizerProvider(
                    context.ModelMetadata.ContainerType ?? context.ModelMetadata.ModelType,
                    _stringLocalizerFactory);
            }

            for (var i = 0; i < context.Results.Count; i++)
            {
                var validatorItem = context.Results[i];
                if (validatorItem.Validator != null)
                    continue;

                if (!(validatorItem.ValidatorMetadata is ValidationAttribute attribute))
                    continue;

                var validator = new ResetDataAnnotationsModelValidator(
                    _validationAttributeAdapterProvider,
                    attribute,
                    stringLocalizer,
                    _stringLocalizerFactory);

                validatorItem.Validator = validator;
                validatorItem.IsReusable = true;
                // Inserts validators based on whether or not they are 'required'. We want to run
                // 'required' validators first so that we get the best possible error message.
                if (attribute is RequiredAttribute)
                {
                    context.Results.Remove(validatorItem);
                    context.Results.Insert(0, validatorItem);
                }
            }

            // Produce a validator if the type supports IValidatableObject
            if (typeof(IValidatableObject).IsAssignableFrom(context.ModelMetadata.ModelType))
            {
                context.Results.Add(new ValidatorItem
                {
                    Validator = new ResetValidatableObjectAdapter(),
                    IsReusable = true
                });
            }
        }


        /// <summary>
        /// 包含验证器集合。
        /// </summary>
        /// <param name="modelType">给定的模型类型。</param>
        /// <param name="validatorMetadata">给定的验证器集合元数据。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public bool HasValidators(Type modelType, IList<object> validatorMetadata)
        {
            validatorMetadata.NotNull(nameof(validatorMetadata));

            if (typeof(IValidatableObject).IsAssignableFrom(modelType))
                return true;

            for (var i = 0; i < validatorMetadata.Count; i++)
            {
                if (validatorMetadata[i] is ValidationAttribute)
                    return true;
            }

            return false;
        }

    }
}
