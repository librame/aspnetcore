#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Validation.Complexity;
using Microsoft.AspNetCore.Http;
using System;

namespace Librame.AspNetCore.Api.Builders
{
    using Extensions.Core.Builders;

    /// <summary>
    /// API 构建器依赖选项。
    /// </summary>
    public class ApiBuilderDependency : AbstractExtensionBuilderDependency<ApiBuilderOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="ApiBuilderDependency"/>。
        /// </summary>
        /// <param name="parentDependency">给定的父级 <see cref="IExtensionBuilderDependency"/>（可选）。</param>
        public ApiBuilderDependency(IExtensionBuilderDependency parentDependency = null)
            : base(nameof(ApiBuilderDependency), parentDependency)
        {
        }


        /// <summary>
        /// 构建用户上下文（<see cref="HttpContext"/> 可为空）。
        /// </summary>
        public Func<HttpContext, object> BuildUserContext { get; set; }
            = context => new GraphUserContext
            {
                User = context?.User
            };

        /// <summary>
        /// 启用指标（默认启用）。
        /// </summary>
        public bool EnableMetrics { get; set; }
            = true;

        /// <summary>
        /// 复杂度配置。
        /// </summary>
        public ComplexityConfiguration Complexity { get; set; }
            = new ComplexityConfiguration
            {
                MaxDepth = 15
            };
    }
}
