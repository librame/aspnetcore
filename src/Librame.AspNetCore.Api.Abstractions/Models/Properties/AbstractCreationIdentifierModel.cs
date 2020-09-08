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
    using Extensions.Data.Stores;

    /// <summary>
    /// 抽象创建标识符模型。
    /// </summary>
    public abstract class AbstractCreationIdentifierModel
        : AbstractCreationModel, ICreationIdentifierModel
    {
        /// <summary>
        /// 标识。
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// 填充创建标识符模型。
        /// </summary>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <typeparam name="TCreatedTime">指定的创建时间类型。</typeparam>
        /// <param name="creation">给定的 <see cref="ICreationIdentifier{TId, TCreatedBy, TCreatedTime}"/>。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public virtual void Populate<TId, TCreatedBy, TCreatedTime>
            (ICreationIdentifier<TId, TCreatedBy, TCreatedTime> creation)
            where TId : IEquatable<TId>
            where TCreatedBy : IEquatable<TCreatedBy>
            where TCreatedTime : struct
        {
            base.Populate(creation);

            Id = creation.Id.ToString();
        }

        /// <summary>
        /// 填充标识符模型。
        /// </summary>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="identifier">给定的 <see cref="IIdentifier{TId}"/>。</param>
        void IIdentifierModel.Populate<TId>(IIdentifier<TId> identifier)
        {
            identifier.NotNull(nameof(identifier));

            Id = identifier.Id.ToString();
        }

    }
}
