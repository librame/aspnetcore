#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.AspNetCore.Mvc.ViewEngines
{
    /// <summary>
    /// 抽象 <see cref="ICompositeViewEngine"/> 静态扩展。
    /// </summary>
    public static class AbstractionCompositeViewEngineExtensions
    {
        /// <summary>
        /// 异步需要主题包视图。
        /// </summary>
        /// <typeparam name="TResult">指定的结果类型。</typeparam>
        /// <param name="engine">给定的 <see cref="ICompositeViewEngine"/>。</param>
        /// <param name="context">给定的 <see cref="ActionContext"/>。</param>
        /// <param name="viewName">给定的视图名称。</param>
        /// <param name="processFactory">给定的视图处理工厂方法。</param>
        /// <returns>返回 <typeparamref name="TResult"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static TResult RequiredThemepackViewAsync<TResult>(this ICompositeViewEngine engine, ActionContext context,
            string viewName, Func<string, TResult> processFactory)
        {
            engine.NotNull(nameof(engine));
            processFactory.NotNull(nameof(processFactory));

            var result = engine.FindView(context, viewName, isMainPage: false);
            if (result.Success)
            {
                return processFactory.Invoke(viewName); // ex: html.RenderPartialAsync(viewName, model)
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
