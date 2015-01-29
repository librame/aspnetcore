// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNet.Mvc
{
    /// <summary>
    /// Librame 控制器静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class LibrameControllerExtensions
    {
        /// <summary>
        /// 未通过验证(412)的 HTTP 响应。
        /// </summary>
        /// <param name="controller">给定的控制器。</param>
        /// <returns>返回一个未通过验证(412)的 HTTP 响应实例。</returns>
        public static NotPassValidationResult NotPassValidation(this Controller controller)
        {
            return new NotPassValidationResult();
        }

        /// <summary>
        /// 输出 JSON 格式的 UTF8 编码内容。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller">给定的控制器。</param>
        /// <param name="pageable">给定的数据分页列表。</param>
        /// <returns>返回内容输出响应对象。</returns>
        public static ContentResult JsonUTF8Content<T>(this Controller controller, IPageable<T> pageable)
        {
            string json = pageable.Rows.JsonSerializer();

            // 转换为 Kendo UI Grid 可识别的数据分页格式
            return JsonUTF8Content(controller, String.Format("{{\"rows\":{1}, \"total\":{0} }}", pageable.Total, json));
        }
        /// <summary>
        /// 输出 JSON 格式的 UTF8 编码内容。
        /// </summary>
        /// <param name="controller">给定的控制器。</param>
        /// <param name="json">给定的 JSON。</param>
        /// <returns>返回内容输出响应对象。</returns>
        public static ContentResult JsonUTF8Content(this Controller controller, string json)
        {
            var encoding = controller.Context.ApplicationServices.GetService<Encoding>();

            return controller.Content(json, "application/json;charset=" + encoding.WebName, encoding);
        }

        /// <summary>
        /// 输出 UTF8 编码的内容。
        /// </summary>
        /// <param name="controller">给定的控制器。</param>
        /// <param name="content">给定的内容。</param>
        /// <returns>返回内容输出响应对象。</returns>
        public static ContentResult UTF8Content(this Controller controller, string content)
        {
            var encoding = controller.Context.ApplicationServices.GetService<Encoding>();

            return controller.Content(content, "text/plain;charset=" + encoding.WebName, encoding);
        }

    }
}