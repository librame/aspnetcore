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
    using Extensions.Core;

    /// <summary>
    /// 身份构建器依赖选项。
    /// </summary>
    public class IdentityBuilderDependencyOptions : ExtensionBuilderDependencyOptions<IdentityBuilderOptions>
    {
        /// <summary>
        /// <see cref="IdentityOptions"/> 配置动作。
        /// </summary>
        public Action<IdentityOptions> RawAction { get; set; }
            = _ => { };
    }
}
