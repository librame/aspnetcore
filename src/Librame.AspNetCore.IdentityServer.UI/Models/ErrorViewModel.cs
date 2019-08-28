// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;

namespace Librame.AspNetCore.IdentityServer.UI
{
    /// <summary>
    /// 错误视图模型。
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// 构造一个 <see cref="ErrorViewModel"/>。
        /// </summary>
        public ErrorViewModel()
        {
        }

        /// <summary>
        /// 构造一个 <see cref="ErrorViewModel"/>。
        /// </summary>
        /// <param name="error">给定的错误。</param>
        public ErrorViewModel(string error)
        {
            Error = new ErrorMessage { Error = error };
        }


        /// <summary>
        /// 错误消息。
        /// </summary>
        public ErrorMessage Error { get; set; }
    }
}