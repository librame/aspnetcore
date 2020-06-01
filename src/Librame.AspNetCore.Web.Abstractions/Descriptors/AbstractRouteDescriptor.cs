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
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Descriptors
{
    using Extensions;

    /// <summary>
    /// 抽象路由描述符。
    /// </summary>
    public abstract class AbstractRouteDescriptor : IEquatable<AbstractRouteDescriptor>
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractRouteDescriptor"/>。
        /// </summary>
        /// <param name="descriptor">给定的 <see cref="AbstractRouteDescriptor"/>。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected AbstractRouteDescriptor(AbstractRouteDescriptor descriptor)
            : this(descriptor.NotNull(nameof(descriptor)).Area, descriptor.Id, descriptor.LockedArea)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="AbstractRouteDescriptor"/>。
        /// </summary>
        /// <param name="area">给定的区域（可选）。</param>
        /// <param name="id">给定的参数（可选）。</param>
        /// <param name="lockedArea">锁定区域（默认锁定；表示区域不会在环境正常化中修改）。</param>
        protected AbstractRouteDescriptor(string area = null, string id = null, bool lockedArea = true)
        {
            Area = area;
            Id = id;
            LockedArea = lockedArea;
        }


        /// <summary>
        /// 区域。
        /// </summary>
        public string Area { get; internal set; }

        /// <summary>
        /// 参数。
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// 锁定区域（默认锁定；表示区域不会在环境正常化中修改）。
        /// </summary>
        public bool LockedArea { get; set; }


        /// <summary>
        /// 是指定视图名称。
        /// </summary>
        /// <param name="compare">给定的 <see cref="AbstractRouteDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool IsViewName(AbstractRouteDescriptor compare)
            => IsViewName(compare?.GetViewName());

        /// <summary>
        /// 是指定视图名称。
        /// </summary>
        /// <param name="viewName">给定的视图名称。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool IsViewName(string viewName)
            => GetViewName().Equals(viewName, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 获取视图名称。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public abstract string GetViewName();


        /// <summary>
        /// 生成模拟链接。
        /// </summary>
        /// <returns>返回链接字符串。</returns>
        public abstract string GenerateSimulativeLink();


        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="AbstractRouteDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(AbstractRouteDescriptor other)
            => ToString().Equals(other?.ToString(), StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="obj">给定的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => obj is AbstractRouteDescriptor other && Equals(other);


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回整数。</returns>
        public override int GetHashCode()
            => ToString().GetHashCode(StringComparison.InvariantCulture);


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回模拟链接字符串。</returns>
        public override string ToString()
            => GenerateSimulativeLink();


        /// <summary>
        /// 从环境正常化（将当前属性值填充为指定环境中存在的路由值）。
        /// </summary>
        /// <param name="ambientValues">给定的环境路由值集合。</param>
        /// <param name="updateOnlyNullPropertyValues">仅更新空属性值（可选；默认启用）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual void NormalizeFromAmbient(IDictionary<string, string> ambientValues,
            bool updateOnlyNullPropertyValues = true)
            => NormalizeFromAmbient(ambientValues, out _, updateOnlyNullPropertyValues);

        /// <summary>
        /// 从环境正常化（将当前属性值填充为指定环境中存在的路由值）。
        /// </summary>
        /// <param name="ambientValues">给定的环境路由值集合。</param>
        /// <param name="result">输出结果。</param>
        /// <param name="updateOnlyNullPropertyValues">仅更新空属性值（可选；默认启用）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual void NormalizeFromAmbient(IDictionary<string, string> ambientValues,
            out string result, bool updateOnlyNullPropertyValues = true)
        {
            ambientValues.NotNull(nameof(ambientValues));
            
            result = null;

            if ((updateOnlyNullPropertyValues && Area.IsNull() || !updateOnlyNullPropertyValues)
                && !LockedArea && ambientValues.TryGetValue("area", out result))
            {
                Area = result;
            }

            if ((updateOnlyNullPropertyValues && Id.IsNull() || !updateOnlyNullPropertyValues)
                && ambientValues.TryGetValue("id", out result))
            {
                Id = result;
            }
        }

        /// <summary>
        /// 从环境正常化（将当前属性值填充为指定环境中存在的路由值）。
        /// </summary>
        /// <param name="ambientValues">给定的环境路由值集合。</param>
        /// <param name="updateOnlyNullPropertyValues">仅更新空属性值（可选；默认启用）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual void NormalizeFromAmbient(RouteValueDictionary ambientValues,
            bool updateOnlyNullPropertyValues = true)
            => NormalizeFromAmbient(ambientValues, out _, updateOnlyNullPropertyValues);

        /// <summary>
        /// 从环境正常化（将当前属性值填充为指定环境中存在的路由值）。
        /// </summary>
        /// <param name="ambientValues">给定的环境路由值集合。</param>
        /// <param name="result">输出结果。</param>
        /// <param name="updateOnlyNullPropertyValues">仅更新空属性值（可选；默认启用）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual void NormalizeFromAmbient(RouteValueDictionary ambientValues,
            out object result, bool updateOnlyNullPropertyValues = true)
        {
            ambientValues.NotNull(nameof(ambientValues));

            result = null;

            if ((updateOnlyNullPropertyValues && Area.IsNull() || !updateOnlyNullPropertyValues)
                && !LockedArea && ambientValues.TryGetValue("area", out result))
            {
                Area = result?.ToString();
            }

            if ((updateOnlyNullPropertyValues && Id.IsNull() || !updateOnlyNullPropertyValues)
                && ambientValues.TryGetValue("id", out result))
            {
                Id = result?.ToString();
            }
        }


        /// <summary>
        /// 正常化到环境（将当前属性值填充到指定环境中的路由值）。
        /// </summary>
        /// <param name="ambientValues">给定的环境路由值集合。</param>
        /// <param name="updateOnlyNotNullPropertyValues">仅更新不为空的属性值（可选；默认启用）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual void NormalizeToAmbient(IDictionary<string, string> ambientValues,
            bool updateOnlyNotNullPropertyValues = true)
        {
            ambientValues.NotNull(nameof(ambientValues));

            if (updateOnlyNotNullPropertyValues && Area.IsNotNull() || !updateOnlyNotNullPropertyValues)
                ambientValues["area"] = Area;

            if (updateOnlyNotNullPropertyValues && Id.IsNotNull() || !updateOnlyNotNullPropertyValues)
                ambientValues["id"] = Id;
        }

        /// <summary>
        /// 从环境正常化（将当前空属性值填充为指定环境中存在的路由值）。
        /// </summary>
        /// <param name="ambientValues">给定的环境路由值集合。</param>
        /// <param name="updateOnlyNotNullPropertyValues">仅更新空属性值（可选；默认启用）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual void NormalizeToAmbient(RouteValueDictionary ambientValues,
            bool updateOnlyNotNullPropertyValues = true)
        {
            ambientValues.NotNull(nameof(ambientValues));

            if (updateOnlyNotNullPropertyValues && Area.IsNotNull() || !updateOnlyNotNullPropertyValues)
                ambientValues["area"] = Area;

            if (updateOnlyNotNullPropertyValues && Id.IsNotNull() || !updateOnlyNotNullPropertyValues)
                ambientValues["id"] = Id;
        }


        /// <summary>
        /// 从路由值集合中解析路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回 <see cref="ActionRouteDescriptor"/>、<see cref="PageRouteDescriptor"/> 或 NULL。</returns>
        public static AbstractRouteDescriptor GeneralParse(IDictionary<string, string> routeValues)
        {
            if (ActionRouteDescriptor.CanParse(routeValues))
                return ActionRouteDescriptor.Parse(routeValues);

            if (PageRouteDescriptor.CanParse(routeValues))
                return PageRouteDescriptor.Parse(routeValues);

            return null;
        }

        /// <summary>
        /// 从路由值集合中解析路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回 <see cref="ActionRouteDescriptor"/>、<see cref="PageRouteDescriptor"/> 或 NULL。</returns>
        public static AbstractRouteDescriptor GeneralParse(RouteValueDictionary routeValues)
        {
            if (ActionRouteDescriptor.CanParse(routeValues))
                return ActionRouteDescriptor.Parse(routeValues);

            if (PageRouteDescriptor.CanParse(routeValues))
                return PageRouteDescriptor.Parse(routeValues);

            return null;
        }

    }
}
