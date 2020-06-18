#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.AspNetCore.Identity.Options
{
    using Extensions.Data;
    using Extensions.Data.Options;

    /// <summary>
    /// 身份表名选项。
    /// </summary>
    public class IdentityTableOptions : AbstractTableOptions
    {
        /// <summary>
        /// 使用 Identity 前缀（默认使用）。
        /// </summary>
        public bool UseIdentityPrefix { get; set; }
            = true;


        /// <summary>
        /// 角色表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> Role { get; set; }

        /// <summary>
        /// 角色声明表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> RoleClaim { get; set; }

        /// <summary>
        /// 用户角色表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> UserRole { get; set; }

        /// <summary>
        /// 用户表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> User { get; set; }

        /// <summary>
        /// 用户声明表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> UserClaim { get; set; }

        /// <summary>
        /// 用户登入表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> UserLogin { get; set; }

        /// <summary>
        /// 用户令牌表描述符配置动作。
        /// </summary>
        public Action<TableDescriptor> UserToken { get; set; }
    }
}
