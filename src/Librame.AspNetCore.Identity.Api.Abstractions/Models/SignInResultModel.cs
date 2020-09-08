#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Api.Models
{
    using Extensions;

    /// <summary>
    /// 登入结果模型。
    /// </summary>
    public class SignInResultModel : SignInResult
    {
        /// <summary>
        /// 构造一个 <see cref="SignInResultModel"/>。
        /// </summary>
        /// <param name="result">给定的 <see cref="SignInResult"/>。</param>
        /// <param name="user">给定的 <see cref="UserModel"/>（可选）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public SignInResultModel(SignInResult result, UserModel user = null)
            : base()
        {
            result.NotNull(nameof(result));

            IsLockedOut = result.IsLockedOut;
            IsNotAllowed = result.IsNotAllowed;
            RequiresTwoFactor = result.RequiresTwoFactor;
            Succeeded = result.Succeeded;

            User = user;
        }


        /// <summary>
        /// 用户模型。
        /// </summary>
        public UserModel User { get; set; }
    }
}
