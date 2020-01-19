// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Librame.AspNetCore.Web.Projects
{
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class ResetControllerActionDescriptorProvider : IActionDescriptorProvider
    {
        private readonly ApplicationPartManager _partManager;
        private readonly ResetApplicationModelFactory _applicationModelFactory;

        public ResetControllerActionDescriptorProvider(
            ApplicationPartManager partManager,
            ResetApplicationModelFactory applicationModelFactory)
        {
            if (partManager == null)
            {
                throw new ArgumentNullException(nameof(partManager));
            }

            if (applicationModelFactory == null)
            {
                throw new ArgumentNullException(nameof(applicationModelFactory));
            }

            _partManager = partManager;
            _applicationModelFactory = applicationModelFactory;
        }

        public int Order => -1000;

        /// <inheritdoc />
        public void OnProvidersExecuting(ActionDescriptorProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            foreach (var descriptor in GetDescriptors())
            {
                context.Results.Add(descriptor);
            }
        }

        /// <inheritdoc />
        public void OnProvidersExecuted(ActionDescriptorProviderContext context)
        {
            // After all of the providers have run, we need to provide a 'null' for each all of route values that
            // participate in action selection.
            //
            // This is important for scenarios like Razor Pages, that use the 'page' route value. An action that
            // uses 'page' shouldn't match when 'action' is set, and an action that uses 'action' shouldn't match when
            // 'page is specified.
            //
            // Or for another example, consider areas. A controller that's not in an area needs a 'null' value for
            // area so it can't match when the route produces an 'area' value.
            var keys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (var i = 0; i < context.Results.Count; i++)
            {
                var action = context.Results[i];
                foreach (var key in action.RouteValues.Keys)
                {
                    keys.Add(key);
                }
            }

            for (var i = 0; i < context.Results.Count; i++)
            {
                var action = context.Results[i];
                foreach (var key in keys)
                {
                    if (!action.RouteValues.ContainsKey(key))
                    {
                        action.RouteValues.Add(key, null);
                    }
                }
            }
        }

        internal IEnumerable<ControllerActionDescriptor> GetDescriptors()
        {
            var controllerTypes = GetControllerTypes();
            var application = _applicationModelFactory.CreateApplicationModel(controllerTypes);
            return ControllerActionDescriptorBuilder.Build(application);
        }

        private IEnumerable<TypeInfo> GetControllerTypes()
        {
            var feature = new ControllerFeature();
            _partManager.PopulateFeature(feature);

            return feature.Controllers;
        }
    }
}
