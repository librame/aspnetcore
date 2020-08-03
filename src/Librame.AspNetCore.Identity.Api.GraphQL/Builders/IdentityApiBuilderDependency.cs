#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Builders
{
    using AspNetCore.Api.Builders;
    using Extensions.Core.Builders;

    /// <summary>
    /// 身份 API 构建器依赖。
    /// </summary>
    public class IdentityApiBuilderDependency : ApiBuilderDependency
    {
        /// <summary>
        /// 构造一个 <see cref="ApiBuilderDependency"/>。
        /// </summary>
        /// <param name="parentDependency">给定的父级 <see cref="IExtensionBuilderDependency"/>（可选）。</param>
        public IdentityApiBuilderDependency(IExtensionBuilderDependency parentDependency = null)
            : base(parentDependency)
        {
        }


        /// <summary>
        /// 支持确认电邮（默认不启用）。
        /// </summary>
        public virtual bool SupportConfirmEmail { get; set; }

        /// <summary>
        /// 支持确认手机号码（默认不启用）。
        /// </summary>
        public virtual bool SupportConfirmPhoneNumber { get; set; }

        /// <summary>
        /// 支持查询所有角色（默认不启用）。
        /// </summary>
        public virtual bool SupportsQueryRoles { get; set; }

        /// <summary>
        /// 支持查询所有用户（默认不启用）。
        /// </summary>
        public virtual bool SupportsQueryUsers { get; set; }

        /// <summary>
        /// 支持查询用户登入（默认不启用）。
        /// </summary>
        public virtual bool SupportsQueryUserLogins { get; set; }

        /// <summary>
        /// 支持查询用户令牌（默认不启用）。
        /// </summary>
        public virtual bool SupportsQueryUserTokens { get; set; }
    }
}
