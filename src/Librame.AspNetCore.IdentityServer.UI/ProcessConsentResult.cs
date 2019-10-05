// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Librame.AspNetCore.IdentityServer.UI
{
    using Extensions;

    /// <summary>
    /// 处理批准结果。
    /// </summary>
    public class ProcessConsentResult
    {
        /// <summary>
        /// 是重定向。
        /// </summary>
        public bool IsRedirect
            => RedirectUri.IsNotEmpty();

        /// <summary>
        /// 重定向 URI。
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// 客户端标识。
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 显示视图。
        /// </summary>
        public bool ShowView
            => ViewModel.IsNotNull();

        /// <summary>
        /// 批准视图模型。
        /// </summary>
        public ConsentViewModel ViewModel { get; set; }

        /// <summary>
        /// 有验证错误。
        /// </summary>
        public bool HasValidationError
            => ValidationError.IsNotEmpty();

        /// <summary>
        /// 验证错误。
        /// </summary>
        public string ValidationError { get; set; }
    }
}
