// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Mvc;

namespace Librame.Utility
{
    /// <summary>
    /// 表示一个执行时将产生未通过验证(412)的 HTTP 响应。
    /// </summary>
    /// <author>Librame Pang</author>
    public class NotPassValidationResult : HttpStatusCodeResult
    {
        /// <summary>
        /// 构造一个未通过验证(412)的 HTTP 响应实例。
        /// </summary>
        public NotPassValidationResult()
            : base(412)
        {
        }
    }
}