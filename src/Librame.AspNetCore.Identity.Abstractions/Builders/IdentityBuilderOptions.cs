#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity
{
    using Extensions.Data;

    /// <summary>
    /// 身份构建器选项。
    /// </summary>
    public class IdentityBuilderOptions : DataBuilderOptions
    {
        /// <summary>
        /// 用户表架构。
        /// </summary>
        public ITableSchema RoleTable { get; set; }

        /// <summary>
        /// 用户声明表架构。
        /// </summary>
        public ITableSchema RoleClaimTable { get; set; }

        /// <summary>
        /// 用户角色表架构。
        /// </summary>
        public ITableSchema UserRoleTable { get; set; }

        /// <summary>
        /// 用户表架构。
        /// </summary>
        public ITableSchema UserTable { get; set; }

        /// <summary>
        /// 用户声明表架构。
        /// </summary>
        public ITableSchema UserClaimTable { get; set; }

        /// <summary>
        /// 用户登陆表架构。
        /// </summary>
        public ITableSchema UserLoginTable { get; set; }

        /// <summary>
        /// 用户令牌表架构。
        /// </summary>
        public ITableSchema UserTokenTable { get; set; }
    }
}
