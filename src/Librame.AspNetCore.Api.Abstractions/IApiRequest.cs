#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Api
{
    using Extensions.Core.Services;

    /// <summary>
    /// API 请求接口。
    /// </summary>
    public interface IApiRequest : IService
    {
        /// <summary>
        /// 操作名称。
        /// </summary>
        string OperationName { get; set; }

        /// <summary>
        /// 查询内容。
        /// </summary>
        string Query { get; set; }


        /// <summary>
        /// 查询参数集合。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        JObject Variables { get; set; }


        /// <summary>
        /// 执行请求的 JSON 结果。
        /// </summary>
        /// <param name="query">给定的查询内容（可选；默认为 <see cref="Query"/>）。</param>
        /// <param name="context">给定的 <see cref="HttpContext"/>（可选）。</param>
        /// <returns>返回字符串。</returns>
        string ExecuteJson(string query = null, HttpContext context = null);

        /// <summary>
        /// 异步执行请求的 JSON 结果。
        /// </summary>
        /// <param name="query">给定的查询内容（可选；默认为 <see cref="Query"/>）。</param>
        /// <param name="context">给定的 <see cref="HttpContext"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="ApiRequestResult"/> 的异步操作。</returns>
        Task<string> ExecuteJsonAsync(string query = null, HttpContext context = null);


        /// <summary>
        /// 异步执行请求。
        /// </summary>
        /// <param name="query">给定的查询内容（可选；默认为 <see cref="Query"/>）。</param>
        /// <param name="context">给定的 <see cref="HttpContext"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="ApiRequestResult"/> 的异步操作。</returns>
        Task<ApiRequestResult> ExecuteAsync(string query = null,
            HttpContext context = null);


        /// <summary>
        /// 装填请求。
        /// </summary>
        /// <param name="syncStream">给定的同步 <see cref="Stream"/>。</param>
        /// <returns>返回 <see cref="IApiRequest"/>。</returns>
        IApiRequest Populate(Stream syncStream);

        /// <summary>
        /// 填充请求。
        /// </summary>
        /// <param name="value">给定的 JSON 值。</param>
        /// <param name="settings">给定的 <see cref="JsonSerializerSettings"/>（可选）。</param>
        /// <returns>返回 <see cref="IApiRequest"/>。</returns>
        IApiRequest Populate(string value, JsonSerializerSettings settings = null);

        /// <summary>
        /// 异步填充请求。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <param name="settings">给定的 <see cref="JsonSerializerSettings"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IApiRequest"/> 的异步操作。</returns>
        Task<IApiRequest> PopulateAsync(HttpContext context, JsonSerializerSettings settings = null);
    }
}
