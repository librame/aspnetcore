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

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions.Data.Resources;
    using Extensions.Data.Stores;

    /// <summary>
    /// 默认身份用户。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    [Description("默认身份用户")]
    public class DefaultIdentityUser<TGenId> : IdentityUser<TGenId>, IId<TGenId>, ICreation<string, DateTimeOffset>, IEquatable<DefaultIdentityUser<TGenId>>
        where TGenId : IEquatable<TGenId>
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
        /// 创建者。
        /// </summary>
        [Display(Name = nameof(CreatedBy), ResourceType = typeof(AbstractEntityResource))]
        public virtual string CreatedBy { get; set; }


        /// <summary>
        /// 获取创建时间。
        /// </summary>
        /// <returns>返回日期与时间。</returns>
        public virtual object GetCustomCreatedTime()
            => CreatedTime;

        /// <summary>
        /// 获取创建者。
        /// </summary>
        /// <returns>返回创建者。</returns>
        public virtual object GetCustomCreatedBy()
            => CreatedBy;


        /// <summary>
        /// 唯一索引是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="DefaultIdentityUser{TGenId}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(DefaultIdentityUser<TGenId> other)
            => NormalizedUserName == other?.NormalizedUserName;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is DefaultIdentityUser<TGenId> other) ? Equals(other) : false;


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
