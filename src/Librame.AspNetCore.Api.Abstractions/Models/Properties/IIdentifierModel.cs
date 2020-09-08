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

namespace Librame.AspNetCore.Api.Models
{
    using Extensions.Core.Identifiers;

    /// <summary>
    /// 标识符模型接口。
    /// </summary>
    public interface IIdentifierModel
    {
        /// <summary>
        /// 标识。
        /// </summary>
        string Id { get; set; }


        /// <summary>
        /// 填充标识符模型。
        /// </summary>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="identifier">给定的 <see cref="IIdentifier{TId}"/>。</param>
        void Populate<TId>(IIdentifier<TId> identifier)
            where TId : IEquatable<TId>;
    }
}
