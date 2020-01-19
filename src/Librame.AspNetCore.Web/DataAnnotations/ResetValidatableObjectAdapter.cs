#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Librame.AspNetCore.Web.DataAnnotations
{
    /// <summary>
    /// 重置可验证对象适配器。
    /// </summary>
    internal class ResetValidatableObjectAdapter : IModelValidator
    {
        /// <summary>
        /// 验证模型。
        /// </summary>
        /// <param name="context">给定的 <see cref="ModelValidationContext"/>。</param>
        /// <returns>返回 <see cref="IEnumerable{ModelValidationResult}"/>。</returns>
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var model = context.Model;
            if (model == null)
                return Enumerable.Empty<ModelValidationResult>();

            if (!(model is IValidatableObject validatable))
            {
                var message = string.Format(CultureInfo.InvariantCulture, "The model object inside the metadata claimed to be compatible with '{0}', but was actually '{1}'.",
                    typeof(IValidatableObject).Name, model.GetType());

                throw new InvalidOperationException(message);
            }

            // The constructed ValidationContext is intentionally slightly different from what
            // DataAnnotationsModelValidator creates. The instance parameter would be context.Container
            // (if non-null) in that class. But, DataAnnotationsModelValidator _also_ passes context.Model
            // separately to any ValidationAttribute.
            var validationContext = new ValidationContext(
                instance: validatable,
                serviceProvider: context.ActionContext?.HttpContext?.RequestServices,
                items: null)
            {
                DisplayName = context.ModelMetadata.GetDisplayName(),
                MemberName = context.ModelMetadata.Name,
            };

            return ConvertResults(validatable.Validate(validationContext));
        }

        private IEnumerable<ModelValidationResult> ConvertResults(IEnumerable<ValidationResult> results)
        {
            foreach (var result in results)
            {
                if (result != ValidationResult.Success)
                {
                    if (result.MemberNames == null || !result.MemberNames.Any())
                    {
                        yield return new ModelValidationResult(memberName: null, message: result.ErrorMessage);
                    }
                    else
                    {
                        foreach (var memberName in result.MemberNames)
                        {
                            yield return new ModelValidationResult(memberName, result.ErrorMessage);
                        }
                    }
                }
            }
        }

    }
}
