#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibrameStandard.Utilities
{
    /// <summary>
    /// <see cref="HttpResponse"/> 实用工具。
    /// </summary>
    public static class HttpResponseUtility
    {

        /// <summary>
        /// 将一个错误请求的消息内容写入响应主体。
        /// </summary>
        /// <param name="response">给定的 HTTP 响应。</param>
        /// <param name="message">给定的消息内容。</param>
        /// <returns>返回一个异步操作。</returns>
        public static async Task WriteErrorRequestAsync(this HttpResponse response, string message)
        {
            await response.WriteMessageAsync(message, HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// 将消息内容写入响应主体。
        /// </summary>
        /// <param name="response">给定的 HTTP 响应。</param>
        /// <param name="message">给定的消息内容。</param>
        /// <param name="statusCode">给定的 HTTP 响应状态码（可选；默认为 <see cref="HttpStatusCode.OK"/>）。</param>
        /// <returns>返回一个异步操作。</returns>
        public static async Task WriteMessageAsync(this HttpResponse response, string message,
            HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            await response.WriteJsonAsync(message, statusCode, null, default(CancellationToken));
        }

        /// <summary>
        /// 将给定的值通过给定的字符编码以 JSON 形式写入响应主体。
        /// </summary>
        /// <remarks>支持直接转换值类型和虚转换字符串类型。</remarks>
        /// <param name="response">给定的 HTTP 响应。</param>
        /// <param name="value">给定的值。</param>
        /// <param name="statusCode">给定的 HTTP 响应状态码（可选；默认为 <see cref="HttpStatusCode.OK"/>）。</param>
        /// <param name="encoding">给定的字符编码（可选；默认为 <see cref="Encoding.UTF8"/>）。</param>
        /// <param name="cancellationToken">给定请求操作时应该被取消的通知（可选）。</param>
        /// <returns>返回一个异步操作。</returns>
        public static async Task WriteJsonAsync<TValue>(this HttpResponse response, TValue value,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            Encoding encoding = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            value.NotNull(nameof(value));
            encoding = encoding.AsOrDefault(Encoding.UTF8);

            response.StatusCode = (int)statusCode;

            var typeInfo = value.GetType().GetTypeInfo();
            if (typeInfo.IsValueType || typeInfo.IsString())
            {
                // 值类型和字符串类型直接写入
                response.ContentType = "text/plain;charset=" + encoding.AsName();
                await response.WriteAsync(value.ToString(), cancellationToken);
            }
            else
            {
                // JSON 序列化
                string json = value.AsJson(false, Newtonsoft.Json.Formatting.Indented);

                response.ContentType = JsonConvertUtility.CONTENT_TYPE + ";charset=" + encoding.AsName();
                await response.WriteAsync(json, cancellationToken);
            }
        }

    }
}
