#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Web.Builders
{
    using Extensions.Core.Builders;

    /// <summary>
    /// Web 构建器依赖。
    /// </summary>
    public class WebBuilderDependency : AbstractExtensionBuilderDependency<WebBuilderOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="WebBuilderDependency"/>。
        /// </summary>
        public WebBuilderDependency()
            : base(nameof(WebBuilderDependency))
        {
        }

    }
}
