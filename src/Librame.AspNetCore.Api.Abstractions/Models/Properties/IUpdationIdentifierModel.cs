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
    /// 更新标识符模型接口。
    /// </summary>
    public interface IUpdationIdentifierModel : ICreationIdentifierModel, IUpdationModel
    {
        /// <summary>
        /// 填充更新标识符模型。
        /// </summary>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <typeparam name="TUpdatedBy">指定的更新者类型。</typeparam>
        /// <typeparam name="TUpdatedTime">指定的更新时间类型。</typeparam>
        /// <param name="updation">给定的 <see cref="IUpdationIdentifier{TId, TUpdatedBy, TUpdatedTime}"/>。</param>
        void Populate<TId, TUpdatedBy, TUpdatedTime>
            (IUpdationIdentifier<TId, TUpdatedBy, TUpdatedTime> updation)
            where TId : IEquatable<TId>
            where TUpdatedBy : IEquatable<TUpdatedBy>
            where TUpdatedTime : struct;
    }
}
