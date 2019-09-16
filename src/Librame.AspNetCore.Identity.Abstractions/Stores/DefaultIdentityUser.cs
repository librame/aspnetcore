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

namespace Librame.AspNetCore.Identity
{
    using Extensions.Data;

    ///// <summary>
    ///// 默认身份用户。
    ///// </summary>
    //[Description("默认身份用户")]
    //public class DefaultIdentityUser : DefaultIdentityUser<string>
    //{
    //    /// <summary>
    //    /// 构造一个 <see cref="DefaultIdentityUser"/>。
    //    /// </summary>
    //    public DefaultIdentityUser()
    //        : this(null)
    //    {
    //    }

    //    /// <summary>
    //    /// 构造一个 <see cref="DefaultIdentityUser"/>。
    //    /// </summary>
    //    /// <param name="userName">给定的用户名称。</param>
    //    public DefaultIdentityUser(string userName)
    //        : base(userName)
    //    {
    //        Id = EntityUtility.EmptyCombGuid;
    //    }
    //}


    /// <summary>
    /// 默认身份用户接口。
    /// </summary>
    public interface IDefaultIdentityUser
    {
    }


    /// <summary>
    /// 默认身份用户。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    [Description("默认身份用户")]
    public class DefaultIdentityUser<TId> : IdentityUser<TId>, IId<TId>, ICreation<TId, DateTimeOffset>, IDefaultIdentityUser
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 构造一个 <see cref="DefaultIdentityUser{TId}"/>。
        /// </summary>
        public DefaultIdentityUser()
            : this(null)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="DefaultIdentityUser{TId}"/>。
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
        public virtual TId CreatedBy { get; set; }


        /// <summary>
        /// 获取创建时间。
        /// </summary>
        /// <returns>返回日期与时间。</returns>
        public virtual object GetCreatedTime()
            => CreatedTime;

        /// <summary>
        /// 获取创建者。
        /// </summary>
        /// <returns>返回创建者。</returns>
        public virtual object GetCreatedBy()
            => CreatedBy;


        /// <summary>
        /// 唯一索引是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="DefaultIdentityUser{TId}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(DefaultIdentityUser<TId> other)
            => NormalizedUserName == other?.NormalizedUserName;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is DefaultIdentityUser<TId> other) ? Equals(other) : false;


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回 32 位整数。</returns>
        public override int GetHashCode()
            => ToString().GetHashCode();


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => NormalizedUserName;
    }
}
