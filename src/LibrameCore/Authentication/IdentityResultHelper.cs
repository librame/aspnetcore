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
using Microsoft.AspNetCore.Identity;
using System;

namespace LibrameCore.Authentication
{
    /// <summary>
    /// 身份结果助手。
    /// </summary>
    public class IdentityResultHelper
    {

        #region Failed

        /// <summary>
        /// 名称为空。
        /// </summary>
        public static readonly IdentityResult NameIsEmpty = BuildFailed("011", Resources.IdentityResult.NameIsEmpty, "Name");
        /// <summary>
        /// 名称已存在（唯一性验证）。
        /// </summary>
        public static readonly IdentityResult NameExists = BuildFailed("012", Resources.IdentityResult.NameExists, "Name");
        /// <summary>
        /// 名称不存在（查询用户）。
        /// </summary>
        public static readonly IdentityResult NameNotExists = BuildFailed("013", Resources.IdentityResult.NameNotExists, "Name");
        /// <summary>
        /// 无效的名称。
        /// </summary>
        public static readonly IdentityResult InvalidName = BuildFailed("014", Resources.IdentityResult.InvalidName, "Name");


        /// <summary>
        /// 密码为空。
        /// </summary>
        public static readonly IdentityResult PasswordIsEmpty = BuildFailed("021", Resources.IdentityResult.PasswordIsEmpty, "Password");
        /// <summary>
        /// 密码不正确。
        /// </summary>
        public static readonly IdentityResult PasswordError = BuildFailed("022", Resources.IdentityResult.PasswordError, "Password");


        /// <summary>
        /// 无效的令牌。
        /// </summary>
        public static readonly IdentityResult InvalidToken = BuildFailed("031", Resources.IdentityResult.InvalidToken);


        /// <summary>
        /// 无效的角色。
        /// </summary>
        public static readonly IdentityResult InvalidRole = BuildFailed("041", Resources.IdentityResult.InvalidRole);


        /// <summary>
        /// 创建认证失败。
        /// </summary>
        /// <param name="ex">给定的异常。</param>
        /// <returns>返回身份错误。</returns>
        public static IdentityResult CreateFailed(Exception ex)
        {
            // 系统未知异常为0级
            return BuildFailed("001", ex.AsInnerMessage(), null);
        }

        /// <summary>
        /// 构建身份认证失败的结果。
        /// </summary>
        /// <param name="suffixCode">给定的后缀码（格式：[001-ErrorNumber]）。</param>
        /// <param name="description">给定的描述。</param>
        /// <param name="key">给定的键名（可选；通常为模型的属性名）。</param>
        /// <returns>返回身份错误。</returns>
        private static IdentityResult BuildFailed(string suffixCode, string description, string key = null)
        {
            var code = "1c003" + suffixCode;

            return BuildBaseFailed(code, description, key);
        }

        /// <summary>
        /// 构建身份失败的结果。
        /// </summary>
        /// <param name="code">给定的代码（格式：[0-系统级、1-用户级]+[s-Standard、c-Core]+[001-ModuleNumber]+[001-ErrorNumber]）。</param>
        /// <param name="description">给定的描述。</param>
        /// <param name="key">给定的键名（可选；通常为模型的属性名）。</param>
        /// <returns>返回身份错误。</returns>
        private static IdentityResult BuildBaseFailed(string code, string description, string key = null)
        {
            var error = new LibrameIdentityError()
            {
                Key = key,
                Code = code,
                Description = description
            };

            return IdentityResult.Failed(error);
        }

        #endregion

    }
}
