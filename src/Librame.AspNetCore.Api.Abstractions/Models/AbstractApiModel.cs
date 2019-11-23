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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Api
{
    /// <summary>
    /// 抽象 API 模型。
    /// </summary>
    public abstract class AbstractApiModel
    {
        private static readonly List<Exception> _errors
            = new List<Exception>();


        /// <summary>
        /// 构造一个 <see cref="AbstractApiModel"/>。
        /// </summary>
        public AbstractApiModel()
        {
            if (_errors.Count > 0)
                _errors.Clear();
        }


        /// <summary>
        /// 错误列表。
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public List<Exception> Errors
            => _errors;

        /// <summary>
        /// 存在错误。
        /// </summary>
        public bool IsError { get; set; }
            = _errors.Count > 0;

        /// <summary>
        /// 消息。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 重定向 URL。
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string RedirectUrl { get; set; }
    }
}
