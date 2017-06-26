#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Authentication.Models;

namespace Microsoft.AspNetCore.Identity
{
    /// <summary>
    /// 用户身份结果。
    /// </summary>
    public class UserIdentityResult
    {
        /// <summary>
        /// 构造一个用户身份结果实例。
        /// </summary>
        /// <param name="identityResult">给定的身份结果。</param>
        /// <param name="user">给定的用户模型（可选）。</param>
        public UserIdentityResult(IdentityResult identityResult, IUserModel user = null)
        {
            IdentityResult = identityResult;
            User = user;
        }


        /// <summary>
        /// 身份结果。
        /// </summary>
        public IdentityResult IdentityResult { get; protected set; }

        /// <summary>
        /// 用户模型。
        /// </summary>
        public IUserModel User { get; protected set; }

    }
}
