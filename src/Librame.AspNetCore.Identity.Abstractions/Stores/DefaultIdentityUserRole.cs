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
    /// 默认身份用户角色。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    [Description("默认身份用户角色")]
    public class DefaultIdentityUserRole<TGenId> : IdentityUserRole<TGenId>, ICreation<string, DateTimeOffset>
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
