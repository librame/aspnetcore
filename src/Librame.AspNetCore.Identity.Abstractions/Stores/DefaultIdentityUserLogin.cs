#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions;
    using Extensions.Data.Resources;
    using Extensions.Data.Stores;

    /// <summary>
    /// 默认身份用户登入。
    /// </summary>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    [Description("默认身份用户登入")]
    public class DefaultIdentityUserLogin<TUserId, TCreatedBy> : IdentityUserLogin<TUserId>,
        ICreation<TCreatedBy>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个默认身份用户登入。
        /// </summary>
        public DefaultIdentityUserLogin()
            : base()
        {
        }


        /// <summary>
        /// 创建时间。
        /// </summary>
        [Display(Name = nameof(CreatedTime), ResourceType = typeof(AbstractEntityResource))]
        public virtual DateTimeOffset CreatedTime { get; set; }

        /// <summary>
        /// 创建时间周期数。
        /// </summary>
        [Display(Name = nameof(CreatedTimeTicks), ResourceType = typeof(AbstractEntityResource))]
        public virtual long CreatedTimeTicks { get; set; }

        /// <summary>
        /// 创建者。
        /// </summary>
        [Display(Name = nameof(CreatedBy), ResourceType = typeof(AbstractEntityResource))]
        public virtual TCreatedBy CreatedBy { get; set; }


        /// <summary>
        /// 获取创建时间类型。
        /// </summary>
        [NotMapped]
        public Type CreatedTimeType
            => typeof(DateTimeOffset);

        /// <summary>
        /// 获取创建者类型。
        /// </summary>
        [NotMapped]
        public Type CreatedByType
            => typeof(TCreatedBy);


        /// <summary>
        /// 异步获取创建时间。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含日期与时间（兼容 <see cref="DateTime"/> 或 <see cref="DateTimeOffset"/>）的异步操作。</returns>
        public virtual ValueTask<object> GetObjectCreatedTimeAsync(CancellationToken cancellationToken)
            => cancellationToken.RunFactoryOrCancellationValueAsync(() => (object)CreatedTime);

        /// <summary>
        /// 异步获取创建者。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含创建者（兼容标识或字符串）的异步操作。</returns>
        public virtual ValueTask<object> GetObjectCreatedByAsync(CancellationToken cancellationToken)
            => cancellationToken.RunFactoryOrCancellationValueAsync(() => (object)CreatedBy);


        /// <summary>
        /// 异步设置创建时间。
        /// </summary>
        /// <param name="newCreatedTime">给定的新创建时间对象。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含日期与时间（兼容 <see cref="DateTime"/> 或 <see cref="DateTimeOffset"/>）的异步操作。</returns>
        public virtual ValueTask<object> SetObjectCreatedTimeAsync(object newCreatedTime,
            CancellationToken cancellationToken = default)
        {
            var realNewCreatedTime = newCreatedTime.CastTo<object, DateTimeOffset>(nameof(newCreatedTime));

            return cancellationToken.RunFactoryOrCancellationValueAsync(() =>
            {
                CreatedTime = realNewCreatedTime;
                return newCreatedTime;
            });
        }

        /// <summary>
        /// 异步设置创建者。
        /// </summary>
        /// <param name="newCreatedBy">给定的新创建者对象。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含创建者（兼容标识或字符串）的异步操作。</returns>
        public virtual ValueTask<object> SetObjectCreatedByAsync(object newCreatedBy,
            CancellationToken cancellationToken = default)
        {
            var realNewCreatedBy = newCreatedBy.CastTo<object, TCreatedBy>(nameof(newCreatedBy));

            return cancellationToken.RunFactoryOrCancellationValueAsync(() =>
            {
                CreatedBy = realNewCreatedBy;
                return newCreatedBy;
            });
        }

    }
}
