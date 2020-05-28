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

namespace Librame.AspNetCore.Web.Descriptors
{
    using Extensions;

    /// <summary>
    /// 动作路由描述符。
    /// </summary>
    public class ActionRouteDescriptor : AbstractRouteDescriptor
    {
        /// <summary>
        /// 动作键名。
        /// </summary>
        public const string ActionKey = "action";

        /// <summary>
        /// 控制器键名。
        /// </summary>
        public const string ControllerKey = "controller";


        /// <summary>
        /// 构造一个 <see cref="AbstractRouteDescriptor"/>。
        /// </summary>
        /// <param name="action">给定的动作。</param>
        /// <param name="controller">给定的控制器。</param>
        /// <param name="area">给定的区域（可选）。</param>
        /// <param name="id">给定的参数（可选）。</param>
        /// <param name="lockedArea">锁定区域（默认锁定；表示区域不会在环境正常化中修改）。</param>
        public ActionRouteDescriptor(string action, string controller,
            string area = null, string id = null, bool lockedArea = true)
            : base(area, id, lockedArea)
        {
            Action = action.NotEmpty(nameof(action));
            Controller = controller.NotEmpty(nameof(controller));
        }

        private ActionRouteDescriptor(bool lockedArea)
            : base(lockedArea: lockedArea)
        {
        }


        /// <summary>
        /// 动作。
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        /// 控制器。
        /// </summary>
        public string Controller { get; private set; }


        /// <summary>
        /// 改变动作。
        /// </summary>
        /// <param name="newAction">给定的新动作。</param>
        /// <returns>返回 <see cref="ActionRouteDescriptor"/>。</returns>
        public ActionRouteDescriptor ChangeAction(string newAction)
        {
            Action = newAction.NotEmpty(nameof(newAction));
            return this;
        }

        /// <summary>
        /// 改变控制器。
        /// </summary>
        /// <param name="newController">给定的新控制器。</param>
        /// <returns>返回 <see cref="ActionRouteDescriptor"/>。</returns>
        public ActionRouteDescriptor ChangeController(string newController)
        {
            Controller = newController.NotEmpty(nameof(newController));
            return this;
        }


        /// <summary>
        /// 带有动作。
        /// </summary>
        /// <param name="newAction">给定的新动作。</param>
        /// <returns>返回 <see cref="ActionRouteDescriptor"/>。</returns>
        public ActionRouteDescriptor WithAction(string newAction)
        {
            newAction.NotEmpty(nameof(newAction));
            return new ActionRouteDescriptor(newAction, Controller, Area, Id);
        }

        /// <summary>
        /// 带有控制器。
        /// </summary>
        /// <param name="newController">给定的新控制器。</param>
        /// <returns>返回 <see cref="ActionRouteDescriptor"/>。</returns>
        public ActionRouteDescriptor WithController(string newController)
        {
            newController.NotEmpty(nameof(newController));
            return new ActionRouteDescriptor(Action, newController, Area, Id);
        }

        /// <summary>
        /// 带有动作和控制器。
        /// </summary>
        /// <param name="newAction">给定的新动作。</param>
        /// <param name="newController">给定的新控制器。</param>
        /// <returns>返回 <see cref="ActionRouteDescriptor"/>。</returns>
        public ActionRouteDescriptor WithActionAndController(string newAction, string newController)
        {
            newAction.NotEmpty(nameof(newAction));
            newController.NotEmpty(nameof(newController));
            return new ActionRouteDescriptor(newAction, newController, Area, Id);
        }


        /// <summary>
        /// 获取视图名称。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string GetViewName()
            => Action;


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

            if ((updateOnlyNullPropertyValues && Action.IsNull() || !updateOnlyNullPropertyValues)
                && ambientValues.TryGetValue(ActionKey, out result))
            {
                Action = result;
            }

            if ((updateOnlyNullPropertyValues && Controller.IsNull() || !updateOnlyNullPropertyValues)
                && ambientValues.TryGetValue(ControllerKey, out result))
            {
                Controller = result;
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

            if ((updateOnlyNullPropertyValues && Action.IsNull() || !updateOnlyNullPropertyValues)
                && ambientValues.TryGetValue(ActionKey, out result))
            {
                Action = result?.ToString();
            }

            if ((updateOnlyNullPropertyValues && Controller.IsNull() || !updateOnlyNullPropertyValues)
                && ambientValues.TryGetValue(ControllerKey, out result))
            {
                Controller = result?.ToString();
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

            if (updateOnlyNotNullPropertyValues && Action.IsNotNull() || !updateOnlyNotNullPropertyValues)
                ambientValues[ActionKey] = Action;

            if (updateOnlyNotNullPropertyValues && Controller.IsNotNull() || !updateOnlyNotNullPropertyValues)
                ambientValues[ControllerKey] = Controller;
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

            if (updateOnlyNotNullPropertyValues && Action.IsNotNull() || !updateOnlyNotNullPropertyValues)
                ambientValues[ActionKey] = Action;

            if (updateOnlyNotNullPropertyValues && Controller.IsNotNull() || !updateOnlyNotNullPropertyValues)
                ambientValues[ControllerKey] = Controller;
        }


        /// <summary>
        /// 判定路由值集合能否解析为动作路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public static bool CanParse(IDictionary<string, string> routeValues)
        {
            routeValues.NotNull(nameof(routeValues));
            return routeValues.ContainsKey(ActionKey) || routeValues.ContainsKey(ControllerKey);
        }

        /// <summary>
        /// 判定路由值集合能否解析为动作路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public static bool CanParse(RouteValueDictionary routeValues)
        {
            routeValues.NotNull(nameof(routeValues));
            return routeValues.ContainsKey(ActionKey) || routeValues.ContainsKey(ControllerKey);
        }


        /// <summary>
        /// 从路由值集合中解析动作路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回 <see cref="ActionRouteDescriptor"/>。</returns>
        public static ActionRouteDescriptor Parse(IDictionary<string, string> routeValues)
        {
            // 初始解锁区域，以免丢失区域参数
            var descriptor = new ActionRouteDescriptor(lockedArea: false);
            descriptor.NormalizeFromAmbient(routeValues, updateOnlyNullPropertyValues: false);

            descriptor.LockedArea = true;
            return descriptor;
        }

        /// <summary>
        /// 从路由值集合中解析动作路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回 <see cref="ActionRouteDescriptor"/>。</returns>
        public static ActionRouteDescriptor Parse(RouteValueDictionary routeValues)
        {
            // 初始解锁区域，以免丢失区域参数
            var descriptor = new ActionRouteDescriptor(lockedArea: false);
            descriptor.NormalizeFromAmbient(routeValues, updateOnlyNullPropertyValues: false);

            descriptor.LockedArea = true;
            return descriptor;
        }


        /// <summary>
        /// 尝试从路由值集合中解析动作路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <param name="result">输出 <see cref="ActionRouteDescriptor"/> 或 NULL。</param>
        /// <returns>返回布尔值。</returns>
        public static bool TryParse(IDictionary<string, string> routeValues,
            out ActionRouteDescriptor result)
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
        /// 尝试从路由值集合中解析动作路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <param name="result">输出 <see cref="ActionRouteDescriptor"/> 或 NULL。</param>
        /// <returns>返回布尔值。</returns>
        public static bool TryParse(RouteValueDictionary routeValues,
            out ActionRouteDescriptor result)
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
