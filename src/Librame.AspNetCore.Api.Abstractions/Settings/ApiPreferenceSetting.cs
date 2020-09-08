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
        /// 模型类型名称工厂方法（支持省略类型名称的模型类型、或类型后缀，如：TestModelType/TestType => Test）。
        /// </summary>
        public Func<Type, string> ModelTypeNameFactory
            => type =>
            {
                type.NotNull(nameof(type));

                if (type.Name.EndsWith("ModelType", StringComparison.OrdinalIgnoreCase))
                    return type.Name.TrimEnd("ModelType", loops: false);

                if (type.Name.EndsWith("Type", StringComparison.OrdinalIgnoreCase))
                    return type.Name.TrimEnd("Type", loops: false);

                return type.Name;
            };

        /// <summary>
        /// 输入模型类型名称工厂方法（支持省略类型名称的模型类型、或类型后缀，如：TestInputModelType/TestInputType => TestInput）。
        /// </summary>
        public Func<Type, string> InputModelTypeNameFactory
            => ModelTypeNameFactory; // 保留 Input 后缀以示区别
    }
}
