#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;

namespace Librame.AspNetCore.Api
{
    using Extensions;

    /// <summary>
    /// API 偏好设置接口。
    /// </summary>
    public interface IApiPreferenceSetting : IPreferenceSetting
    {
        /// <summary>
        /// 限定请求路径。
        /// </summary>
        string RestrictRequestPath { get; }

        /// <summary>
        /// 限定请求方法列表。
        /// </summary>
        IReadOnlyList<string> RestrictRequestMethods { get; }

        /// <summary>
        /// 响应内容类型。
        /// </summary>
        string ResponseContentType { get; }


        /// <summary>
        /// API 类型名称工厂方法。
        /// </summary>
        Func<Type, string> ApiTypeNameFactory { get; }

        /// <summary>
        /// API 输入类型名称工厂方法。
        /// </summary>
        Func<Type, string> ApiInputTypeNameFactory { get; }
    }
}
