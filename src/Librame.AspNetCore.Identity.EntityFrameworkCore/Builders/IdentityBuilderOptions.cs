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
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 身份构建器选项。
    /// </summary>
    public class IdentityBuilderOptions : AbstractBuilderOptions, IBuilderOptions
    {
        /// <summary>
        /// 配置核心身份选项。
        /// </summary>
        public Action<IdentityOptions> ConfigureCoreIdentity { get; set; }

        /// <summary>
        /// 配置 UI 模式。
        /// </summary>
        public Action<IIdentityBuilder> ConfigureUIMode { get; set; }


        /// <summary>
        /// 角色表。
        /// </summary>
        public Func<Type, ITableSchema> RoleTableFactory { get; set; }
            = type => type.AsTableSchema(names => names.TrimStart("Default"));

        /// <summary>
        /// 角色声明表。
        /// </summary>
        public Func<Type, ITableSchema> RoleClaimTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 用户角色表。
        /// </summary>
        public Func<Type, ITableSchema> UserRoleTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 用户表。
        /// </summary>
        public Func<Type, ITableSchema> UserTableFactory { get; set; }
            = type => type.AsTableSchema(names => names.TrimStart("Default"));

        /// <summary>
        /// 用户声明表。
        /// </summary>
        public Func<Type, ITableSchema> UserClaimTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 用户登入表。
        /// </summary>
        public Func<Type, ITableSchema> UserLoginTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 用户令牌表。
        /// </summary>
        public Func<Type, ITableSchema> UserTokenTableFactory { get; set; }
            = type => type.AsTableSchema();
    }
}
