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

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 带用户泛型参数的页面应用模型特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class PageApplicationModelWithUserAttribute : PageApplicationModelAttribute
    {
        /// <summary>
        /// 构造一个 <see cref="PageApplicationModelWithUserAttribute"/>。
        /// </summary>
        /// <param name="implementationType">给定的实现类型。</param>
        public PageApplicationModelWithUserAttribute(Type implementationType)
            : base(implementationType)
        {
            if (implementationType.IsAbstract || !implementationType.IsGenericTypeDefinition)
                throw new InvalidOperationException("Implementation type can't be abstract or non generic.");

            if (implementationType.GenericTypeArguments.Length != 1)
                throw new InvalidOperationException("Implementation type contains wrong generic arity.");
        }

    }
}
