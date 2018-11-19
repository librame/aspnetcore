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
        /// 用户表选项。
        /// </summary>
        public ITableOptions RoleTable { get; set; }

        /// <summary>
        /// 用户声明表选项。
        /// </summary>
        public ITableOptions RoleClaimTable { get; set; }

        /// <summary>
        /// 用户角色表选项。
        /// </summary>
        public ITableOptions UserRoleTable { get; set; }

        /// <summary>
        /// 用户表选项。
        /// </summary>
        public ITableOptions UserTable { get; set; }

        /// <summary>
        /// 用户声明表选项。
        /// </summary>
        public ITableOptions UserClaimTable { get; set; }

        /// <summary>
        /// 用户登陆表选项。
        /// </summary>
        public ITableOptions UserLoginTable { get; set; }

        /// <summary>
        /// 用户令牌表选项。
        /// </summary>
        public ITableOptions UserTokenTable { get; set; }
    }
}
