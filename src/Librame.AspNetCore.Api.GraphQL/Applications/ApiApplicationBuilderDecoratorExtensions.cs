#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Applications;
using Librame.AspNetCore.Api.Applications;
using Librame.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// API <see cref="IApplicationBuilderDecorator"/> 静态扩展。
    /// </summary>
    public static class ApiApplicationBuilderDecoratorExtensions
    {
        /// <summary>
        /// 使用 API 应用。
        /// </summary>
        /// <param name="decorator">给定的 <see cref="IApplicationBuilderDecorator"/>。</param>
        /// <returns>返回 <see cref="IApplicationBuilderDecorator"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IApplicationBuilderDecorator UseApi(this IApplicationBuilderDecorator decorator)
        {
            decorator.NotNull(nameof(decorator));

            decorator.Source.UseMiddleware<ApiApplicationMiddleware>();

            return decorator;
        }

    }
}
