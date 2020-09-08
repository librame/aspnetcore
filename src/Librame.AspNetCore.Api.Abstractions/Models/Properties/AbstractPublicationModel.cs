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
    using Extensions.Data.Stores;

    /// <summary>
    /// 抽象发表模型。
    /// </summary>
    public abstract class AbstractPublicationModel
        : AbstractCreationModel, IPublicationModel
    {
        /// <summary>
        /// 发表时间。
        /// </summary>
        public string PublishedTime { get; set; }

        /// <summary>
        /// 发表者。
        /// </summary>
        public string PublishedBy { get; set; }

        /// <summary>
        /// 发表为（如：资源链接）。
        /// </summary>
        public string PublishedAs { get; set; }


        /// <summary>
        /// 填充发表模型。
        /// </summary>
        /// <typeparam name="TPublishedBy">指定的发表者类型。</typeparam>
        /// <typeparam name="TPublishedTime">指定的发表时间类型。</typeparam>
        /// <param name="publication">给定的 <see cref="IPublicationIdentifier{TPublishedBy, TPublishedTime}"/>。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public virtual void Populate<TPublishedBy, TPublishedTime>
            (IPublication<TPublishedBy, TPublishedTime> publication)
            where TPublishedBy : IEquatable<TPublishedBy>
            where TPublishedTime : struct
        {
            base.Populate(publication);

            PublishedTime = publication.PublishedTime.ToString();
            PublishedBy = publication.PublishedBy.ToString();
            PublishedAs = publication.PublishedAs;
        }

    }
}
