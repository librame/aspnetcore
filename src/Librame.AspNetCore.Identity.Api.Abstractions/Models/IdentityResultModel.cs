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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Api.Models
{
    /// <summary>
    /// 身份结果模型。
    /// </summary>
    public class IdentityResultModel
    {
        /// <summary>
        /// 操作成功。
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// 错误集合。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public List<IdentityError> Errors { get; set; }
    }
}
