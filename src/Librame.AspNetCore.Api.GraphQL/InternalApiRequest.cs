#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Newtonsoft.Json.Linq;

namespace Librame.AspNetCore.Api
{
    /// <summary>
    /// 内部 API 请求。
    /// </summary>
    internal class InternalApiRequest : IApiRequest
    {
        /// <summary>
        /// 请求查询。
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// 查询参数集合。
        /// </summary>
        public JObject Variables { get; set; }
    }
}
