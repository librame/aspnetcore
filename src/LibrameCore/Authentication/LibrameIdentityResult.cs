#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameCore.Authentication.Models;
using LibrameStandard.Utilities;
using System;

namespace Microsoft.AspNetCore.Identity
{
    /// <summary>
    /// Librame 身份结果。
    /// </summary>
    public class LibrameIdentityResult : IdentityResult
    {
        /// <summary>
        /// 身份结果。
        /// </summary>
        public IdentityResult IdentityResult { get; set; }

        /// <summary>
        /// 用户模型。
        /// </summary>
        public IUserModel User { get; set; }


        #region Failed

        /// <summary>
        /// 名称为空。
        /// </summary>
        public static readonly LibrameIdentityResult NameIsEmpty = BuildAuthenticationFailed("011", LibrameCore.Resources.IdentityResult.NameIsEmpty, "Name");
        /// <summary>
        /// 名称已存在（唯一性验证）。
        /// </summary>
        public static readonly LibrameIdentityResult NameExists = BuildAuthenticationFailed("012", LibrameCore.Resources.IdentityResult.NameExists, "Name");
        /// <summary>
        /// 名称不存在（查询用户）。
        /// </summary>
        public static readonly LibrameIdentityResult NameNotExists = BuildAuthenticationFailed("013", LibrameCore.Resources.IdentityResult.NameNotExists, "Name");
        /// <summary>
        /// 无效的名称。
        /// </summary>
        public static readonly LibrameIdentityResult InvalidName = BuildAuthenticationFailed("014", LibrameCore.Resources.IdentityResult.InvalidName, "Name");


        /// <summary>
        /// 密码为空。
        /// </summary>
        public static readonly LibrameIdentityResult PasswordIsEmpty = BuildAuthenticationFailed("021", LibrameCore.Resources.IdentityResult.PasswordIsEmpty, "Password");
        /// <summary>
        /// 密码不正确。
        /// </summary>
        public static readonly LibrameIdentityResult PasswordError = BuildAuthenticationFailed("022", LibrameCore.Resources.IdentityResult.PasswordError, "Password");


        /// <summary>
        /// 无效的令牌。
        /// </summary>
        public static readonly LibrameIdentityResult InvalidToken = BuildAuthenticationFailed("031", LibrameCore.Resources.IdentityResult.InvalidToken);


        /// <summary>
        /// 无效的角色。
        /// </summary>
        public static readonly LibrameIdentityResult InvalidRole = BuildAuthenticationFailed("041", LibrameCore.Resources.IdentityResult.InvalidRole);


        /// <summary>
        /// 创建认证失败。
        /// </summary>
        /// <param name="ex">给定的异常。</param>
        /// <param name="user">给定的用户模型（可选）。</param>
        /// <returns>返回身份错误。</returns>
        public static LibrameIdentityResult CreateAuthenticationFailed(Exception ex, IUserModel user = null)
        {
            // 系统未知异常为0级
            return BuildAuthenticationFailed("001", ex.AsInnerMessage(), null, user);
        }

        /// <summary>
        /// 构建身份认证失败的结果。
        /// </summary>
        /// <param name="suffixCode">给定的后缀码（格式：[001-ErrorNumber]）。</param>
        /// <param name="description">给定的描述。</param>
        /// <param name="key">给定的键名（可选；通常为模型的属性名）。</param>
        /// <param name="user">给定的用户模型（可选）。</param>
        /// <returns>返回身份错误。</returns>
        private static LibrameIdentityResult BuildAuthenticationFailed(string suffixCode, string description, string key = null, IUserModel user = null)
        {
            var code = "1c003" + suffixCode;

            return BuildIdentityFailed(code, description, key, user);
        }
        /// <summary>
        /// 构建身份失败的结果。
        /// </summary>
        /// <param name="code">给定的代码（格式：[0-系统级、1-用户级]+[s-Standard、c-Core]+[001-ModuleNumber]+[001-ErrorNumber]）。</param>
        /// <param name="description">给定的描述。</param>
        /// <param name="key">给定的键名（可选；通常为模型的属性名）。</param>
        /// <param name="user">给定的用户模型（可选）。</param>
        /// <returns>返回身份错误。</returns>
        private static LibrameIdentityResult BuildIdentityFailed(string code, string description, string key = null, IUserModel user = null)
        {
            var error = new LibrameIdentityError()
            {
                Key = key,
                Code = code,
                Description = description
            };

            return new LibrameIdentityResult
            {
                IdentityResult = Failed(error),
                User = user
            };
        }

        #endregion

    }
}
