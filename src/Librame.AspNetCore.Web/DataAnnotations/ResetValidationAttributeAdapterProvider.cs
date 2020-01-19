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
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace Librame.AspNetCore.Web.DataAnnotations
{
    using Extensions;

    /// <summary>
    /// 重置验证特性适配器提供程序。
    /// </summary>
    public class ResetValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;


        /// <summary>
        /// 构造一个 <see cref="ResetValidationAttributeAdapterProvider"/> 实例。
        /// </summary>
        /// <param name="stringLocalizerFactory">给定的 <see cref="IStringLocalizerFactory"/>。</param>
        public ResetValidationAttributeAdapterProvider(IStringLocalizerFactory stringLocalizerFactory)
        {
            _stringLocalizerFactory = stringLocalizerFactory.NotNull(nameof(stringLocalizerFactory));
        }


        /// <summary>
        /// 为给定属性创建特性适配器。
        /// </summary>
        /// <param name="attribute">给定的 <see cref="ValidationAttribute"/>。</param>
        /// <param name="stringLocalizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <returns>返回 <see cref="IAttributeAdapter"/>。</returns>
        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            IAttributeAdapter adapter;

            if (attribute is RegularExpressionAttribute regularExpressionAttribute)
            {
                adapter = new ResetRegularExpressionAttributeAdapter(regularExpressionAttribute, stringLocalizer, _stringLocalizerFactory);
            }
            else if (attribute is MaxLengthAttribute maxLengthAttribute)
            {
                adapter = new ResetMaxLengthAttributeAdapter(maxLengthAttribute, stringLocalizer, _stringLocalizerFactory);
            }
            else if (attribute is RequiredAttribute requiredAttribute)
            {
                adapter = new ResetRequiredAttributeAdapter(requiredAttribute, stringLocalizer, _stringLocalizerFactory);
            }
            else if (attribute is CompareAttribute compareAttribute)
            {
                adapter = new ResetCompareAttributeAdapter(compareAttribute, stringLocalizer, _stringLocalizerFactory);
            }
            else if (attribute is MinLengthAttribute minLengthAttribute)
            {
                adapter = new ResetMinLengthAttributeAdapter(minLengthAttribute, stringLocalizer, _stringLocalizerFactory);
            }
            else if (attribute is CreditCardAttribute creditCardAttribute)
            {
                adapter = new ResetDataTypeAttributeAdapter(creditCardAttribute, "data-val-creditcard", stringLocalizer, _stringLocalizerFactory);
            }
            else if (attribute is StringLengthAttribute stringLengthAttribute)
            {
                adapter = new ResetStringLengthAttributeAdapter(stringLengthAttribute, stringLocalizer, _stringLocalizerFactory);
            }
            else if (attribute is RangeAttribute rangeAttribute)
            {
                adapter = new ResetRangeAttributeAdapter(rangeAttribute, stringLocalizer, _stringLocalizerFactory);
            }
            else if (attribute is EmailAddressAttribute emailAddressAttribute)
            {
                adapter = new ResetDataTypeAttributeAdapter(emailAddressAttribute, "data-val-email", stringLocalizer, _stringLocalizerFactory);
            }
            else if (attribute is PhoneAttribute phoneAttribute)
            {
                adapter = new ResetDataTypeAttributeAdapter(phoneAttribute, "data-val-phone", stringLocalizer, _stringLocalizerFactory);
            }
            else if (attribute is UrlAttribute urlAttribute)
            {
                adapter = new ResetDataTypeAttributeAdapter(urlAttribute, "data-val-url", stringLocalizer, _stringLocalizerFactory);
            }
            else if (attribute is FileExtensionsAttribute fileExtensionsAttribute)
            {
                adapter = new ResetFileExtensionsAttributeAdapter(fileExtensionsAttribute, stringLocalizer, _stringLocalizerFactory);
            }
            else
            {
                adapter = null;
            }

            return adapter;
        }

    }
}
