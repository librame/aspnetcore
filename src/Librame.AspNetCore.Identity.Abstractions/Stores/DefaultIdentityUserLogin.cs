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
    ///// 默认身份用户登入。
    ///// </summary>
    //[Description("默认身份用户登入")]
    //public class DefaultIdentityUserLogin : DefaultIdentityUserLogin<string>
    //{
    //}


    /// <summary>
    /// 默认身份用户登入。
    /// </summary>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    [Description("默认身份用户登入")]
    public class DefaultIdentityUserLogin<TUserId> : IdentityUserLogin<TUserId>, ICreation<TUserId, DateTimeOffset>
        where TUserId : IEquatable<TUserId>
    {
        /// <summary>
        /// 构造一个 <see cref="DefaultIdentityUserLogin{TUserId}"/>。
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
        /// 创建者。
        /// </summary>
        [Display(Name = nameof(CreatedBy), ResourceType = typeof(AbstractEntityResource))]
        public virtual TUserId CreatedBy { get; set; }


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
    }
}
