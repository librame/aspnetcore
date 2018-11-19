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

namespace Librame.AspNetCore.Identity.UI.Pages
{
    /// <summary>
    /// 内部 UI 身份特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class InternalUIIdentityAttribute : Attribute
    {
        /// <summary>
        /// 构造一个 <see cref="InternalUIIdentityAttribute"/> 实例。
        /// </summary>
        /// <param name="implementationTemplate">给定的实现模板类型。</param>
        public InternalUIIdentityAttribute(Type implementationTemplate)
        {
            Template = implementationTemplate;
        }


        /// <summary>
        /// 模板类型。
        /// </summary>
        public Type Template { get; }
    }
}
