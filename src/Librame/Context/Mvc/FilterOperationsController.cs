// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Utility;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Librame.Context.Mvc
{
    /// <summary>
    /// <see cref="Data.FilterOperation"/> 过滤运算方式枚举的控制器。
    /// </summary>
    /// <author>Librame Pang</author>
    public class FilterOperationsController : Controller
    {
        /// <summary>
        /// 首页视图。
        /// </summary>
        /// <returns>输出 JSON 格式的字符串。</returns>
        public virtual IActionResult Index()
        {
            return Json(EnumUtils.FilterOperations);
        }

        /// <summary>
        /// 详情视图。
        /// </summary>
        /// <returns>输出 JSON 格式的字符串。</returns>
        public virtual IActionResult Detail(int id)
        {
            return Json(EnumUtils.FilterOperations.Where(p => p.Value == id).FirstOrDefault());
        }

    }
}