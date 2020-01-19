#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Web.Routings;
using Librame.Extensions;
using Librame.Extensions.Core.Resources;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Microsoft.Extensions.Localization
{
    /// <summary>
    /// 导航描述符静态扩展。
    /// </summary>
    public static class NavigationDescriptorExtensions
    {
        /// <summary>
        /// 表示为导航描述符。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="propertyExpression">给定用于导航的字符串定位器属性表达式（可选；默认使用路由视图名称）。</param>
        /// <param name="optionalAction">给定的 <see cref="INavigationDescriptorOptional"/> 动作（可选）。</param>
        /// <returns>返回 <see cref="NavigationDescriptor{TResource}"/>。</returns>
        public static NavigationDescriptor<TResource> AsNavigation<TResource>(this IStringLocalizer<TResource> localizer,
            RouteDescriptor route,
            Expression<Func<TResource, string>> propertyExpression = null,
            Action<INavigationDescriptorOptional> optionalAction = null)
            where TResource : class, IResource
            => new NavigationDescriptor<TResource>(route, localizer, propertyExpression, optionalAction);


        #region Change

        /// <summary>
        /// 改变路由。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="routeAction">给定的路由动作。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static TNavigation ChangeRoute<TNavigation>(this TNavigation navigation,
            Action<RouteDescriptor> routeAction)
            where TNavigation : AbstractNavigationDescriptor
        {
            navigation.NotNull(nameof(navigation));
            routeAction.NotNull(nameof(routeAction));

            routeAction.Invoke(navigation.Route);
            return navigation;
        }

        /// <summary>
        /// 改变路由。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newRouteFactory">给定的 <see cref="RouteDescriptor"/> 工厂方法。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static TNavigation ChangeRoute<TNavigation>(this TNavigation navigation,
            Func<RouteDescriptor, RouteDescriptor> newRouteFactory)
            where TNavigation : AbstractNavigationDescriptor
        {
            navigation.NotNull(nameof(navigation));
            newRouteFactory.NotNull(nameof(newRouteFactory));

            navigation.ChangeRoute(newRouteFactory.Invoke(navigation.Route));
            return navigation;
        }


        /// <summary>
        /// 改变可选配置。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="optionalAction">给定的 <see cref="INavigationDescriptorOptional"/> 动作。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "navigation")]
        public static TNavigation ChangeOptional<TNavigation>(this TNavigation navigation,
            Action<INavigationDescriptorOptional> optionalAction)
            where TNavigation : AbstractNavigationDescriptor
        {
            navigation.NotNull(nameof(navigation));

            navigation.ChangeOptional(optionalAction);
            return navigation;
        }


        /// <summary>
        /// 改变本地化定位器。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newLocalizer">给定的新 <see cref="IStringLocalizer"/>。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "navigation")]
        public static TNavigation ChangeLocalizer<TNavigation>(this TNavigation navigation,
            IStringLocalizer newLocalizer)
            where TNavigation : AbstractNavigationDescriptor
        {
            navigation.NotNull(nameof(navigation));

            navigation.ChangeLocalizer(newLocalizer);
            return navigation;
        }

        /// <summary>
        /// 改变本地化定位器。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newLocalizerName">给定的新本地化定位器名称。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "navigation")]
        public static TNavigation ChangeLocalizerName<TNavigation>(this TNavigation navigation,
            string newLocalizerName)
            where TNavigation : AbstractNavigationDescriptor
        {
            navigation.NotNull(nameof(navigation));

            navigation.ChangeLocalizerName(newLocalizerName);
            return navigation;
        }


        /// <summary>
        /// 改变文本。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newText">给定的新文本。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "navigation")]
        public static TNavigation ChangeText<TNavigation>(this TNavigation navigation,
            string newText)
            where TNavigation : AbstractNavigationDescriptor
        {
            navigation.NotNull(nameof(navigation));

            navigation.ChangeText(newText);
            return navigation;
        }

        #endregion


        #region New

        /// <summary>
        /// 用新路由动作以及当前其他参数构造一个新实例。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newAction">给定的新动作。</param>
        /// <param name="newLocalizerNameFactory">给定的新定位器名称工厂方法（可选；默认使用路由动作）。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        public static TNavigation NewRouteAction<TNavigation>(this TNavigation navigation,
            string newAction, Func<RouteDescriptor, string> newLocalizerNameFactory = null)
            where TNavigation : AbstractNavigationDescriptor
            => navigation.NewRoute(route => route.NewAction(newAction), newLocalizerNameFactory ?? (route => newAction));

        /// <summary>
        /// 用新路由动作以及当前其他参数构造一个新实例。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newAction">给定的新动作。</param>
        /// <param name="newLocalizerName">给定用于导航的新字符串定位器名称。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        public static TNavigation NewRouteAction<TNavigation>(this TNavigation navigation,
            string newAction, string newLocalizerName)
            where TNavigation : AbstractNavigationDescriptor
            => navigation.NewRoute(route => route.NewAction(newAction), newLocalizerName);


        /// <summary>
        /// 用新路由动作、控制器、区域（可选）以及当前其他参数构造一个新实例（默认使用路由动作）。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newAction">给定的新动作。</param>
        /// <param name="newController">给定的新控制器。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        public static TNavigation NewRouteController<TNavigation>(this TNavigation navigation,
            string newAction, string newController)
            where TNavigation : AbstractNavigationDescriptor
            => navigation.NewRoute(route => RouteDescriptor.ByController(newAction, newController, route.Area), newAction);

        ///// <summary>
        ///// 用新路由动作、控制器、区域（可选）以及当前其他参数构造一个新实例（默认使用路由动作）。
        ///// </summary>
        ///// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        ///// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        ///// <param name="newAction">给定的新动作。</param>
        ///// <param name="newController">给定的新控制器。</param>
        ///// <param name="newArea">给定的新区域（如果为空，则表示清空区域）。</param>
        ///// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        //public static TNavigation NewRouteController<TNavigation>(this TNavigation navigation,
        //    string newAction, string newController, string newArea)
        //    where TNavigation : AbstractNavigationDescriptor
        //    => navigation.NewRoute(route => RouteDescriptor.ByController(newAction, newController, newArea), newAction);


        /// <summary>
        /// 用新路由动作、控制器、区域（可选）以及当前其他参数构造一个新实例。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newAction">给定的新动作。</param>
        /// <param name="newController">给定的新控制器。</param>
        /// <param name="newLocalizerNameFactory">给定的新定位器名称工厂方法。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        public static TNavigation NewRouteController<TNavigation>(this TNavigation navigation,
            string newAction, string newController,
            Func<RouteDescriptor, string> newLocalizerNameFactory)
            where TNavigation : AbstractNavigationDescriptor
            => navigation.NewRoute(route => RouteDescriptor.ByController(newAction, newController, route.Area), newLocalizerNameFactory);

        /// <summary>
        /// 用新路由动作、控制器、区域（可选）以及当前其他参数构造一个新实例。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newAction">给定的新动作。</param>
        /// <param name="newController">给定的新控制器。</param>
        /// <param name="newLocalizerNameFactory">给定的新定位器名称工厂方法。</param>
        /// <param name="newArea">给定的新区域（如果为空，则表示清空区域）。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        public static TNavigation NewRouteController<TNavigation>(this TNavigation navigation,
            string newAction, string newController,
            Func<RouteDescriptor, string> newLocalizerNameFactory, string newArea)
            where TNavigation : AbstractNavigationDescriptor
            => navigation.NewRoute(route => RouteDescriptor.ByController(newAction, newController, newArea), newLocalizerNameFactory);


        /// <summary>
        /// 用新路由动作、控制器、区域（可选）以及当前其他参数构造一个新实例。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newAction">给定的新动作。</param>
        /// <param name="newController">给定的新控制器。</param>
        /// <param name="newLocalizerName">给定用于导航的新字符串定位器名称。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        public static TNavigation NewRouteController<TNavigation>(this TNavigation navigation,
            string newAction, string newController,
            string newLocalizerName)
            where TNavigation : AbstractNavigationDescriptor
            => navigation.NewRoute(route => RouteDescriptor.ByController(newAction, newController, route.Area), newLocalizerName);

        /// <summary>
        /// 用新路由动作、控制器、区域（可选）以及当前其他参数构造一个新实例。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newAction">给定的新动作。</param>
        /// <param name="newController">给定的新控制器。</param>
        /// <param name="newLocalizerName">给定用于导航的新字符串定位器名称。</param>
        /// <param name="newArea">给定的新区域（如果为空，则表示清空区域）。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        public static TNavigation NewRouteController<TNavigation>(this TNavigation navigation,
            string newAction, string newController,
            string newLocalizerName, string newArea)
            where TNavigation : AbstractNavigationDescriptor
            => navigation.NewRoute(route => RouteDescriptor.ByController(newAction, newController, newArea), newLocalizerName);


        /// <summary>
        /// 用新路由页面以及当前其他参数构造一个新实例（默认使用页面名称）。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newPage">给定的新页面。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        public static TNavigation NewRoutePage<TNavigation>(this TNavigation navigation,
            string newPage)
            where TNavigation : AbstractNavigationDescriptor
            => navigation.NewRoute(route => RouteDescriptor.ByPage(newPage, route.Area), route => route.PageName);

        /// <summary>
        /// 用新路由页面以及当前其他参数构造一个新实例（默认使用页面名称）。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newPage">给定的新页面。</param>
        /// <param name="newArea">给定的新区域（如果为空，则表示清空区域）。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        public static TNavigation NewRoutePage<TNavigation>(this TNavigation navigation,
            string newPage, string newArea)
            where TNavigation : AbstractNavigationDescriptor
            => navigation.NewRoute(route => RouteDescriptor.ByPage(newPage, newArea), route => route.PageName);


        /// <summary>
        /// 用新路由页面以及当前其他参数构造一个新实例。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newPage">给定的新页面。</param>
        /// <param name="newLocalizerNameFactory">给定的新定位器名称工厂方法。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        public static TNavigation NewRoutePage<TNavigation>(this TNavigation navigation,
            string newPage, Func<RouteDescriptor, string> newLocalizerNameFactory)
            where TNavigation : AbstractNavigationDescriptor
            => navigation.NewRoute(route => RouteDescriptor.ByPage(newPage, route.Area), newLocalizerNameFactory);

        /// <summary>
        /// 用新路由页面以及当前其他参数构造一个新实例。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newPage">给定的新页面。</param>
        /// <param name="newLocalizerNameFactory">给定的新定位器名称工厂方法。</param>
        /// <param name="newArea">给定的新区域（如果为空，则表示清空区域）。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        public static TNavigation NewRoutePage<TNavigation>(this TNavigation navigation,
            string newPage, Func<RouteDescriptor, string> newLocalizerNameFactory, string newArea)
            where TNavigation : AbstractNavigationDescriptor
            => navigation.NewRoute(route => RouteDescriptor.ByPage(newPage, newArea), newLocalizerNameFactory);


        ///// <summary>
        ///// 用新路由页面以及当前其他参数构造一个新实例。
        ///// </summary>
        ///// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        ///// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        ///// <param name="newPage">给定的新页面。</param>
        ///// <param name="newLocalizerName">给定用于导航的新字符串定位器名称。</param>
        ///// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        //public static TNavigation NewRoutePage<TNavigation>(this TNavigation navigation,
        //    string newPage, string newLocalizerName)
        //    where TNavigation : AbstractNavigationDescriptor
        //    => navigation.NewRoute(route => RouteDescriptor.ByPage(newPage, route.Area), newLocalizerName);

        /// <summary>
        /// 用新路由页面以及当前其他参数构造一个新实例。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newPage">给定的新页面。</param>
        /// <param name="newLocalizerName">给定用于导航的新字符串定位器名称。</param>
        /// <param name="newArea">给定的新区域（如果为空，则表示清空区域）。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        public static TNavigation NewRoutePage<TNavigation>(this TNavigation navigation,
            string newPage, string newLocalizerName, string newArea)
            where TNavigation : AbstractNavigationDescriptor
            => navigation.NewRoute(route => RouteDescriptor.ByPage(newPage, newArea), newLocalizerName);


        /// <summary>
        /// 用新路由以及当前其他参数构造一个新实例。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newRouteFactory">给定的新 <see cref="RouteDescriptor"/> 工厂方法。</param>
        /// <param name="newLocalizerNameFactory">给定的新定位器名称工厂方法。</param>
        /// <returns>返回 <typeparamref name="TNavigation"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static TNavigation NewRoute<TNavigation>(this TNavigation navigation,
            Func<RouteDescriptor, RouteDescriptor> newRouteFactory, Func<RouteDescriptor, string> newLocalizerNameFactory)
            where TNavigation : AbstractNavigationDescriptor
        {
            navigation.NotNull(nameof(navigation));
            newRouteFactory.NotNull(nameof(newRouteFactory));

            var newRoute = newRouteFactory.Invoke(navigation.Route);
            var newNavigation = navigation.NewRoute(newRoute, newLocalizerNameFactory.Invoke(newRoute));
            return (TNavigation)newNavigation;
        }

        /// <summary>
        /// 用新路由以及当前其他参数构造一个新实例。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newRouteFactory">给定的新 <see cref="RouteDescriptor"/> 工厂方法。</param>
        /// <param name="newLocalizerName">给定用于导航的新字符串定位器名称。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static TNavigation NewRoute<TNavigation>(this TNavigation navigation,
            Func<RouteDescriptor, RouteDescriptor> newRouteFactory, string newLocalizerName)
            where TNavigation : AbstractNavigationDescriptor
        {
            navigation.NotNull(nameof(navigation));
            newRouteFactory.NotNull(nameof(newRouteFactory));

            var newNavigation = navigation.NewRoute(newRouteFactory.Invoke(navigation.Route), newLocalizerName);
            return (TNavigation)newNavigation;
        }

        /// <summary>
        /// 用新路由以及当前其他参数构造一个新实例。
        /// </summary>
        /// <typeparam name="TNavigation">指定的导航描述符类型。</typeparam>
        /// <param name="navigation">给定的 <typeparamref name="TNavigation"/>。</param>
        /// <param name="newRoute">给定的新 <see cref="RouteDescriptor"/>。</param>
        /// <param name="newLocalizerNameFactory">给定的新定位器名称工厂方法。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static TNavigation NewRoute<TNavigation>(this TNavigation navigation,
            RouteDescriptor newRoute, Func<RouteDescriptor, string> newLocalizerNameFactory)
            where TNavigation : AbstractNavigationDescriptor
        {
            navigation.NotNull(nameof(navigation));
            newLocalizerNameFactory.NotNull(nameof(newLocalizerNameFactory));

            var newNavigation = navigation.NewRoute(newRoute, newLocalizerNameFactory.Invoke(newRoute));
            return (TNavigation)newNavigation;
        }

        #endregion

    }
}
