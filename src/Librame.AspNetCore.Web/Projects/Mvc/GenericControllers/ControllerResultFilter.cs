// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Librame.AspNetCore.Web.Projects
{
    using Extensions;

    /// <summary>
    /// A filter implementation which delegates to the controller for result filter interfaces.
    /// </summary>
    internal class ControllerResultFilter : IAsyncResultFilter, IOrderedFilter
    {
        // Controller-filter methods run farthest from the result by default.
        /// <inheritdoc />
        public int Order { get; set; } = int.MinValue;


        /// <inheritdoc />
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递")]
        public Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next)
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
                //    nameof(ResultExecutingContext)));
                throw new InvalidOperationException($"The '{nameof(context.Controller)}' property of '{nameof(ResultExecutingContext)}' must not be null.");
            }

            if (controller is IAsyncResultFilter asyncResultFilter)
            {
                return asyncResultFilter.OnResultExecutionAsync(context, next);
            }
            else if (controller is IResultFilter resultFilter)
            {
                return ExecuteResultFilter(context, next, resultFilter);
            }
            else
            {
                return next();
            }
        }

        private static async Task ExecuteResultFilter(
            ResultExecutingContext context,
            ResultExecutionDelegate next,
            IResultFilter resultFilter)
        {
            resultFilter.OnResultExecuting(context);
            if (!context.Cancel)
            {
                resultFilter.OnResultExecuted(await next.Invoke().ConfigureAwait());
            }
        }
    }
}
