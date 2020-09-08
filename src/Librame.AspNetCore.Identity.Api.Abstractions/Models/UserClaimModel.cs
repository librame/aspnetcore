#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Security.Claims;

namespace Librame.AspNetCore.Identity.Api.Models
{
    using AspNetCore.Api.Models;

    /// <summary>
    /// 用户声明模型。
    /// </summary>
    public class UserClaimModel : AbstractCreationIdentifierModel
    {
        /// <summary>
        /// 声明类型。
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// 声明值。
        /// </summary>
        public string ClaimValue { get; set; }


        /// <summary>
        /// 用户模型。
        /// </summary>
        public UserModel User { get; set; }


        /// <summary>
        /// 转为声明。
        /// </summary>
        /// <returns>返回 <see cref="Claim"/>。</returns>
        public virtual Claim ToClaim()
            => new Claim(ClaimType, ClaimValue);

    }
}
