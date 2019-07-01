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
    /// <summary>
    /// 验证特性静态扩展。
    /// </summary>
    internal static class ValidationAttributeExtensions
    {

        /// <summary>
        /// 格式化错误消息。
        /// </summary>
        /// <param name="attribute">给定的 <see cref="ValidationAttribute"/>。</param>
        /// <param name="localizerFactory">给定的 <see cref="IStringLocalizerFactory"/>。</param>
        /// <param name="modelMetadata">给定的 <see cref="ModelMetadata"/>。</param>
        /// <param name="args">给定的格式化参数对象数组。</param>
        /// <returns>返回字符串。</returns>
        public static string FormatErrorMessage(this ValidationAttribute attribute, IStringLocalizerFactory localizerFactory,
            ModelMetadata modelMetadata, params object[] args)
        {
            attribute.NotNull(nameof(attribute));
            localizerFactory.NotNull(nameof(localizerFactory));

            var localizer = localizerFactory.Create(attribute.ErrorMessageResourceType);

            return attribute.FormatErrorMessage(localizer, modelMetadata, args);
        }

        /// <summary>
        /// 格式化错误消息。
        /// </summary>
        /// <param name="attribute">给定的 <see cref="ValidationAttribute"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <param name="modelMetadata">给定的 <see cref="ModelMetadata"/>。</param>
        /// <param name="args">给定的格式化参数对象数组。</param>
        /// <returns>返回字符串。</returns>
        public static string FormatErrorMessage(this ValidationAttribute attribute, IStringLocalizer localizer,
            ModelMetadata modelMetadata, params object[] args)
        {
            attribute.NotNull(nameof(attribute));
            localizer.NotNull(nameof(localizer));
            modelMetadata.NotNull(nameof(modelMetadata));

            var formatErrorMessage = localizer[attribute.ErrorMessageResourceName ?? modelMetadata.Name];

            if (args.IsNullOrEmpty()) args = new object[] { modelMetadata.DisplayName };

            return string.Format(CultureInfo.CurrentCulture, formatErrorMessage, args);
        }

    }
}
