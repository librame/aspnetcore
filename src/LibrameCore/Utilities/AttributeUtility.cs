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
using System.Linq;

namespace LibrameCore.Utilities
{
    /// <summary>
    /// <see cref="System.Attribute"/> 实用工具。
    /// </summary>
    public static class AttributeUtility
    {
        /// <summary>
        /// 获取类属性。
        /// </summary>
        /// <typeparam name="T">指定要获取的类型。</typeparam>
        /// <typeparam name="TAttribute">指定要获取的属性类型。</typeparam>
        /// <param name="inherit">搜索此成员的继承链以查找这些属性，则为 true；否则为 false。</param>
        /// <returns>返回属性对象。</returns>
        public static TAttribute Attribute<T, TAttribute>(bool inherit = false)
            where TAttribute : Attribute
        {
            return typeof(T).Attribute<TAttribute>(inherit);
        }
        /// <summary>
        /// 获取类属性。
        /// </summary>
        /// <typeparam name="TAttribute">指定要获取的属性类型。</typeparam>
        /// <param name="type">给定的类型。</param>
        /// <param name="inherit">搜索此成员的继承链以查找这些属性，则为 true；否则为 false。</param>
        /// <returns>返回属性对象。</returns>
        public static TAttribute Attribute<TAttribute>(this Type type, bool inherit = false)
            where TAttribute : Attribute
        {
            return type.NotNull(nameof(type)).GetTypeInfo().Attribute<TAttribute>(inherit);
        }


        /// <summary>
        /// 获取此成员信息包含的自定义属性特性。
        /// </summary>
        /// <typeparam name="TAttribute">指定的属性类型。</typeparam>
        /// <param name="member">指定的成员信息。</param>
        /// <param name="inherit">指定是否搜索该成员的继承链以查找这些特性。</param>
        /// <returns>返回自定义属性对象。</returns>
        public static TAttribute Attribute<TAttribute>(this MemberInfo member, bool inherit = false)
            where TAttribute : Attribute
        {
            var attribs = member.NotNull(nameof(member)).GetCustomAttributes(typeof(TAttribute), inherit);

            return (TAttribute)attribs?.FirstOrDefault();
        }

    }
}
