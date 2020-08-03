// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Web.Projects
{
    using Extensions;

    /// <summary>
    /// A filter implementation which delegates to the controller for action filter interfaces.
    /// </summary>
    internal class ControllerActionFilter : IAsyncActionFilter, IOrderedFilter
    {
        // Controller-filter methods run farthest from the action by default.
        /// <inheritdoc />
        public int Order { get; set; } = int.MinValue;


        /// <inheritdoc />
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递")]
        public Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            var controller = context.Controller;
            if (controller == null)
            {
                //throw new InvalidOperationException(Resources.FormatPropertyOfTypeCannotBeNull(
                //    nameof(context.Controller),
                //    nameof(ActionExecutingContext)));
                throw new InvalidOperationException($"The '{nameof(context.Controller)}' property of '{nameof(ActionExecutingContext)}' must not be null.");
            }

            if (controller is IAsyncActionFilter asyncActionFilter)
            {
                return asyncActionFilter.OnActionExecutionAsync(context, next);
            }
            else if (controller is IActionFilter actionFilter)
            {
                return ExecuteActionFilter(context, next, actionFilter);
            }
            else
            {
                return next();
            }
        }

        private static async Task ExecuteActionFilter(
            ActionExecutingContext context,
            ActionExecutionDelegate next,
            IActionFilter actionFilter)
        {
            actionFilter.OnActionExecuting(context);
            if (context.Result == null)
            {
                actionFilter.OnActionExecuted(await next.Invoke().ConfigureAwait());
            }
        }
    }
}
