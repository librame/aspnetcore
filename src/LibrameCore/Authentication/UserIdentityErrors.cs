#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Utilities;
using System;

namespace Microsoft.AspNetCore.Identity
{
    /// <summary>
    /// 用户身份错误集合。
    /// </summary>
    public class UserIdentityErrors
    {
        /// <summary>
        /// 认证适配器错误前缀。
        /// </summary>
        private static readonly string AuthenticationAdapterErrorPrefix = "1c003";


        #region Name

        /// <summary>
        /// 名称为空。
        /// </summary>
        public static readonly IdentityError NameIsEmpty = BuildIdentityError(AuthenticationAdapterErrorPrefix + "011", "名称为空");

        /// <summary>
        /// 名称不存在。
        /// </summary>
        public static readonly IdentityError NameNotExists = BuildIdentityError(AuthenticationAdapterErrorPrefix + "012", "名称不存在");

        #endregion


        #region Password

        /// <summary>
        /// 密码为空。
        /// </summary>
        public static readonly IdentityError PasswordIsEmpty = BuildIdentityError(AuthenticationAdapterErrorPrefix + "021", "密码为空");

        /// <summary>
        /// 密码不正确。
        /// </summary>
        public static readonly IdentityError PasswordError = BuildIdentityError(AuthenticationAdapterErrorPrefix + "022", "密码不正确");

        #endregion


        #region Token

        /// <summary>
        /// 令牌无效。
        /// </summary>
        public static readonly IdentityError TokenInvalid = BuildIdentityError(AuthenticationAdapterErrorPrefix + "031", "令牌无效");

        #endregion


        /// <summary>
        /// 创建用户错误。
        /// </summary>
        /// <param name="ex">给定的异常。</param>
        /// <returns>返回身份错误。</returns>
        public static IdentityError CreateUserError(Exception ex)
        {
            // 系统未知异常为0级
            return BuildIdentityError(AuthenticationAdapterErrorPrefix + "001", ex.AsInnerMessage());
        }


        /// <summary>
        /// 构建身份错误。
        /// </summary>
        /// <param name="code">给定的代码（格式：[0-系统级、1-用户级]+[s-Standard、c-Core]+[001-AdapterNumber]+[001-ErrorNumber]）。</param>
        /// <param name="description">给定的描述。</param>
        /// <returns>返回身份错误。</returns>
        private static IdentityError BuildIdentityError(string code, string description)
        {
            return new IdentityError()
            {
                Code = code,
                Description = description
            };
        }

    }
}
