#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

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

        /// <summary>
        /// 用户类型。
        /// </summary>
        Type UserType { get; }
    }
}
