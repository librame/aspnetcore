// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Librame.AspNetCore.Web.Projects
{
    internal class FilterDescriptorOrderComparer : IComparer<FilterDescriptor>
    {
        public static FilterDescriptorOrderComparer Comparer { get; } = new FilterDescriptorOrderComparer();

        public int Compare(FilterDescriptor x, FilterDescriptor y)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            if (y == null)
            {
                throw new ArgumentNullException(nameof(y));
            }

            if (x.Order == y.Order)
            {
                return x.Scope.CompareTo(y.Scope);
            }
            else
            {
                return x.Order.CompareTo(y.Order);
            }
        }
    }
}
