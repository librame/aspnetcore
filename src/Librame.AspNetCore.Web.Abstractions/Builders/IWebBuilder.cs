#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Web.Builders
{
    using Extensions.Core.Builders;

    /// <summary>
    /// Web 构建器接口。
    /// </summary>
    public interface IWebBuilder : IExtensionBuilder
    {
        /// <summary>
        /// 支持泛型控制器。
        /// </summary>
        bool SupportedGenericController { get; }
    }
}
