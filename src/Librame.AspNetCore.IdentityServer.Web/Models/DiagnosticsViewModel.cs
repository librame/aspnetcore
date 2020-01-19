// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Librame.AspNetCore.IdentityServer.Web.Models
{
    using Extensions;

    /// <summary>
    /// �����ͼģ�͡�
    /// </summary>
    public class DiagnosticsViewModel
    {
        /// <summary>
        /// ����һ�� <see cref="DiagnosticsViewModel"/>��
        /// </summary>
        /// <param name="result">������ <see cref="Microsoft.AspNetCore.Authentication.AuthenticateResult"/>��</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "result")]
        public DiagnosticsViewModel(AuthenticateResult result)
        {
            AuthenticateResult = result.NotNull(nameof(result));

            if (result.Properties.Items.ContainsKey("client_list"))
            {
                var encoded = result.Properties.Items["client_list"];
                var bytes = Base64Url.Decode(encoded);
                var value = Encoding.UTF8.GetString(bytes);

                Clients = JsonConvert.DeserializeObject<string[]>(value);
            }
        }


        /// <summary>
        /// ��֤�����
        /// </summary>
        public AuthenticateResult AuthenticateResult { get; }

        /// <summary>
        /// �ͻ��˼��ϡ�
        /// </summary>
        public IEnumerable<string> Clients { get; }
            = new List<string>();
    }
}