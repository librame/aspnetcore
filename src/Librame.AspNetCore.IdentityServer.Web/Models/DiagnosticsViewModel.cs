// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.IdentityServer.Web.Models
{
    using Extensions;
    using Extensions.Core.Builders;

    /// <summary>
    /// 诊断视图模型。
    /// </summary>
    public class DiagnosticsViewModel
    {
        /// <summary>
        /// 构造一个 <see cref="DiagnosticsViewModel"/>。
        /// </summary>
        /// <param name="result">给定的 <see cref="Microsoft.AspNetCore.Authentication.AuthenticateResult"/>。</param>
        /// <param name="coreOptions">给定的 <see cref="CoreBuilderOptions"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public DiagnosticsViewModel(AuthenticateResult result, CoreBuilderOptions coreOptions)
        {
            AuthenticateResult = result.NotNull(nameof(result));

            if (result.Properties.Items.ContainsKey("client_list"))
            {
                var encoded = result.Properties.Items["client_list"];
                var buffer = Base64Url.Decode(encoded);

                Clients = JsonConvert.DeserializeObject<string[]>(buffer.AsEncodingString(coreOptions.Encoding));
            }
        }


        /// <summary>
        /// 认证结果。
        /// </summary>
        public AuthenticateResult AuthenticateResult { get; }

        /// <summary>
        /// 客户端集合。
        /// </summary>
        public IEnumerable<string> Clients { get; }
            = new List<string>();
    }
}