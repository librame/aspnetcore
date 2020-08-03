#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Librame.AspNetCore.Web.Applications
{
    using AspNetCore.Web.Builders;
    using Extensions;

    /// <summary>
    /// 泛型应用模型特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class GenericApplicationModelAttribute : Attribute
    {
        private readonly Type _baseGenericTypeDefinitionMapperType
            = typeof(IGenericTypeDefinitionMapper);

        private Type[] _genericTypeParameters;


        /// <summary>
        /// 构造一个 <see cref="GenericApplicationModelAttribute"/>。
        /// </summary>
        /// <param name="genericTypeDefinitionMapperType">给定实现 <see cref="IGenericTypeDefinitionMapper"/> 接口的泛型类型定义映射器类型。</param>
        /// <param name="genericTypeDefinition">给定的泛型类型定义（可选，默认为当前类型；不支持抽象或非泛型类型）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public GenericApplicationModelAttribute(Type genericTypeDefinitionMapperType,
            Type genericTypeDefinition = null)
        {
            if (!genericTypeDefinitionMapperType.IsImplementedInterfaceType(_baseGenericTypeDefinitionMapperType))
                throw new NotSupportedException($"The generic type definition mapper type '{genericTypeDefinitionMapperType}' does not implementation '{_baseGenericTypeDefinitionMapperType}'");

            if (genericTypeDefinition.IsNotNull())
                ValidGenericTypeDefinition(genericTypeDefinition);

            GenericTypeDefinitionMapperType = genericTypeDefinitionMapperType;
            GenericTypeDefinition = genericTypeDefinition;
        }


        /// <summary>
        /// 泛型类型定义映射器类型。
        /// </summary>
        public Type GenericTypeDefinitionMapperType { get; }

        /// <summary>
        /// 泛型类型定义（不支持抽象或非泛型类型）。
        /// </summary>
        public Type GenericTypeDefinition { get; private set; }


        /// <summary>
        /// 如果当前泛型类型为空，则应用泛型类型。
        /// </summary>
        /// <param name="genericTypeDefinition">给定的泛型类型定义（可选，默认为当前类型；不支持抽象或非泛型类型）。</param>
        /// <returns>返回 <see cref="GenericApplicationModelAttribute"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public GenericApplicationModelAttribute ApplyGenericTypeDefinitionIfNull(Type genericTypeDefinition)
        {
            if (GenericTypeDefinition.IsNull())
            {
                genericTypeDefinition.NotNull(nameof(genericTypeDefinition));
                GenericTypeDefinition = ValidGenericTypeDefinition(genericTypeDefinition);
            }

            return this;
        }


        /// <summary>
        /// 构建实现类型。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <returns>返回 <see cref="Type"/>。</returns>
        public Type BuildImplementationType(IWebBuilder builder)
        {
            var mapper = GenericTypeDefinitionMapperType.EnsureCreate<IGenericTypeDefinitionMapper>();
            return BuildImplementationType(mapper.MapGenericTypeArguments(builder, _genericTypeParameters));
        }

        /// <summary>
        /// 构建实现类型。
        /// </summary>
        /// <param name="typeArguments">给定的类型参数数组。</param>
        /// <returns>返回 <see cref="Type"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
        public Type BuildImplementationType(params Type[] typeArguments)
        {
            if (GenericTypeDefinition.IsNull())
                throw new InvalidOperationException($"The generic type definition is null. You should use the {nameof(ApplyGenericTypeDefinitionIfNull)}().");

            if (_genericTypeParameters.Length != typeArguments.Length)
                throw new InvalidOperationException($"Unmatched generic type definition '{GenericTypeDefinition}' type arguments.");

            return GenericTypeDefinition.MakeGenericType(typeArguments);
        }


        private Type ValidGenericTypeDefinition(Type genericTypeDefinition)
        {
            if (genericTypeDefinition.IsAbstract || !genericTypeDefinition.IsGenericTypeDefinition)
                throw new InvalidOperationException($"The generic type definition '{genericTypeDefinition}' can't be abstract or non generic.");

            // 泛型类型定义使用 GenericTypeParameters 获取定义的参数数组
            _genericTypeParameters = genericTypeDefinition.GetTypeInfo().GenericTypeParameters;

            return genericTypeDefinition;
        }

    }
}
