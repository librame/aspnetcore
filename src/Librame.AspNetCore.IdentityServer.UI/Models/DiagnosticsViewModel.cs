// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Librame.AspNetCore.IdentityServer.UI
{
    /// <summary>
    /// 诊断视图模型。
    /// </summary>
    public class DiagnosticsViewModel
    {
        /// <summary>
        /// 构造一个 <see cref="DiagnosticsViewModel"/>。
        /// </summary>
        /// <param name="result">给定的 <see cref="Microsoft.AspNetCore.Authentication.AuthenticateResult"/>。</param>
        public DiagnosticsViewModel(AuthenticateResult result)
        {
            AuthenticateResult = result;

            if (result.Properties.Items.ContainsKey("client_list"))
            {
                var encoded = result.Properties.Items["client_list"];
                var bytes = Base64Url.Decode(encoded);
                var value = Encoding.UTF8.GetString(bytes);

                Clients = JsonConvert.DeserializeObject<string[]>(value);
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