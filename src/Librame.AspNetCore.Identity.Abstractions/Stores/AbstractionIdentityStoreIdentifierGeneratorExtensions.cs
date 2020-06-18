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

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions;
    using Extensions.Core.Identifiers;
    using Extensions.Core.Services;
    using Extensions.Data.Stores;

    /// <summary>
    /// <see cref="IIdentityStoreIdentifierGenerator{TId}"/> 静态扩展。
    /// </summary>
    public static class AbstractionIdentityStoreIdentifierGeneratorExtensions
    {
        private static readonly Type _identifierTypeDefinition
            = typeof(IIdentifier<>);

        private static readonly Type _identifierGeneratorTypeDefinition
            = typeof(IStoreIdentifierGenerator<>);


        /// <summary>
        /// 获取身份存储标识符生成器。
        /// </summary>
        /// <typeparam name="TUser">指定实现 <see cref="IIdentifier{TId}"/> 的用户类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="serviceProvider">给定的 <see cref="IServiceProvider"/>。</param>
        /// <returns>返回 <see cref="IIdentityStoreIdentifierGenerator{TId}"/>。</returns>
        public static IIdentityStoreIdentifierGenerator<TId> GetIdentityStoreIdentifierGeneratorByUser<TUser, TId>
            (this IServiceProvider serviceProvider)
            where TId : IEquatable<TId>
            => serviceProvider.GetIdentityStoreIdentifierGeneratorByUser<TId>(typeof(TUser));

        /// <summary>
        /// 获取身份存储标识符生成器。
        /// </summary>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="serviceProvider">给定的 <see cref="IServiceProvider"/>。</param>
        /// <param name="userType">给定实现 <see cref="IIdentifier{TId}"/> 的用户类型。</param>
        /// <returns>返回 <see cref="IIdentityStoreIdentifierGenerator{TId}"/>。</returns>
        public static IIdentityStoreIdentifierGenerator<TId> GetIdentityStoreIdentifierGeneratorByUser<TId>
            (this IServiceProvider serviceProvider, Type userType)
            where TId : IEquatable<TId>
        {
            if (!userType.IsImplementedInterface(_identifierTypeDefinition, out var resultType))
                throw new InvalidOperationException($"The user type '{userType}' is not implemented {_identifierTypeDefinition}.");

            return serviceProvider.GetIdentityStoreIdentifierGenerator<TId>(resultType.GenericTypeArguments[0]);
        }

        /// <summary>
        /// 获取身份存储标识符生成器。
        /// </summary>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="serviceProvider">给定的 <see cref="IServiceProvider"/>。</param>
        /// <param name="idType">给定的生成式标识符类型。</param>
        /// <returns>返回 <see cref="IIdentityStoreIdentifierGenerator{TId}"/>。</returns>
        public static IIdentityStoreIdentifierGenerator<TId> GetIdentityStoreIdentifierGenerator<TId>
            (this IServiceProvider serviceProvider, Type idType)
            where TId : IEquatable<TId>
            => (IIdentityStoreIdentifierGenerator<TId>)serviceProvider.GetRequiredService(_identifierGeneratorTypeDefinition.MakeGenericType(idType));


        /// <summary>
        /// 获取身份存储标识符生成器。
        /// </summary>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <typeparam name="TUser">指定实现 <see cref="IIdentifier{TId}"/> 的用户类型。</typeparam>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactory"/>。</param>
        /// <returns>返回 <see cref="IIdentityStoreIdentifierGenerator{TId}"/>。</returns>
        public static IIdentityStoreIdentifierGenerator<TId> GetIdentityStoreIdentifierGeneratorByUser<TUser, TId>
            (this ServiceFactory serviceFactory)
            where TId : IEquatable<TId>
            => serviceFactory.GetIdentityStoreIdentifierGeneratorByUser<TId>(typeof(TUser));

        /// <summary>
        /// 获取身份存储标识符生成器。
        /// </summary>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactory"/>。</param>
        /// <param name="userType">给定实现 <see cref="IIdentifier{TId}"/> 的用户类型。</param>
        /// <returns>返回 <see cref="IIdentityStoreIdentifierGenerator{TId}"/>。</returns>
        public static IIdentityStoreIdentifierGenerator<TId> GetIdentityStoreIdentifierGeneratorByUser<TId>
            (this ServiceFactory serviceFactory, Type userType)
            where TId : IEquatable<TId>
        {
            if (!userType.IsImplementedInterface(_identifierTypeDefinition, out var resultType))
                throw new InvalidOperationException($"The user type '{userType}' is not implemented {_identifierTypeDefinition}.");

            return serviceFactory.GetIdentityStoreIdentifierGenerator<TId>(resultType.GenericTypeArguments[0]);
        }

        /// <summary>
        /// 获取身份存储标识符生成器。
        /// </summary>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactory"/>。</param>
        /// <param name="idType">给定的生成式标识符类型。</param>
        /// <returns>返回 <see cref="IIdentityStoreIdentifierGenerator{TId}"/>。</returns>
        public static IIdentityStoreIdentifierGenerator<TId> GetIdentityStoreIdentifierGenerator<TId>
            (this ServiceFactory serviceFactory, Type idType)
            where TId : IEquatable<TId>
            => (IIdentityStoreIdentifierGenerator<TId>)serviceFactory.GetRequiredService(_identifierGeneratorTypeDefinition.MakeGenericType(idType));

    }
}
