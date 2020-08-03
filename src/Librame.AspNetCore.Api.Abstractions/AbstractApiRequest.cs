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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Api
{
    using Extensions;
    using Extensions.Core.Services;

    /// <summary>
    /// 抽象 API 请求。
    /// </summary>
    public abstract class AbstractApiRequest : AbstractService, IApiRequest
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractApiRequest"/>。
        /// </summary>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected AbstractApiRequest(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }


        /// <summary>
        /// 操作名称。
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// 查询内容。
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// 查询参数集合。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public JObject Variables { get; set; }


        /// <summary>
        /// 执行请求的 JSON 结果。
        /// </summary>
        /// <param name="query">给定的查询内容（可选；默认为 <see cref="Query"/>）。</param>
        /// <param name="context">给定的 <see cref="HttpContext"/>（可选）。</param>
        /// <returns>返回字符串。</returns>
        public abstract string ExecuteJson(string query = null,
            HttpContext context = null);

        /// <summary>
        /// 异步执行请求的 JSON 结果。
        /// </summary>
        /// <param name="query">给定的查询内容（可选；默认为 <see cref="Query"/>）。</param>
        /// <param name="context">给定的 <see cref="HttpContext"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="ApiRequestResult"/> 的异步操作。</returns>
        public abstract Task<string> ExecuteJsonAsync(string query = null,
            HttpContext context = null);


        /// <summary>
        /// 异步执行请求。
        /// </summary>
        /// <param name="query">给定的查询内容（可选；默认为 <see cref="Query"/>）。</param>
        /// <param name="context">给定的 <see cref="HttpContext"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="ApiRequestResult"/> 的异步操作。</returns>
        public abstract Task<ApiRequestResult> ExecuteAsync(string query = null,
            HttpContext context = null);


        /// <summary>
        /// 填充请求。
        /// </summary>
        /// <param name="syncStream">给定的同步 <see cref="Stream"/>。</param>
        /// <returns>返回 <see cref="IApiRequest"/>。</returns>
        public virtual IApiRequest Populate(Stream syncStream)
        {
            using (var sr = new StreamReader(syncStream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                js.Populate(jtr, this);
            }

            return this;
        }

        /// <summary>
        /// 填充请求。
        /// </summary>
        /// <param name="value">给定的 JSON 值。</param>
        /// <param name="settings">给定的 <see cref="JsonSerializerSettings"/>（可选）。</param>
        /// <returns>返回 <see cref="IApiRequest"/>。</returns>
        public virtual IApiRequest Populate(string value,
            JsonSerializerSettings settings = null)
        {
            if (value.IsNotEmpty())
                JsonConvert.PopulateObject(value, this, settings);

            return this;
        }

        /// <summary>
        /// 异步填充请求。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <param name="settings">给定的 <see cref="JsonSerializerSettings"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IApiRequest"/> 的异步操作。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual async Task<IApiRequest> PopulateAsync(HttpContext context,
            JsonSerializerSettings settings = null)
        {
            context.NotNull(nameof(context));

            string value;

            context.Request.EnableBuffering();
            using (var sr = new StreamReader(context.Request.Body))
            {
                value = await sr.ReadToEndAsync().ConfigureAwait();

                // Do some processing with body…
                // Reset the request body stream position so the next middleware can read it
                context.Request.Body.Position = 0;
            }

            return Populate(value);
        }

    }
}
