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
    /// 重置最大长度特性适配器。
    /// </summary>
    public class ResetMaxLengthAttributeAdapter : AttributeAdapterBase<MaxLengthAttribute>
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly string _max;


        /// <summary>
        /// 构造一个 <see cref="ResetMaxLengthAttributeAdapter"/> 实例。
        /// </summary>
        /// <param name="attribute">给定的 <see cref="MaxLengthAttribute"/>。</param>
        /// <param name="stringLocalizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <param name="stringLocalizerFactory">给定的 <see cref="IStringLocalizerFactory"/>。</param>
        public ResetMaxLengthAttributeAdapter(MaxLengthAttribute attribute, IStringLocalizer stringLocalizer,
            IStringLocalizerFactory stringLocalizerFactory)
            : base(attribute, stringLocalizer)
        {
            _stringLocalizerFactory = stringLocalizerFactory.NotNull(nameof(stringLocalizerFactory));
            _max = Attribute.Length.ToString(CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// 添加验证。
        /// </summary>
        /// <param name="context">给定的 <see cref="ClientModelValidationContext"/>。</param>
        public override void AddValidation(ClientModelValidationContext context)
        {
            context.NotNull(nameof(context));

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-maxlength", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-maxlength-max", _max);
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
                validationContext.ModelMetadata.DisplayName, Attribute.Length);
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
