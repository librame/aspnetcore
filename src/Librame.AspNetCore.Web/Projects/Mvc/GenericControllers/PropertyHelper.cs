// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Librame.AspNetCore.Web.Projects
{
    internal class PropertyHelper
    {
        // We need to be able to check if a type is a 'ref struct' - but we need to be able to compile
        // for platforms where the attribute is not defined, like net46. So we can fetch the attribute
        // by late binding. If the attribute isn't defined, then we assume we won't encounter any
        // 'ref struct' types.
        private static readonly Type IsByRefLikeAttribute = Type.GetType("System.Runtime.CompilerServices.IsByRefLikeAttribute", throwOnError: false);


        // Using an array rather than IEnumerable, as target will be called on the hot path numerous times.
        private static readonly ConcurrentDictionary<Type, PropertyHelper[]> PropertiesCache =
            new ConcurrentDictionary<Type, PropertyHelper[]>();


        /// <summary>
        /// Initializes a fast <see cref="PropertyHelper"/>.
        /// This constructor does not cache the helper. For caching, use <see cref="GetProperties(Type)"/>.
        /// </summary>
        public PropertyHelper(PropertyInfo property)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Name = property.Name;
        }

        /// <summary>
        /// Gets the backing <see cref="PropertyInfo"/>.
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Gets (or sets in derived types) the property name.
        /// </summary>
        public virtual string Name { get; protected set; }


        /// <summary>
        /// Creates and caches fast property helpers that expose getters for every public get property on the
        /// specified type.
        /// </summary>
        /// <param name="type">The type to extract property accessors for.</param>
        /// <returns>A cached array of all public properties of the specified type.
        /// </returns>
        public static PropertyHelper[] GetProperties(Type type)
        {
            return GetProperties(type, p => CreateInstance(p), PropertiesCache);
        }

        private static PropertyHelper CreateInstance(PropertyInfo property)
        {
            return new PropertyHelper(property);
        }

        protected static PropertyHelper[] GetProperties(
            Type type,
            Func<PropertyInfo, PropertyHelper> createPropertyHelper,
            ConcurrentDictionary<Type, PropertyHelper[]> cache)
        {
            // Unwrap nullable types. This means Nullable<T>.Value and Nullable<T>.HasValue will not be
            // part of the sequence of properties returned by this method.
            type = Nullable.GetUnderlyingType(type) ?? type;

            if (!cache.TryGetValue(type, out var helpers))
            {
                // We avoid loading indexed properties using the Where statement.
                var properties = type.GetRuntimeProperties().Where(p => IsInterestingProperty(p));

                var typeInfo = type.GetTypeInfo();
                if (typeInfo.IsInterface)
                {
                    // Reflection does not return information about inherited properties on the interface itself.
                    properties = properties.Concat(typeInfo.ImplementedInterfaces.SelectMany(
                        interfaceType => interfaceType.GetRuntimeProperties().Where(p => IsInterestingProperty(p))));
                }

                helpers = properties.Select(p => createPropertyHelper(p)).ToArray();
                cache.TryAdd(type, helpers);
            }

            return helpers;
        }

        private static bool IsInterestingProperty(PropertyInfo property)
        {
            // For improving application startup time, do not use GetIndexParameters() api early in this check as it
            // creates a copy of parameter array and also we would like to check for the presence of a get method
            // and short circuit asap.
            return
                property.GetMethod != null &&
                property.GetMethod.IsPublic &&
                !property.GetMethod.IsStatic &&

                // PropertyHelper can't work with ref structs.
                !IsRefStructProperty(property) &&

                // Indexed properties are not useful (or valid) for grabbing properties off an object.
                property.GetMethod.GetParameters().Length == 0;
        }

        // PropertyHelper can't really interact with ref-struct properties since they can't be 
        // boxed and can't be used as generic types. We just ignore them.
        //
        // see: https://github.com/aspnet/Mvc/issues/8545
        private static bool IsRefStructProperty(PropertyInfo property)
        {
            return
                IsByRefLikeAttribute != null &&
                property.PropertyType.IsValueType &&
                property.PropertyType.IsDefined(IsByRefLikeAttribute);
        }
    }
}
