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
    /// 内部数据注释用户界面构建器静态扩展。
    /// </summary>
    internal static class InternalDataAnnotationUserInterfaceBuilderExtensions
    {
        /// <summary>
        /// 添加数据注释集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUserInterfaceBuilder"/>。</param>
        /// <returns>返回 <see cref="IUserInterfaceBuilder"/>。</returns>
        public static IUserInterfaceBuilder AddDataAnnotations(this IUserInterfaceBuilder builder)
        {
            builder.Services.TryReplace(typeof(IConfigureOptions<MvcOptions>), typeof(MvcDataAnnotationsMvcOptionsSetup),
                typeof(ResetMvcDataAnnotationsMvcOptionsSetup));
            builder.Services.TryReplace<IValidationAttributeAdapterProvider, ResetValidationAttributeAdapterProvider>();

            return builder;
        }

    }
}
