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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 重置比较特性适配器。
    /// </summary>
    public class ResetCompareAttributeAdapter : AttributeAdapterBase<CompareAttribute>
    {
        private readonly string _otherProperty;


        /// <summary>
        /// 构造一个 <see cref="ResetCompareAttributeAdapter"/> 实例。
        /// </summary>
        /// <param name="attribute">给定的 <see cref="CompareAttribute"/>。</param>
        /// <param name="stringLocalizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <param name="stringLocalizerFactory">给定的 <see cref="IStringLocalizerFactory"/>。</param>
        public ResetCompareAttributeAdapter(CompareAttribute attribute, IStringLocalizer stringLocalizer,
            IStringLocalizerFactory stringLocalizerFactory)
            : base(new CompareAttributeWrapper(attribute, stringLocalizerFactory), stringLocalizer)
        {
            _otherProperty = "*." + attribute.OtherProperty;
        }


        /// <summary>
        /// 添加验证。
        /// </summary>
        /// <param name="context">给定的 <see cref="ClientModelValidationContext"/>。</param>
        public override void AddValidation(ClientModelValidationContext context)
        {
            context.NotNull(nameof(context));

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-equalto", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-equalto-other", _otherProperty);
        }

        
        /// <summary>
        /// 获取错误消息。
        /// </summary>
        /// <param name="validationContext">给定的 <see cref="ModelValidationContextBase"/>。</param>
        /// <returns>返回字符串。</returns>
        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            validationContext.NotNull(nameof(validationContext));

            var displayName = validationContext.ModelMetadata.GetDisplayName();
            var otherPropertyDisplayName = CompareAttributeWrapper.GetOtherPropertyDisplayName(
                validationContext,
                Attribute);

            ((CompareAttributeWrapper)Attribute).ValidationContext = validationContext;

            return GetErrorMessage(validationContext.ModelMetadata, displayName, otherPropertyDisplayName);
        }


        // TODO: This entire class is needed because System.ComponentModel.DataAnnotations.CompareAttribute doesn't
        // populate OtherPropertyDisplayName until you call FormatErrorMessage.
        private sealed class CompareAttributeWrapper : CompareAttribute
        {
            private readonly IStringLocalizerFactory _stringLocalizerFactory;

            public ModelValidationContextBase ValidationContext { get; set; }

            public CompareAttributeWrapper(CompareAttribute attribute, IStringLocalizerFactory stringLocalizerFactory)
                : base(attribute.OtherProperty)
            {
                _stringLocalizerFactory = stringLocalizerFactory;

                // Copy settable properties from wrapped attribute. Don't reset default message accessor (set as
                // CompareAttribute constructor calls ValidationAttribute constructor) when all properties are null to
                // preserve default error message. Reset the message accessor when just ErrorMessageResourceType is
                // non-null to ensure correct InvalidOperationException.
                if (!string.IsNullOrEmpty(attribute.ErrorMessage) ||
                    !string.IsNullOrEmpty(attribute.ErrorMessageResourceName) ||
                    attribute.ErrorMessageResourceType != null)
                {
                    ErrorMessage = attribute.ErrorMessage;
                    ErrorMessageResourceName = attribute.ErrorMessageResourceName;
                    ErrorMessageResourceType = attribute.ErrorMessageResourceType;
                }
            }

            public override string FormatErrorMessage(string name)
            {
                var displayName = ValidationContext.ModelMetadata.GetDisplayName();
                var errorMessageString = string.Empty;

                if (ErrorMessageResourceType != null)
                {
                    var resourceTypeLocalizer = _stringLocalizerFactory.Create(ErrorMessageResourceType);
                    errorMessageString = resourceTypeLocalizer[displayName];
                }
                else
                {
                    errorMessageString = ErrorMessageString;
                }

                return string.Format(CultureInfo.CurrentCulture,
                                     errorMessageString,
                                     displayName,
                                     GetOtherPropertyDisplayName(ValidationContext, this));
            }

            public static string GetOtherPropertyDisplayName(
                ModelValidationContextBase validationContext,
                CompareAttribute attribute)
            {
                // The System.ComponentModel.DataAnnotations.CompareAttribute doesn't populate the
                // OtherPropertyDisplayName until after IsValid() is called. Therefore, at the time we get
                // the error message for client validation, the display name is not populated and won't be used.
                var otherPropertyDisplayName = attribute.OtherPropertyDisplayName;
                if (otherPropertyDisplayName == null && validationContext.ModelMetadata.ContainerType != null)
                {
                    var otherProperty = validationContext.MetadataProvider.GetMetadataForProperty(
                        validationContext.ModelMetadata.ContainerType,
                        attribute.OtherProperty);
                    if (otherProperty != null)
                    {
                        return otherProperty.GetDisplayName();
                    }
                }

                return attribute.OtherProperty;
            }

        }

    }
}