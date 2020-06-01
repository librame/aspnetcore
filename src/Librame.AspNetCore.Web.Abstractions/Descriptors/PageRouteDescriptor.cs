#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Librame.AspNetCore.Web.Descriptors
{
    using Extensions;

    /// <summary>
    /// 页面路由描述符。
    /// </summary>
    public class PageRouteDescriptor : AbstractRouteDescriptor
    {
        /// <summary>
        /// 页面键名。
        /// </summary>
        public const string PageKey = "page";


        /// <summary>
        /// 构造一个 <see cref="AbstractRouteDescriptor"/>。
        /// </summary>
        /// <param name="page">给定的页面。</param>
        /// <param name="area">给定的区域（可选）。</param>
        /// <param name="id">给定的参数（可选）。</param>
        /// <param name="lockedArea">锁定区域（默认锁定；表示区域不会在环境正常化中修改）。</param>
        public PageRouteDescriptor(string page,
            string area = null, string id = null, bool lockedArea = true)
            : base(area, id, lockedArea)
        {
            Page = page.NotEmpty(nameof(page));
        }

        private PageRouteDescriptor(bool lockedArea)
            : base(lockedArea: lockedArea)
        {
        }


        /// <summary>
        /// 页面。
        /// </summary>
        public string Page { get; private set; }


        /// <summary>
        /// 改变页面名称，如果有路径部分则保持不变（如将当前 Page 的值 '/Path/PageName' 改变为 '/Path/NewPageName'）。
        /// </summary>
        /// <param name="newPageName">给定的新页面名称（不支持路径）。</param>
        /// <returns>返回 <see cref="PageRouteDescriptor"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public PageRouteDescriptor ChangePageName(string newPageName)
            => ChangePage(NormalizeNewPageByName(newPageName));

        /// <summary>
        /// 改变页面。
        /// </summary>
        /// <param name="newPage">给定的新页面。</param>
        /// <returns>返回 <see cref="PageRouteDescriptor"/>。</returns>
        public PageRouteDescriptor ChangePage(string newPage)
        {
            Page = newPage.NotEmpty(nameof(newPage));
            return this;
        }


        /// <summary>
        /// 带有页面名称，如果有路径部分则保持不变（如将当前 Page 的值 '/Path/PageName' 改变为 '/Path/NewPageName'）。
        /// </summary>
        /// <param name="newPageName">给定的新页面名称（不支持路径）。</param>
        /// <returns>返回 <see cref="PageRouteDescriptor"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public PageRouteDescriptor WithPageName(string newPageName)
            => WithPage(NormalizeNewPageByName(newPageName));

        /// <summary>
        /// 带有页面。
        /// </summary>
        /// <param name="newPage">给定的新页面。</param>
        /// <returns>返回 <see cref="PageRouteDescriptor"/>。</returns>
        public PageRouteDescriptor WithPage(string newPage)
        {
            newPage.NotEmpty(nameof(newPage));
            return new PageRouteDescriptor(newPage, Area, Id);
        }

        private string NormalizeNewPageByName(string newPageName)
        {
            newPageName.NotEmpty(nameof(newPageName));

            (var _, var extension) = Page.GetFileBaseNameAndExtension(out var basePath);
            return $"{basePath}{newPageName}{extension}";
        }


        /// <summary>
        /// 获取视图名称。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string GetViewName()
        {
            (var baseName, _) = Page.GetFileBaseNameAndExtension(out _);
            return baseName;
        }


        /// <summary>
        /// 生成模拟链接。
        /// </summary>
        /// <returns>返回链接字符串。</returns>
        public override string GenerateSimulativeLink()
        {
            var sb = new StringBuilder();

            if (Area.IsNotEmpty())
                sb.Append($"/{Area}");

            if (Page.IsNotEmpty())
                sb.Append(Page.EnsureLeading('/'));

            if (Id.IsNotEmpty())
                sb.Append(Id.EnsureLeading('?'));

            return sb.ToString();
        }


        /// <summary>
        /// 从环境正常化（将当前属性值填充为指定环境中存在的路由值）。
        /// </summary>
        /// <param name="ambientValues">给定的环境路由值集合。</param>
        /// <param name="result">输出结果。</param>
        /// <param name="updateOnlyNullPropertyValues">仅更新空属性值（可选；默认启用）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public override void NormalizeFromAmbient(IDictionary<string, string> ambientValues,
            out string result, bool updateOnlyNullPropertyValues = true)
        {
            base.NormalizeFromAmbient(ambientValues, out result, updateOnlyNullPropertyValues);

            if ((updateOnlyNullPropertyValues && Page.IsNull() || !updateOnlyNullPropertyValues)
                && ambientValues.TryGetValue(PageKey, out result))
            {
                Page = result;
            }
        }

        /// <summary>
        /// 从环境正常化（将当前属性值填充为指定环境中存在的路由值）。
        /// </summary>
        /// <param name="ambientValues">给定的环境路由值集合。</param>
        /// <param name="result">输出结果。</param>
        /// <param name="updateOnlyNullPropertyValues">仅更新空属性值（可选；默认启用）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public override void NormalizeFromAmbient(RouteValueDictionary ambientValues,
            out object result, bool updateOnlyNullPropertyValues = true)
        {
            base.NormalizeFromAmbient(ambientValues, out result, updateOnlyNullPropertyValues);

            if ((updateOnlyNullPropertyValues && Page.IsNull() || !updateOnlyNullPropertyValues)
                && ambientValues.TryGetValue(PageKey, out result))
            {
                Page = result?.ToString();
            }
        }


        /// <summary>
        /// 正常化到环境（将当前属性值填充到指定环境中的路由值）。
        /// </summary>
        /// <param name="ambientValues">给定的环境路由值集合。</param>
        /// <param name="updateOnlyNotNullPropertyValues">仅更新不为空的属性值（可选；默认启用）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public override void NormalizeToAmbient(IDictionary<string, string> ambientValues,
            bool updateOnlyNotNullPropertyValues = true)
        {
            base.NormalizeToAmbient(ambientValues, updateOnlyNotNullPropertyValues);

            if (updateOnlyNotNullPropertyValues && Page.IsNotNull() || !updateOnlyNotNullPropertyValues)
                ambientValues[PageKey] = Page;
        }

        /// <summary>
        /// 从环境正常化（将当前空属性值填充为指定环境中存在的路由值）。
        /// </summary>
        /// <param name="ambientValues">给定的环境路由值集合。</param>
        /// <param name="updateOnlyNotNullPropertyValues">仅更新空属性值（可选；默认启用）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public override void NormalizeToAmbient(RouteValueDictionary ambientValues,
            bool updateOnlyNotNullPropertyValues = true)
        {
            base.NormalizeToAmbient(ambientValues, updateOnlyNotNullPropertyValues);

            if (updateOnlyNotNullPropertyValues && Page.IsNotNull() || !updateOnlyNotNullPropertyValues)
                ambientValues[PageKey] = Page;
        }


        /// <summary>
        /// 判定路由值集合能否解析为页面路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回布尔值。</returns>
        public static bool CanParse(IDictionary<string, string> routeValues)
            => routeValues.NotNull(nameof(routeValues)).ContainsKey(PageKey);

        /// <summary>
        /// 判定路由值集合能否解析为页面路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回布尔值。</returns>
        public static bool CanParse(RouteValueDictionary routeValues)
            => routeValues.NotNull(nameof(routeValues)).ContainsKey(PageKey);


        /// <summary>
        /// 从路由值集合中解析页面路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回 <see cref="PageRouteDescriptor"/>。</returns>
        public static PageRouteDescriptor Parse(IDictionary<string, string> routeValues)
        {
            // 初始解锁区域，以免丢失区域参数
            var descriptor = new PageRouteDescriptor(lockedArea: false);
            descriptor.NormalizeFromAmbient(routeValues, updateOnlyNullPropertyValues: false);

            descriptor.LockedArea = true;
            return descriptor;
        }

        /// <summary>
        /// 从路由值集合中解析页面路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回 <see cref="PageRouteDescriptor"/>。</returns>
        public static PageRouteDescriptor Parse(RouteValueDictionary routeValues)
        {
            // 初始解锁区域，以免丢失区域参数
            var descriptor = new PageRouteDescriptor(lockedArea: false);
            descriptor.NormalizeFromAmbient(routeValues, updateOnlyNullPropertyValues: false);

            descriptor.LockedArea = true;
            return descriptor;
        }


        /// <summary>
        /// 尝试从路由值集合中解析页面路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <param name="result">输出 <see cref="PageRouteDescriptor"/> 或 NULL。</param>
        /// <returns>返回布尔值。</returns>
        public static bool TryParse(IDictionary<string, string> routeValues,
            out PageRouteDescriptor result)
        {
            if (CanParse(routeValues))
            {
                result = Parse(routeValues);
                return true;
            }

            result = null;
            return false;
        }

        /// <summary>
        /// 尝试从路由值集合中解析页面路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <param name="result">输出 <see cref="PageRouteDescriptor"/> 或 NULL。</param>
        /// <returns>返回布尔值。</returns>
        public static bool TryParse(RouteValueDictionary routeValues,
            out PageRouteDescriptor result)
        {
            if (CanParse(routeValues))
            {
                result = Parse(routeValues);
                return true;
            }

            result = null;
            return false;
        }

    }
}
