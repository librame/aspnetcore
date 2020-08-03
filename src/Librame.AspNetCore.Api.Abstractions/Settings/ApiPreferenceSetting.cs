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
    /// API 偏好设置。
    /// </summary>
    public class ApiPreferenceSetting : AbstractPreferenceSetting, IApiPreferenceSetting
    {
        /// <summary>
        /// 限定请求路径。
        /// </summary>
        public string RestrictRequestPath
            => "/api";

        /// <summary>
        /// 限定请求方法列表。
        /// </summary>
        public IReadOnlyList<string> RestrictRequestMethods
            => new List<string> { "GET", "POST" };

        /// <summary>
        /// 响应内容类型。
        /// </summary>
        public string ResponseContentType
            => "application/json";


        /// <summary>
        /// API 类型名称工厂方法（如：ApiType => Api）。
        /// </summary>
        public Func<Type, string> ApiTypeNameFactory
            => type => type?.Name.TrimEnd(nameof(Type));

        /// <summary>
        /// API 输入类型名称工厂方法（如：ApiInputType => ApiInput）。
        /// </summary>
        public Func<Type, string> ApiInputTypeNameFactory
            => type => type?.Name.TrimEnd(nameof(Type));
    }
}
