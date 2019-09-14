#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Reflection;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 带用户泛型参数的应用站点模板特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ApplicationSiteTemplateWithUserAttribute : ApplicationSiteTemplateAttribute
    {
        /// <summary>
        /// 构造一个 <see cref="ApplicationSiteTemplateWithUserAttribute"/>。
        /// </summary>
        /// <param name="implementationType">给定的实现类型。</param>
        public ApplicationSiteTemplateWithUserAttribute(Type implementationType)
            : base(implementationType)
        {
            if (implementationType.IsAbstract || !implementationType.IsGenericTypeDefinition)
                throw new InvalidOperationException("Implementation type can't be abstract or non generic.");

            //if (implementationType.GenericTypeArguments.Length != 1)
            //    throw new InvalidOperationException("Implementation type contains wrong generic arity.");
        }


        /// <summary>
        /// 制作泛型类型信息。
        /// </summary>
        /// <param name="userType">给定的用户类型。</param>
        /// <returns>返回 <see cref="TypeInfo"/>。</returns>
        public TypeInfo MakeGenericTypeInfo(Type userType)
        {
            var type = ImplementationType.MakeGenericType(userType);
            return type.GetTypeInfo();
        }

    }
}
