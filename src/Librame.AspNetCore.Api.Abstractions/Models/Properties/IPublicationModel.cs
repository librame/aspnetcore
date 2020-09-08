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
    /// 发表模型接口。
    /// </summary>
    public interface IPublicationModel : ICreationModel
    {
        /// <summary>
        /// 发表时间。
        /// </summary>
        string PublishedTime { get; set; }

        /// <summary>
        /// 发表者。
        /// </summary>
        string PublishedBy { get; set; }

        /// <summary>
        /// 发表为（如：资源链接）。
        /// </summary>
        string PublishedAs { get; set; }


        /// <summary>
        /// 填充发表模型。
        /// </summary>
        /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
        /// <typeparam name="TPublishedTime">指定的发表时间类型。</typeparam>
        /// <param name="publication">给定的 <see cref="IPublication{TPublishedBy, TPublishedTime}"/>。</param>
        void Populate<TPublishedBy, TPublishedTime>
            (IPublication<TPublishedBy, TPublishedTime> publication)
            where TPublishedBy : IEquatable<TPublishedBy>
            where TPublishedTime : struct;
    }
}
