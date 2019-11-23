#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.AspNetCore.Identity
{
    using Extensions.Data;

    /// <summary>
    /// 身份构建器选项。
    /// </summary>
    public class IdentityBuilderOptions : DataBuilderOptionsBase<IdentityTableNameSchemaOptions>
    {
        /// <summary>
        /// 启用密码规则提示（默认启用）。
        /// </summary>
        public bool PasswordRulePromptEnabled { get; set; }
            = true;

        /// <summary>
        /// 认证器 URI 工厂方法。
        /// </summary>
        public Func<IdentityAuthenticatorDescriptor, string> AuthenticatorUriFactory { get; set; }
            = descr => descr.BuildOtpAuthUriString();
    }


    /// <summary>
    /// 身份表名架构选项。
    /// </summary>
    public class IdentityTableNameSchemaOptions : TableNameSchemaOptions
    {
        /// <summary>
        /// 角色工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> RoleFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 角色声明工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> RoleClaimFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 用户角色工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> UserRoleFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 用户工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> UserFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 用户声明工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> UserClaimFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 用户登入工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> UserLoginFactory { get; set; }
            = type => type.AsSchema();

        /// <summary>
        /// 用户令牌工厂方法。
        /// </summary>
        public Func<TableNameDescriptor, TableNameSchema> UserTokenFactory { get; set; }
            = type => type.AsSchema();
    }
}
