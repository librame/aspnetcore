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
    using Extensions.Data.Stores;

    /// <summary>
    /// 创建标识符模型接口。
    /// </summary>
    public interface ICreationIdentifierModel : IIdentifierModel, ICreationModel
    {
        /// <summary>
        /// 填充创建标识符模型。
        /// </summary>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <typeparam name="TCreatedTime">指定的创建时间类型。</typeparam>
        /// <param name="creation">给定的 <see cref="ICreationIdentifier{TId, TCreatedBy, TCreatedTime}"/>。</param>
        void Populate<TId, TCreatedBy, TCreatedTime>
            (ICreationIdentifier<TId, TCreatedBy, TCreatedTime> creation)
            where TId : IEquatable<TId>
            where TCreatedBy : IEquatable<TCreatedBy>
            where TCreatedTime : struct;
    }
}
