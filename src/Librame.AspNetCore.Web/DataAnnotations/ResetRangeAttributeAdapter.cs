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
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Librame.AspNetCore.Web.DataAnnotations
{
    using Extensions;

    /// <summary>
    /// 重置范围特性适配器。
    /// </summary>
    internal class ResetRangeAttributeAdapter : AttributeAdapterBase<RangeAttribute>
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly string _max;
        private readonly string _min;


        /// <summary>
        /// 构造一个 <see cref="ResetRangeAttributeAdapter"/> 实例。
        /// </summary>
        /// <param name="attribute">给定的 <see cref="RangeAttribute"/>。</param>
        /// <param name="stringLocalizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <param name="stringLocalizerFactory">给定的 <see cref="IStringLocalizerFactory"/>。</param>
        public ResetRangeAttributeAdapter(RangeAttribute attribute, IStringLocalizer stringLocalizer,
            IStringLocalizerFactory stringLocalizerFactory)
            : base(attribute, stringLocalizer)
        {
            _stringLocalizerFactory = stringLocalizerFactory.NotNull(nameof(stringLocalizerFactory));

            // This will trigger the conversion of Attribute.Minimum and Attribute.Maximum.
            // This is needed, because the attribute is stateful and will convert from a string like
            // "100m" to the decimal value 100.
            //
            // Validate a randomly selected number.
            attribute.IsValid(3); 

            _max = Convert.ToString(Attribute.Maximum, CultureInfo.InvariantCulture);
            _min = Convert.ToString(Attribute.Minimum, CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// 添加验证。
        /// </summary>
        /// <param name="context">给定的 <see cref="ClientModelValidationContext"/>。</param>
        public override void AddValidation(ClientModelValidationContext context)
        {
            context.NotNull(nameof(context));

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-range", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-range-max", _max);
            MergeAttribute(context.Attributes, "data-val-range-min", _min);
        }


        /// <summary>
        /// 获取错误消息。
        /// </summary>
        /// <param name="validationContext">给定的 <see cref="ModelValidationContextBase"/>。</param>
        /// <returns>返回字符串。</returns>
        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            validationContext.NotNull(nameof(validationContext));

            return GetErrorMessage(validationContext.ModelMetadata,
                validationContext.ModelMetadata.DisplayName, Attribute.Maximum, Attribute.Minimum);
        }

        /// <summary>
        /// 获取错误消息。
        /// </summary>
        /// <param name="modelMetadata">给定的 <see cref="ModelMetadata"/>。</param>
        /// <param name="arguments">给定的参数对象数组。</param>
        /// <returns>返回字符串。</returns>
        protected override string GetErrorMessage(ModelMetadata modelMetadata, params object[] arguments)
        {
            if (Attribute.ErrorMessageResourceType.IsNotNull())
                return Attribute.FormatErrorMessage(_stringLocalizerFactory, modelMetadata, arguments);

            return Attribute.FormatErrorMessage(modelMetadata.DisplayName);
        }

    }
}