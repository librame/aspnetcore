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
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 内部数据注释 UI 构建器静态扩展。
    /// </summary>
    internal static class InternalDataAnnotationUIBuilderExtensions
    {
        /// <summary>
        /// 添加数据注释集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUIBuilder"/>。</param>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public static IUIBuilder AddDataAnnotations(this IUIBuilder builder)
        {
            builder.Services.TryReplace(typeof(IConfigureOptions<MvcOptions>), typeof(MvcDataAnnotationsMvcOptionsSetup),
                typeof(ResetMvcDataAnnotationsMvcOptionsSetup));
            builder.Services.TryReplace<IValidationAttributeAdapterProvider, ResetValidationAttributeAdapterProvider>();

            return builder;
        }

    }
}
