#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Runtime.InteropServices;

namespace LibrameStandard.Authentication
{
    using Handlers;
    using Managers;
    using Models;
    using Senders;
    using Utilities;

    /// <summary>
    /// 认证设置。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AuthenticationSettings : ILibrameSettings
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix = (nameof(Authentication) + ":");

        
        /// <summary>
        /// 处理程序集合。
        /// </summary>
        public AuthenticationHandlersSetting Handlers { get; set; }
            = new AuthenticationHandlersSetting();

        
        /// <summary>
        /// 管理器集合。
        /// </summary>
        public AuthenticationManagersSetting Managers { get; set; }
            = new AuthenticationManagersSetting();


        /// <summary>
        /// 模型集合。
        /// </summary>
        public AuthenticationModelsSetting Models { get; set; }
            = new AuthenticationModelsSetting();


        /// <summary>
        /// 发送器集合。
        /// </summary>
        public AuthenticationSendersSetting Senders { get; set; }
            = new AuthenticationSendersSetting();
    }


    /// <summary>
    /// 认证处理程序设置。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AuthenticationHandlersSetting
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix
            = (AuthenticationSettings.KeyPrefix + nameof(AuthenticationSettings.Handlers) + ":");


        #region TokenHandlerTypeName

        /// <summary>
        /// 令牌处理程序类型名键。
        /// </summary>
        public static readonly string TokenHandlerTypeNameKey
            = (KeyPrefix + nameof(TokenHandlerTypeName));

        /// <summary>
        /// 默认令牌处理程序类型名。
        /// </summary>
        public static readonly string DefaultTokenHandlerTypeName
            = typeof(TokenHandler).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 令牌处理程序类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultTokenHandlerTypeName"/>。
        /// </value>
        public string TokenHandlerTypeName { get; set; } = DefaultTokenHandlerTypeName;

        #endregion

    }


    /// <summary>
    /// 认证管理器设置。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AuthenticationManagersSetting
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix
            = (AuthenticationSettings.KeyPrefix + nameof(AuthenticationSettings.Managers) + ":");


        #region PasswordManagerTypeName

        /// <summary>
        /// 密码管理器类型名键。
        /// </summary>
        public static readonly string PasswordManagerTypeNameKey
            = (KeyPrefix + nameof(PasswordManagerTypeName));

        /// <summary>
        /// 默认密码管理器类型名。
        /// </summary>
        public static readonly string DefaultPasswordManagerTypeName
            = typeof(PasswordManager).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 密码管理器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultPasswordManagerTypeName"/>。
        /// </value>
        public string PasswordManagerTypeName { get; set; } = DefaultPasswordManagerTypeName;

        #endregion


        #region RoleManagerTypeName

        /// <summary>
        /// 角色管理器类型名键。
        /// </summary>
        public static readonly string RoleManagerTypeNameKey
            = (KeyPrefix + nameof(RoleManagerTypeName));

        /// <summary>
        /// 默认角色管理器类型名。
        /// </summary>
        public static readonly string DefaultRoleManagerTypeName
            = typeof(RoleManager).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 角色管理器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultRoleManagerTypeName"/>。
        /// </value>
        public string RoleManagerTypeName { get; set; } = DefaultRoleManagerTypeName;

        #endregion


        #region TokenManagerTypeName

        /// <summary>
        /// 令牌管理器类型名键。
        /// </summary>
        public static readonly string TokenManagerTypeNameKey
            = (KeyPrefix + nameof(TokenManagerTypeName));

        /// <summary>
        /// 默认令牌管理器类型名。
        /// </summary>
        public static readonly string DefaultTokenManagerTypeName
            = typeof(TokenManager).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 令牌管理器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultTokenManagerTypeName"/>。
        /// </value>
        public string TokenManagerTypeName { get; set; } = DefaultTokenManagerTypeName;

        #endregion


        #region UserManager

        /// <summary>
        /// 用户管理器类型名键。
        /// </summary>
        public static readonly string UserManagerTypeNameKey
            = (KeyPrefix + nameof(UserManagerTypeName));

        /// <summary>
        /// 默认用户管理器类型名。
        /// </summary>
        public static readonly string DefaultUserManagerTypeName
            = typeof(UserManager).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 用户管理器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultUserManagerTypeName"/>。
        /// </value>
        public string UserManagerTypeName { get; set; } = DefaultUserManagerTypeName;

        #endregion

    }


    /// <summary>
    /// 认证模型设置。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AuthenticationModelsSetting
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix
            = (AuthenticationSettings.KeyPrefix + nameof(AuthenticationSettings.Models) + ":");


        #region RoleModelTypeName

        /// <summary>
        /// 角色模型类型名键。
        /// </summary>
        public static readonly string RoleModelTypeNameKey
            = (KeyPrefix + nameof(RoleModelTypeName));

        /// <summary>
        /// 默认角色模型类型名。
        /// </summary>
        public static readonly string DefaultRoleModelTypeName
            = typeof(RoleModel).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 角色模型类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultRoleModelTypeName"/>。
        /// </value>
        public string RoleModelTypeName { get; set; } = DefaultRoleModelTypeName;

        #endregion


        #region TokenModelTypeName

        /// <summary>
        /// 令牌模型类型名键。
        /// </summary>
        public static readonly string TokenModelTypeNameKey
            = (KeyPrefix + nameof(TokenModelTypeName));

        /// <summary>
        /// 默认令牌模型类型名。
        /// </summary>
        public static readonly string DefaultTokenModelTypeName
            = typeof(TokenModel).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 令牌模型类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultTokenModelTypeName"/>。
        /// </value>
        public string TokenModelTypeName { get; set; } = DefaultTokenModelTypeName;

        #endregion


        #region UserModel

        /// <summary>
        /// 用户模型类型名键。
        /// </summary>
        public static readonly string UserModelTypeNameKey
            = (KeyPrefix + nameof(UserModelTypeName));

        /// <summary>
        /// 默认用户模型类型名。
        /// </summary>
        public static readonly string DefaultUserModelTypeName
            = typeof(UserModel).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 用户模型类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultUserModelTypeName"/>。
        /// </value>
        public string UserModelTypeName { get; set; } = DefaultUserModelTypeName;

        #endregion

    }


    /// <summary>
    /// 认证发送器设置。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AuthenticationSendersSetting
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix
            = (AuthenticationSettings.KeyPrefix + nameof(AuthenticationSettings.Senders) + ":");


        #region EmailSenderTypeName

        /// <summary>
        /// 邮箱发送器类型名键。
        /// </summary>
        public static readonly string EmailSenderTypeNameKey
            = (KeyPrefix + nameof(EmailSenderTypeName));

        /// <summary>
        /// 默认邮箱发送器类型名。
        /// </summary>
        public static readonly string DefaultEmailSenderTypeName
            = typeof(EmailSender).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 邮箱发送器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultEmailSenderTypeName"/>。
        /// </value>
        public string EmailSenderTypeName { get; set; } = DefaultEmailSenderTypeName;

        #endregion


        #region SmsSenderTypeName

        /// <summary>
        /// 短信发送器类型名键。
        /// </summary>
        public static readonly string SmsSenderTypeNameKey
            = (KeyPrefix + nameof(SmsSenderTypeName));

        /// <summary>
        /// 默认短信发送器类型名。
        /// </summary>
        public static readonly string DefaultSmsSenderTypeName
            = typeof(SmsSender).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 短信发送器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultSmsSenderTypeName"/>。
        /// </value>
        public string SmsSenderTypeName { get; set; } = DefaultSmsSenderTypeName;

        #endregion

    }
}
