#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Applications
{
    using AspNetCore.Identity.Builders;
    using AspNetCore.Web.Builders;
    using Extensions;
    using Extensions.Core.Builders;

    /// <summary>
    /// 身份泛型类型定义映射器。
    /// </summary>
    public class IdentityGenericTypeDefinitionMapper : IGenericTypeDefinitionMapper
    {
        /// <summary>
        /// 映射泛型类型参数集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <param name="genericTypeParameters">给定的泛型类型参数数组。</param>
        /// <returns>返回类型数组集合。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递")]
        public virtual Type[] MapGenericTypeArguments(IWebBuilder builder, Type[] genericTypeParameters)
        {
            if (!builder.TryGetBuilder(out IIdentityBuilderDecorator decorator))
                throw new InvalidOperationException($"You need to register to builder.{nameof(IdentityBuilderDecoratorExtensions.AddIdentity)}().");
            
            genericTypeParameters.NotNull(nameof(genericTypeParameters));

            return genericTypeParameters.Length switch
            {
                1 => new Type[]
                {
                    decorator.Source.UserType
                },

                2 => new Type[]
                {
                    decorator.Source.UserType,
                    decorator.DataBuilder.AccessorMappingDescriptor.GenId.ArgumentType
                },

                3 => new Type[]
                {
                    decorator.Source.UserType,
                    decorator.DataBuilder.AccessorMappingDescriptor.GenId.ArgumentType,
                    decorator.DataBuilder.AccessorMappingDescriptor.CreatedBy.ArgumentType
                },

                _ => Array.Empty<Type>()
            };
        }

    }
}
