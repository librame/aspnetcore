#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
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
        public ApiBuilderDependency()
            : base(nameof(ApiBuilderDependency))
        {
        }

    }
}
