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
    using Extensions.Core.Identifiers;
    using Extensions.Data.Resources;
    using Extensions.Data.Stores;

    /// <summary>
    /// 默认身份用户。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    [Description("默认身份用户")]
    public class DefaultIdentityUser<TGenId, TCreatedBy> : IdentityUser<TGenId>,
        IIdentifier<TGenId>, ICreation<TCreatedBy>,
        IEquatable<DefaultIdentityUser<TGenId, TCreatedBy>>
        where TGenId : IEquatable<TGenId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个默认身份用户。
        /// </summary>
        public DefaultIdentityUser()
            : this(null)
        {
        }

        /// <summary>
        /// 构造一个默认身份用户。
        /// </summary>
        /// <param name="userName">给定的用户名称。</param>
        public DefaultIdentityUser(string userName)
            : base(userName)
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
        /// 标识类型。
        /// </summary>
        [NotMapped]
        public Type IdType
            => typeof(TGenId);

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
        /// 获取对象标识。
        /// </summary>
        /// <returns>返回标识（兼容各种引用与值类型标识）。</returns>
        public virtual object GetObjectId()
            => Id;

        /// <summary>
        /// 异步获取对象标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含标识（兼容各种引用与值类型标识）的异步操作。</returns>
        public virtual ValueTask<object> GetObjectIdAsync(CancellationToken cancellationToken)
            => cancellationToken.RunOrCancelValueAsync(() => (object)Id);


        /// <summary>
        /// 获取对象创建时间。
        /// </summary>
        /// <returns>返回日期与时间（兼容 <see cref="DateTime"/> 或 <see cref="DateTimeOffset"/>）。</returns>
        public virtual object GetObjectCreatedTime()
            => CreatedTime;

        /// <summary>
        /// 异步获取创建时间。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含日期与时间（兼容 <see cref="DateTime"/> 或 <see cref="DateTimeOffset"/>）的异步操作。</returns>
        public virtual ValueTask<object> GetObjectCreatedTimeAsync(CancellationToken cancellationToken)
            => cancellationToken.RunOrCancelValueAsync(() => (object)CreatedTime);


        /// <summary>
        /// 获取对象创建者。
        /// </summary>
        /// <returns>返回创建者（兼容标识或字符串）。</returns>
        public virtual object GetObjectCreatedBy()
            => CreatedBy;

        /// <summary>
        /// 异步获取创建者。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含创建者（兼容标识或字符串）的异步操作。</returns>
        public virtual ValueTask<object> GetObjectCreatedByAsync(CancellationToken cancellationToken)
            => cancellationToken.RunOrCancelValueAsync(() => (object)CreatedBy);


        /// <summary>
        /// 异步设置对象标识。
        /// </summary>
        /// <param name="newId">给定的新对象标识。</param>
        /// <returns>返回标识（兼容各种引用与值类型标识）。</returns>
        public virtual object SetObjectId(object newId)
        {
            Id = newId.CastTo<object, TGenId>(nameof(newId));
            return newId;
        }

        /// <summary>
        /// 异步设置对象标识。
        /// </summary>
        /// <param name="newId">给定的新对象标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含标识（兼容各种引用与值类型标识）的异步操作。</returns>
        public virtual ValueTask<object> SetObjectIdAsync(object newId, CancellationToken cancellationToken = default)
        {
            var realNewId = newId.CastTo<object, TGenId>(nameof(newId));

            return cancellationToken.RunOrCancelValueAsync(() =>
            {
                Id = realNewId;
                return newId;
            });
        }


        /// <summary>
        /// 设置对象创建时间。
        /// </summary>
        /// <param name="newCreatedTime">给定的新创建时间对象。</param>
        /// <returns>返回日期与时间（兼容 <see cref="DateTime"/> 或 <see cref="DateTimeOffset"/>）。</returns>
        public virtual object SetObjectCreatedTime(object newCreatedTime)
        {
            CreatedTime = newCreatedTime.CastTo<object, DateTimeOffset>(nameof(newCreatedTime));
            return newCreatedTime;
        }

        /// <summary>
        /// 异步设置对象创建时间。
        /// </summary>
        /// <param name="newCreatedTime">给定的新创建时间对象。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含日期与时间（兼容 <see cref="DateTime"/> 或 <see cref="DateTimeOffset"/>）的异步操作。</returns>
        public virtual ValueTask<object> SetObjectCreatedTimeAsync(object newCreatedTime,
            CancellationToken cancellationToken = default)
        {
            var realNewCreatedTime = newCreatedTime.CastTo<object, DateTimeOffset>(nameof(newCreatedTime));

            return cancellationToken.RunOrCancelValueAsync(() =>
            {
                CreatedTime = realNewCreatedTime;
                return newCreatedTime;
            });
        }


        /// <summary>
        /// 设置对象创建者。
        /// </summary>
        /// <param name="newCreatedBy">给定的新创建者对象。</param>
        /// <returns>返回创建者（兼容标识或字符串）。</returns>
        public virtual object SetObjectCreatedBy(object newCreatedBy)
        {
            CreatedBy = newCreatedBy.CastTo<object, TCreatedBy>(nameof(newCreatedBy));
            return newCreatedBy;
        }

        /// <summary>
        /// 异步设置对象创建者。
        /// </summary>
        /// <param name="newCreatedBy">给定的新创建者对象。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含创建者（兼容标识或字符串）的异步操作。</returns>
        public virtual ValueTask<object> SetObjectCreatedByAsync(object newCreatedBy,
            CancellationToken cancellationToken = default)
        {
            var realNewCreatedBy = newCreatedBy.CastTo<object, TCreatedBy>(nameof(newCreatedBy));

            return cancellationToken.RunOrCancelValueAsync(() =>
            {
                CreatedBy = realNewCreatedBy;
                return newCreatedBy;
            });
        }


        /// <summary>
        /// 除主键外的唯一索引相等比较（参见实体映射的唯一索引配置）。
        /// </summary>
        /// <param name="other">给定的 <see cref="DefaultIdentityUser{TGenId, TCreatedBy}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(DefaultIdentityUser<TGenId, TCreatedBy> other)
            => NormalizedUserName == other?.NormalizedUserName;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is DefaultIdentityUser<TGenId, TCreatedBy> other) ? Equals(other) : false;


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回 32 位整数。</returns>
        public override int GetHashCode()
            => ToString().GetHashCode(StringComparison.OrdinalIgnoreCase);


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => NormalizedUserName;

    }
}
