#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
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
    /// <see cref="IIdentityStoreIdentifierGenerator"/> 静态扩展。
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
        /// <param name="serviceProvider">给定的 <see cref="IServiceProvider"/>。</param>
        /// <returns>返回 <see cref="IIdentityStoreIdentifierGenerator"/>。</returns>
        public static IIdentityStoreIdentifierGenerator GetIdentityStoreIdentifierGeneratorByUser<TUser>(this IServiceProvider serviceProvider)
            => serviceProvider.GetIdentityStoreIdentifierGeneratorByUser(typeof(TUser));

        /// <summary>
        /// 获取身份存储标识符生成器。
        /// </summary>
        /// <param name="serviceProvider">给定的 <see cref="IServiceProvider"/>。</param>
        /// <param name="userType">给定实现 <see cref="IIdentifier{TId}"/> 的用户类型。</param>
        /// <returns>返回 <see cref="IIdentityStoreIdentifierGenerator"/>。</returns>
        public static IIdentityStoreIdentifierGenerator GetIdentityStoreIdentifierGeneratorByUser(this IServiceProvider serviceProvider, Type userType)
        {
            if (!userType.IsImplementedInterface(_identifierTypeDefinition, out var resultType))
                throw new InvalidOperationException($"The user type '{userType}' is not implemented {_identifierTypeDefinition}.");

            return serviceProvider.GetIdentityStoreIdentifierGenerator(resultType.GenericTypeArguments[0]);
        }

        /// <summary>
        /// 获取身份存储标识符生成器。
        /// </summary>
        /// <param name="serviceProvider">给定的 <see cref="IServiceProvider"/>。</param>
        /// <param name="idType">给定的生成式标识符类型。</param>
        /// <returns>返回 <see cref="IIdentityStoreIdentifierGenerator"/>。</returns>
        public static IIdentityStoreIdentifierGenerator GetIdentityStoreIdentifierGenerator(this IServiceProvider serviceProvider, Type idType)
            => (IIdentityStoreIdentifierGenerator)serviceProvider.GetRequiredService(_identifierGeneratorTypeDefinition.MakeGenericType(idType));


        /// <summary>
        /// 获取身份存储标识符生成器。
        /// </summary>
        /// <typeparam name="TUser">指定实现 <see cref="IIdentifier{TId}"/> 的用户类型。</typeparam>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactory"/>。</param>
        /// <returns>返回 <see cref="IIdentityStoreIdentifierGenerator"/>。</returns>
        public static IIdentityStoreIdentifierGenerator GetIdentityStoreIdentifierGeneratorByUser<TUser>(this ServiceFactory serviceFactory)
            => serviceFactory.GetIdentityStoreIdentifierGeneratorByUser(typeof(TUser));

        /// <summary>
        /// 获取身份存储标识符生成器。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactory"/>。</param>
        /// <param name="userType">给定实现 <see cref="IIdentifier{TId}"/> 的用户类型。</param>
        /// <returns>返回 <see cref="IIdentityStoreIdentifierGenerator"/>。</returns>
        public static IIdentityStoreIdentifierGenerator GetIdentityStoreIdentifierGeneratorByUser(this ServiceFactory serviceFactory, Type userType)
        {
            if (!userType.IsImplementedInterface(_identifierTypeDefinition, out var resultType))
                throw new InvalidOperationException($"The user type '{userType}' is not implemented {_identifierTypeDefinition}.");

            return serviceFactory.GetIdentityStoreIdentifierGenerator(resultType.GenericTypeArguments[0]);
        }

        /// <summary>
        /// 获取身份存储标识符生成器。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactory"/>。</param>
        /// <param name="idType">给定的生成式标识符类型。</param>
        /// <returns>返回 <see cref="IIdentityStoreIdentifierGenerator"/>。</returns>
        public static IIdentityStoreIdentifierGenerator GetIdentityStoreIdentifierGenerator(this ServiceFactory serviceFactory, Type idType)
            => (IIdentityStoreIdentifierGenerator)serviceFactory.GetRequiredService(_identifierGeneratorTypeDefinition.MakeGenericType(idType));

    }
}
