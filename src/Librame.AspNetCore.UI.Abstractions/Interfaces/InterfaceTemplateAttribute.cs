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
    using Extensions;

    /// <summary>
    /// 界面模板特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class InterfaceTemplateAttribute : Attribute
    {
        /// <summary>
        /// 构造一个 <see cref="InterfaceTemplateAttribute"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="implementationType"/> is null.
        /// </exception>
        /// <param name="implementationType">给定的实现类型。</param>
        public InterfaceTemplateAttribute(Type implementationType)
        {
            ImplementationType = implementationType.NotNull(nameof(implementationType));
        }


        /// <summary>
        /// 实现类型。
        /// </summary>
        public Type ImplementationType { get; }
    }
}
