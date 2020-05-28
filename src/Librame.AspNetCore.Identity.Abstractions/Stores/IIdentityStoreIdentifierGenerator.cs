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
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions.Data.Stores;

    /// <summary>
    /// 身份存储标识符生成器接口。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    public interface IIdentityStoreIdentifierGenerator<TGenId> : IIdentityStoreIdentifierGenerator, IStoreIdentifierGenerator<TGenId>
        where TGenId : IEquatable<TGenId>
    {
        /// <summary>
        /// 异步生成角色标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TGenId"/> 的异步操作。</returns>
        new Task<TGenId> GenerateRoleIdAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步生成用户标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TGenId"/> 的异步操作。</returns>
        new Task<TGenId> GenerateUserIdAsync(CancellationToken cancellationToken = default);
    }


    /// <summary>
    /// 身份存储标识符生成器接口。
    /// </summary>
    public interface IIdentityStoreIdentifierGenerator : IStoreIdentifierGenerator
    {
        /// <summary>
        /// 异步获取角色标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含标识对象的异步操作。</returns>
        Task<object> GenerateRoleIdAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取用户标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含标识对象的异步操作。</returns>
        Task<object> GenerateUserIdAsync(CancellationToken cancellationToken = default);
    }
}
