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

namespace Librame.AspNetCore.Identity
{
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// 身份构建器选项。
    /// </summary>
    public class IdentityBuilderOptions : DataBuilderOptionsBase<IdentityTableSchemaOptions>
    {
        /// <summary>
        /// 配置核心身份选项。
        /// </summary>
        public Action<IdentityOptions> ConfigureCoreIdentity { get; set; }

        /// <summary>
        /// 配置 UI 模式。
        /// </summary>
        public Action<IIdentityBuilder> ConfigureUIMode { get; set; }
    }


    /// <summary>
    /// 身份表架构选项。
    /// </summary>
    public class IdentityTableSchemaOptions : ITableSchemaOptions
    {
        /// <summary>
        /// 角色工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> RoleFactory { get; set; }
            = type => type.AsTableSchema(names => names.TrimStart("Default"));

        /// <summary>
        /// 角色声明工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> RoleClaimFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 用户角色工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> UserRoleFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 用户工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> UserFactory { get; set; }
            = type => type.AsTableSchema(names => names.TrimStart("Default"));

        /// <summary>
        /// 用户声明工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> UserClaimFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 用户登入工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> UserLoginFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 用户令牌工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> UserTokenFactory { get; set; }
            = type => type.AsTableSchema();
    }
}
