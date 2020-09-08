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
    /// 发表标识符模型接口。
    /// </summary>
    public interface IPublicationIdentifierModel : ICreationIdentifierModel, IPublicationModel
    {
        /// <summary>
        /// 填充发表标识符模型。
        /// </summary>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
        /// <typeparam name="TPublishedTime">指定的发表时间类型。</typeparam>
        /// <param name="publication">给定的 <see cref="IPublicationIdentifier{TId, TPublishedBy, TPublishedTime}"/>。</param>
        void Populate<TId, TPublishedBy, TPublishedTime>
            (IPublicationIdentifier<TId, TPublishedBy, TPublishedTime> publication)
            where TId : IEquatable<TId>
            where TPublishedBy : IEquatable<TPublishedBy>
            where TPublishedTime : struct;
    }
}
