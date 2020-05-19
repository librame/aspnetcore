#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions;
    using Extensions.Data.Resources;
    using Extensions.Data.Stores;

    /// <summary>
    /// 默认身份用户角色。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    [Description("默认身份用户角色")]
    public class DefaultIdentityUserRole<TGenId> : IdentityUserRole<TGenId>, ICreation<string, DateTimeOffset>, ICreatedTimeTicks
        where TGenId : IEquatable<TGenId>
    {
        /// <summary>
        /// 构造一个 <see cref="DefaultIdentityUserRole{TGenId}"/>。
        /// </summary>
        public DefaultIdentityUserRole()
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
        public virtual string CreatedBy { get; set; }


        /// <summary>
        /// 异步获取创建者。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含字符串的异步操作。</returns>
        public virtual Task<string> GetCreatedByAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunFactoryOrCancellationAsync(() => CreatedBy);

        Task<object> ICreation.GetCreatedByAsync(CancellationToken cancellationToken)
            => cancellationToken.RunFactoryOrCancellationAsync(() => (object)CreatedBy);

        /// <summary>
        /// 异步获取创建时间。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="DateTimeOffset"/> 的异步操作。</returns>
        public virtual Task<DateTimeOffset> GetCreatedTimeAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunFactoryOrCancellationAsync(() => CreatedTime);

        Task<object> ICreation.GetCreatedTimeAsync(CancellationToken cancellationToken)
            => cancellationToken.RunFactoryOrCancellationAsync(() => (object)CreatedTime);


        /// <summary>
        /// 异步设置创建者。
        /// </summary>
        /// <param name="createdBy">给定的创建者。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="Task"/>。</returns>
        public virtual Task SetCreatedByAsync(string createdBy, CancellationToken cancellationToken = default)
            => cancellationToken.RunFactoryOrCancellationAsync(() => CreatedBy = createdBy);

        /// <summary>
        /// 异步设置创建者。
        /// </summary>
        /// <param name="obj">给定的创建者对象。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="Task"/>。</returns>
        public virtual Task SetCreatedByAsync(object obj, CancellationToken cancellationToken = default)
        {
            var createdBy = obj.CastTo<object, string>(nameof(obj));
            return cancellationToken.RunActionOrCancellationAsync(() => CreatedBy = createdBy);
        }

        /// <summary>
        /// 异步设置创建时间。
        /// </summary>
        /// <param name="createdTime">给定的创建时间。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="Task"/>。</returns>
        public virtual Task SetCreatedTimeAsync(DateTimeOffset createdTime, CancellationToken cancellationToken = default)
            => cancellationToken.RunFactoryOrCancellationAsync(() => CreatedTime = createdTime);

        /// <summary>
        /// 异步设置创建时间。
        /// </summary>
        /// <param name="obj">给定的创建时间对象。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="Task"/>。</returns>
        public virtual Task SetCreatedTimeAsync(object obj, CancellationToken cancellationToken = default)
        {
            var createdTime = obj.CastTo<object, DateTimeOffset>(nameof(obj));
            return cancellationToken.RunActionOrCancellationAsync(() => CreatedTime = createdTime);
        }

    }
}
