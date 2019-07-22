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
    /// API 请求接口。
    /// </summary>
    public interface IApiRequest
    {
        /// <summary>
        /// 操作名称。
        /// </summary>
        string OperationName { get; set; }

        /// <summary>
        /// 请求查询。
        /// </summary>
        string Query { get; set; }

        /// <summary>
        /// 查询参数集合。
        /// </summary>
        JObject Variables { get; set; }
    }
}
