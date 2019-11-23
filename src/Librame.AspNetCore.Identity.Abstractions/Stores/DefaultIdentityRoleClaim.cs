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

    /// <summary>
    /// 默认身份角色声明。
    /// </summary>
    /// <typeparam name="TRoleId">指定的角色标识类型。</typeparam>
    [Description("默认身份角色声明")]
    public class DefaultIdentityRoleClaim<TRoleId> : IdentityRoleClaim<TRoleId>, IId<int>, ICreation<string, DateTimeOffset>
        where TRoleId : IEquatable<TRoleId>
    {
        /// <summary>
        /// 构造一个 <see cref="DefaultIdentityRoleClaim{TRoleId}"/>。
        /// </summary>
        public DefaultIdentityRoleClaim()
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
    }
}
