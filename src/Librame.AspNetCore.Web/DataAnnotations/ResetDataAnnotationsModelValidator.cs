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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Librame.AspNetCore.Web.DataAnnotations
{
    using Extensions;

    /// <summary>
    /// 重置数据注释模型验证器。
    /// </summary>
    internal class ResetDataAnnotationsModelValidator : IModelValidator
    {
        private static readonly object _emptyValidationContextInstance = new object();
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IValidationAttributeAdapterProvider _validationAttributeAdapterProvider;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;


        /// <summary>
        /// 构造一个 <see cref="ResetDataAnnotationsModelValidator"/> 实例。
        /// </summary>
        /// <param name="validationAttributeAdapterProvider">给定的 <see cref="IValidationAttributeAdapterProvider"/>。</param>
        /// <param name="attribute">给定的 <see cref="ValidationAttribute"/>。</param>
        /// <param name="stringLocalizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <param name="stringLocalizerFactory">给定的 <see cref="IStringLocalizerFactory"/>。</param>
        public ResetDataAnnotationsModelValidator(
            IValidationAttributeAdapterProvider validationAttributeAdapterProvider,
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer,
            IStringLocalizerFactory stringLocalizerFactory)
        {
            Attribute = attribute.NotNull(nameof(attribute));
            _validationAttributeAdapterProvider = validationAttributeAdapterProvider
                .NotNull(nameof(validationAttributeAdapterProvider));
            _stringLocalizer = stringLocalizer;
            _stringLocalizerFactory = stringLocalizerFactory;
        }


        /// <summary>
        /// 验证特性。
        /// </summary>
        public ValidationAttribute Attribute { get; }


        /// <summary>
        /// 验证模型。
        /// </summary>
        /// <param name="validationContext">给定的 <see cref="ModelValidationContext"/>。</param>
        /// <returns>返回 <see cref="IEnumerable{ModelValidationResult}"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext validationContext)
        {
            validationContext.NotNull(nameof(validationContext));
            
            if (validationContext.ModelMetadata == null)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "The '{0}' property of '{1}' must not be null.",
                        nameof(validationContext.ModelMetadata),
                        typeof(ModelValidationContext)),
                    nameof(validationContext));
            }

            if (validationContext.MetadataProvider == null)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "The '{0}' property of '{1}' must not be null.",
                        nameof(validationContext.MetadataProvider),
                        typeof(ModelValidationContext)),
                    nameof(validationContext));
            }

            var metadata = validationContext.ModelMetadata;
            var memberName = metadata.Name;
            var container = validationContext.Container;

            var context = new ValidationContext(
                instance: container ?? validationContext.Model ?? _emptyValidationContextInstance,
                serviceProvider: validationContext.ActionContext?.HttpContext?.RequestServices,
                items: null)
            {
                DisplayName = metadata.GetDisplayName(),
                MemberName = memberName
            };

            // 解决视图页在 POST 取消后重新绑定模型会抛出找不到可访问静态属性名的异常
            if (validationContext.Model.IsNull())
                return Enumerable.Empty<ModelValidationResult>();

            var result = Attribute.GetValidationResult(validationContext.Model, context);
            if (result == ValidationResult.Success)
                return Enumerable.Empty<ModelValidationResult>();

            string errorMessage;

            if (_stringLocalizer != null &&
                !string.IsNullOrEmpty(Attribute.ErrorMessage) &&
                string.IsNullOrEmpty(Attribute.ErrorMessageResourceName) &&
                Attribute.ErrorMessageResourceType == null)
            {
                errorMessage = GetErrorMessage(validationContext) ?? result.ErrorMessage;
            }
            else
            {
                errorMessage = result.ErrorMessage;
            }

            var validationResults = new List<ModelValidationResult>();
            if (result.MemberNames != null)
            {
                foreach (var resultMemberName in result.MemberNames)
                {
                    // ModelValidationResult.MemberName is used by invoking validators (such as ModelValidator) to
                    // append construct the ModelKey for ModelStateDictionary. When validating at type level we
                    // want the returned MemberNames if specified (e.g. "person.Address.FirstName"). For property
                    // validation, the ModelKey can be constructed using the ModelMetadata and we should ignore
                    // MemberName (we don't want "person.Name.Name"). However the invoking validator does not have
                    // a way to distinguish between these two cases. Consequently we'll only set MemberName if this
                    // validation returns a MemberName that is different from the property being validated.
                    var newMemberName = string.Equals(resultMemberName, memberName, StringComparison.Ordinal) ?
                        null :
                        resultMemberName;
                    var validationResult = new ModelValidationResult(newMemberName, errorMessage);

                    validationResults.Add(validationResult);
                }
            }

            if (validationResults.Count == 0)
            {
                // result.MemberNames was null or empty.
                validationResults.Add(new ModelValidationResult(memberName: null, message: errorMessage));
            }

            return validationResults;
        }

        private string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            var adapter = _validationAttributeAdapterProvider.GetAttributeAdapter(Attribute, _stringLocalizer);
            return adapter?.GetErrorMessage(validationContext);
        }
    }
}
