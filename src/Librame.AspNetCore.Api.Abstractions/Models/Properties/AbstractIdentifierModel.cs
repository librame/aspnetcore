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

namespace Librame.AspNetCore.Api.Models
{
    using Extensions;
    using Extensions.Core.Identifiers;

    /// <summary>
    /// 抽象标识符模型。
    /// </summary>
    public abstract class AbstractIdentifierModel : IIdentifierModel
    {
        /// <summary>
        /// 标识。
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// 填充标识符模型。
        /// </summary>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="identifier">给定的 <see cref="IIdentifier{TId}"/>。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public virtual void Populate<TId>(IIdentifier<TId> identifier)
            where TId : IEquatable<TId>
        {
            identifier.NotNull(nameof(identifier));
            
            Id = identifier.Id.ToString();
        }

    }
}
