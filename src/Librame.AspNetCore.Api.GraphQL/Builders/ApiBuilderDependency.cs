#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

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

    }
}
