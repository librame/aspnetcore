#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.ViewEngines
{
    /// <summary>
    /// <see cref="ICompositeViewEngine"/> 静态扩展。
    /// </summary>
    public static class UiCompositeViewEngineExtensions
    {
        /// <summary>
        /// 异步渲染主题包部分视图。
        /// </summary>
        /// <param name="engine">给定的 <see cref="ICompositeViewEngine"/>。</param>
        /// <param name="context">给定的 <see cref="ActionContext"/>。</param>
        /// <param name="viewName">给定的视图名称。</param>
        /// <param name="renderFactory">给定的渲染工厂方法。</param>
        /// <returns>返回一个异步操作。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static Task RenderThemepackPartialViewAsync(this ICompositeViewEngine engine, ActionContext context, string viewName,
            Func<string, Task> renderFactory)
        {
            engine.NotNull(nameof(engine));
            renderFactory.NotNull(nameof(renderFactory));

            var result = engine.FindView(context, viewName, isMainPage: false);
            if (result.Success)
            {
                return renderFactory.Invoke(viewName); // html.RenderPartialAsync(viewName, model)
            }
            else
            {
                throw new InvalidOperationException($"The current UI layout requires a partial view '{viewName}' " +
                    $"usually located at '/Pages/{viewName}' or at '/Views/Shared/{viewName}' to work. Based on your configuration " +
                    $"we have looked at it in the following locations: {Environment.NewLine}{string.Join(Environment.NewLine, result.SearchedLocations)}.");
            }
        }

    }
}
