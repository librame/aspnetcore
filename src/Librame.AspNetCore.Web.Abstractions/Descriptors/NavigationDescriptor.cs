#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Descriptors
{
    using Extensions;
    using Extensions.Core.Combiners;

    /// <summary>
    /// 导航描述符。
    /// </summary>
    public class NavigationDescriptor
    {
        /// <summary>
        /// 构造一个 <see cref="NavigationDescriptor"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer"/>。</param>
        /// <param name="localizerName">给定的本地化名称。</param>
        /// <param name="initialLink">给定的初始化链接。</param>
        public NavigationDescriptor(IHtmlLocalizer localizer,
            string localizerName, string initialLink)
        {
            Localizer = localizer.NotNull(nameof(localizer));
            LocalizerName = Id = localizerName.NotEmpty(nameof(localizerName));
            InitialLink = initialLink.NotEmpty(nameof(initialLink));
        }

        /// <summary>
        /// 构造一个 <see cref="NavigationDescriptor"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer"/>。</param>
        /// <param name="route">给定的 <see cref="AbstractRouteDescriptor"/>。</param>
        /// <param name="localizerName">给定的本地化名称（可选；默认使用路由视图名称）。</param>
        public NavigationDescriptor(IHtmlLocalizer localizer,
            AbstractRouteDescriptor route, string localizerName = null)
        {
            Localizer = localizer.NotNull(nameof(localizer));
            Route = route.NotNull(nameof(route));
            LocalizerName = Id = localizerName ?? Route.GetViewName();
        }


        /// <summary>
        /// 本地化器。
        /// </summary>
        /// <value>返回 <see cref="IHtmlLocalizer"/>。</value>
        public IHtmlLocalizer Localizer { get; }

        /// <summary>
        /// 本地化器名称。
        /// </summary>
        public string LocalizerName { get; }

        /// <summary>
        /// 初始化链接。
        /// </summary>
        /// <value>返回字符串或 NULL。</value>
        public string InitialLink { get; }

        /// <summary>
        /// 路由描述符。
        /// </summary>
        /// <value>返回 <see cref="AbstractRouteDescriptor"/> 或 NULL。</value>
        public AbstractRouteDescriptor Route { get; private set; }


        /// <summary>
        /// 标识（默认为本地化器名称）。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 图标。
        /// </summary>
        public string Icon { get; set; }
            = "glyphicon glyphicon-link";

        /// <summary>
        /// 目标。
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 可见性（默认可见）。
        /// </summary>
        public bool Visibility { get; set; }
            = true;


        /// <summary>
        /// 子级导航。
        /// </summary>
        public List<NavigationDescriptor> Children { get; }
            = new List<NavigationDescriptor>();


        /// <summary>
        /// 改变标识。
        /// </summary>
        /// <param name="id">给定的标识。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor ChangeId(string id)
        {
            Id = id.NotEmpty(nameof(id));
            return this;
        }

        /// <summary>
        /// 改变图标。
        /// </summary>
        /// <param name="icon">给定的图标。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor ChangeIcon(string icon)
        {
            Icon = icon.NotEmpty(nameof(icon));
            return this;
        }

        /// <summary>
        /// 改变为空白窗口加载链接目标。
        /// </summary>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor ChangeTargetBlank()
        {
            Target = "_blank";
            return this;
        }

        /// <summary>
        /// 改变路由描述符。
        /// </summary>
        /// <typeparam name="TDescriptor">指定的路由描述符类型。</typeparam>
        /// <param name="newRouteFactory">给定的新路由描述符工厂方法。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor ChangeRoute<TDescriptor>(Func<TDescriptor, TDescriptor> newRouteFactory)
            where TDescriptor : AbstractRouteDescriptor
            => ChangeRoute(newRouteFactory?.Invoke((TDescriptor)Route));

        /// <summary>
        /// 改变路由描述符。
        /// </summary>
        /// <param name="newRoute">给定的新 <see cref="AbstractRouteDescriptor"/>。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor ChangeRoute(AbstractRouteDescriptor newRoute)
        {
            Route = newRoute.NotNull(nameof(newRoute));
            return this;
        }


        /// <summary>
        /// 带有初始化链接。
        /// </summary>
        /// <param name="newInitialLink">给定的新初始化链接。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor WithInitialLink(string newInitialLink)
        {
            var nav = new NavigationDescriptor(Localizer, LocalizerName, newInitialLink);

            nav.Id = Id;
            nav.Icon = Icon;
            nav.Target = Target;
            nav.Visibility = Visibility;

            return nav;
        }

        /// <summary>
        /// 带有路由描述符。
        /// </summary>
        /// <typeparam name="TRoute">指定的路由描述符类型。</typeparam>
        /// <param name="newRouteFactory">给定的新路由描述符工厂方法。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor WithRoute<TRoute>(Func<TRoute, TRoute> newRouteFactory)
            where TRoute : AbstractRouteDescriptor
            => WithRoute(newRouteFactory?.Invoke((TRoute)Route));

        /// <summary>
        /// 带有路由描述符。
        /// </summary>
        /// <param name="newRoute">给定的新 <see cref="AbstractRouteDescriptor"/>。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor WithRoute(AbstractRouteDescriptor newRoute)
        {
            var nav = new NavigationDescriptor(Localizer, newRoute, LocalizerName);

            nav.Id = Id;
            nav.Icon = Icon;
            nav.Target = Target;
            nav.Visibility = Visibility;

            return nav;
        }


        /// <summary>
        /// 获取本地化 HTML 文本。
        /// </summary>
        /// <returns>返回 <see cref="LocalizedHtmlString"/>。</returns>
        public LocalizedHtmlString GetLocalizedHtmlText()
            => Localizer[LocalizerName];

        /// <summary>
        /// 获取本地化 HTML 文本。
        /// </summary>
        /// <param name="arguments">给定的格式化对象参数集合。</param>
        /// <returns>返回 <see cref="LocalizedHtmlString"/>。</returns>
        public LocalizedHtmlString GetLocalizedHtmlText(params object[] arguments)
            => Localizer[LocalizerName, arguments];

        /// <summary>
        /// 获取本地化文本。
        /// </summary>
        /// <returns>返回 <see cref="LocalizedString"/>。</returns>
        public LocalizedString GetLocalizedText()
            => Localizer.GetString(LocalizerName);

        /// <summary>
        /// 获取本地化文本。
        /// </summary>
        /// <param name="arguments">给定的格式化对象参数集合。</param>
        /// <returns>返回 <see cref="LocalizedString"/>。</returns>
        public LocalizedString GetLocalizedText(params object[] arguments)
            => Localizer.GetString(LocalizerName, arguments);


        /// <summary>
        /// 是否激活导航（支持对比路由视图名称或本地化名称）。
        /// </summary>
        /// <param name="compare">给定的 <see cref="AbstractRouteDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool IsActivated(AbstractRouteDescriptor compare)
        {
            if (!(Route?.IsViewName(compare)).Value)
                return LocalizerName.Equals(compare?.GetViewName(), StringComparison.OrdinalIgnoreCase);

            return true;
        }

        /// <summary>
        /// 是否激活导航（支持对比路由视图名称或本地化名称）。
        /// </summary>
        /// <param name="viewNameOrLocalizerName">给定要激活的路由视图名称或本地化名称。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool IsActivated(string viewNameOrLocalizerName)
        {
            if (!(Route?.IsViewName(viewNameOrLocalizerName)).Value)
                return LocalizerName.Equals(viewNameOrLocalizerName, StringComparison.OrdinalIgnoreCase);

            return true;
        }


        /// <summary>
        /// 生成链接。
        /// </summary>
        /// <param name="urlHelper">给定的 <see cref="IUrlHelper"/>。</param>
        /// <param name="appendRequestQuery">附加当前请求查询字符串（可选；默认附加）。</param>
        /// <param name="configureOtherRouteValues">配置其他路由值集合（可选）。</param>
        /// <returns>返回链接字符串。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual string GenerateLink(IUrlHelper urlHelper, bool appendRequestQuery = true,
            Action<RouteValueDictionary> configureOtherRouteValues = null)
        {
            urlHelper.NotNull(nameof(urlHelper));

            if (Route.IsNotNull())
            {
                Route.NormalizeFromAmbient(urlHelper.ActionContext.RouteData.Values);

                var routeValues = new RouteValueDictionary();
                routeValues.Add("area", Route.Area);

                if (appendRequestQuery)
                {
                    foreach (var pair in urlHelper.ActionContext.HttpContext?.Request.Query)
                    {
                        routeValues.Add(pair.Key, pair.Value);
                    }
                }

                configureOtherRouteValues?.Invoke(routeValues);

                if (Route is ActionRouteDescriptor actionRoute)
                    return urlHelper.Action(actionRoute.Action, actionRoute.Controller, routeValues);

                if (Route is PageRouteDescriptor pageRoute)
                    return urlHelper.Page(pageRoute.Page, routeValues);
            }

            if (InitialLink.IsNotEmpty() && appendRequestQuery)
            {
                var queryString = UriCombinerCore.ToQuery(urlHelper.ActionContext.HttpContext?.Request.Query,
                    !InitialLink.Contains(UriCombiner.QueryStringDelimiter, StringComparison.OrdinalIgnoreCase));

                return InitialLink + queryString;
            }

            return InitialLink;
        }

        /// <summary>
        /// 生成模拟链接（仅在无法使用 <see cref="IUrlHelper"/> 参数，且项目环境使用默认路由设定时使用）。
        /// </summary>
        /// <returns>返回链接字符串。</returns>
        public virtual string GenerateSimulativeLink()
        {
            if (Route.IsNull())
                return InitialLink;

            return Route.GenerateSimulativeLink();
        }

    }
}
