#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Librame.AspNetCore.Web.Projects
{
    using Extensions;

    /// <summary>
    /// 带用户的泛型控制器模型提供程序。
    /// </summary>
    /// <remarks>
    /// 参考 <see cref="ControllerFeatureProvider"/>。
    /// </remarks>
    public class GenericControllerModelProviderWithUser : IApplicationFeatureProvider<ControllerFeature>
    {
        private const string ControllerTypeNameSuffix = "Controller";
        private readonly Type _userType;


        /// <summary>
        /// 构造一个 <see cref="GenericControllerModelProviderWithUser"/>。
        /// </summary>
        /// <param name="userType">给定的用户类型。</param>
        public GenericControllerModelProviderWithUser(Type userType)
        {
            _userType = userType.NotNull(nameof(userType));
        }


        /// <summary>
        /// 填充特征。
        /// </summary>
        /// <param name="parts">给定的 <see cref="IEnumerable{ApplicationPart}"/>。</param>
        /// <param name="feature">给定的 <see cref="ControllerFeature"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var part in parts.OfType<IApplicationPartTypeProvider>())
            {
                foreach (var type in part.Types.Where(IsController))
                {
                    // 限定为泛型应用模型特性的应用模型集合
                    if (type.TryGetCustomAttribute(out GenericApplicationModelAttribute attribute))
                    {
                        var implementationType = attribute.ApplyGenericTypeIfNull(type)
                            .BuildImplementationType(_userType);

                        if (!feature.Controllers.Contains(implementationType))
                            feature.Controllers.Add(implementationType.GetTypeInfo());
                    }
                }
            }
        }

        /// <summary>
        /// Determines if a given <paramref name="typeInfo"/> is a controller.
        /// </summary>
        /// <param name="typeInfo">The <see cref="TypeInfo"/> candidate.</param>
        /// <returns><code>true</code> if the type is a controller; otherwise <code>false</code>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        protected virtual bool IsController(TypeInfo typeInfo)
        {
            if (!typeInfo.IsClass)
            {
                return false;
            }

            if (typeInfo.IsAbstract)
            {
                return false;
            }

            // We only consider public top-level classes as controllers. IsPublic returns false for nested
            // classes, regardless of visibility modifiers
            if (!typeInfo.IsPublic)
            {
                return false;
            }

            //if (typeInfo.ContainsGenericParameters)
            //{
            //    return false;
            //}

            if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
            {
                return false;
            }

            if (!typeInfo.Name.EndsWith(ControllerTypeNameSuffix, StringComparison.OrdinalIgnoreCase) &&
                !typeInfo.IsDefined(typeof(ControllerAttribute)))
            {
                return false;
            }

            return true;
        }

    }
}
