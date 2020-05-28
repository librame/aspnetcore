#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;

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
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        JObject Variables { get; set; }
    }
}
