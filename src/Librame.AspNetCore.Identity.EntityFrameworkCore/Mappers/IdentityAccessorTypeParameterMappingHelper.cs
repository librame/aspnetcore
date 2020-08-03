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

namespace Librame.AspNetCore.Identity.Mappers
{
    using AspNetCore.Identity.Accessors;
    using Extensions;
    using Extensions.Core.Mappers;
    using Extensions.Data.Mappers;

    /// <summary>
    /// 身份访问器类型参数映射助手。
    /// </summary>
    public static class IdentityAccessorTypeParameterMappingHelper
    {
        /// <summary>
        /// 解析身份映射器。
        /// </summary>
        /// <param name="baseMapper">给定的基础 <see cref="AccessorTypeParameterMapper"/>。</param>
        /// <returns>返回 <see cref="IdentityAccessorTypeParameterMapper"/>。</returns>
        public static IdentityAccessorTypeParameterMapper ParseMapper(AccessorTypeParameterMapper baseMapper)
        {
            var mappings = ParseCollection(baseMapper?.Accessor.ArgumentType);
            return new IdentityAccessorTypeParameterMapper(baseMapper, mappings);
        }


        /// <summary>
        /// 解析类型参数映射集合（默认以定义参数类型名称为键名）。
        /// </summary>
        /// <param name="accessorTypeImplementation">给定的访问器类型实现。</param>
        /// <returns>返回 <see cref="TypeParameterMappingCollection"/>。</returns>
        public static TypeParameterMappingCollection ParseCollection(Type accessorTypeImplementation)
        {
            var accessorTypeDefinition = typeof(IIdentityAccessor<,,,,,,>);

            // 因访问器默认服务类型为 IAccessor，所以不强制实现访问器泛型类型定义
            if (!accessorTypeImplementation.IsImplementedInterfaceType(accessorTypeDefinition, out var resultType))
                return null;

            return TypeParameterMappingHelper.ParseCollection(accessorTypeDefinition, resultType);
        }

    }
}
