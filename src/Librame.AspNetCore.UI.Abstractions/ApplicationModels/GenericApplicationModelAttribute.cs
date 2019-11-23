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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 泛型应用模型特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class GenericApplicationModelAttribute : Attribute
    {
        /// <summary>
        /// 构造一个 <see cref="GenericApplicationModelAttribute"/>。
        /// </summary>
        /// <param name="genericType">给定的泛型类型（可选；不支持抽象或非泛型类型）。</param>
        public GenericApplicationModelAttribute(Type genericType = null)
        {
            GenericType = genericType;
        }


        /// <summary>
        /// 泛型类型（不支持抽象或非泛型类型）。
        /// </summary>
        public Type GenericType { get; set; }


        /// <summary>
        /// 如果当前泛型类型为 NULL，则应用泛型类型。
        /// </summary>
        /// <param name="genericType">给定的泛型类型。</param>
        /// <returns>返回 <see cref="GenericApplicationModelAttribute"/>。</returns>
        public GenericApplicationModelAttribute ApplyGenericTypeIfNull(Type genericType)
        {
            if (GenericType.IsNull())
                GenericType = genericType.NotNull(nameof(genericType));

            return this;
        }


        /// <summary>
        /// 构建实现类型。
        /// </summary>
        /// <param name="typeArguments">给定的类型参数数组。</param>
        /// <returns>返回 <see cref="Type"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
        public Type BuildImplementationType(params Type[] typeArguments)
        {
            GenericType.NotNull(nameof(GenericType));

            if (GenericType.IsAbstract || !GenericType.IsGenericTypeDefinition)
                throw new InvalidOperationException("Implementation type can't be abstract or non generic.");

            //if (GenericType.GenericTypeArguments.Length != 1)
            //    throw new InvalidOperationException("Implementation type contains wrong generic arity.");

            return GenericType.MakeGenericType(typeArguments);
        }

    }
}
