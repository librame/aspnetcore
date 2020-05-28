#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Resources
{
    using Extensions.Core.Resources;

    /// <summary>
    /// 抽象应用信息资源。
    /// </summary>
    public abstract class AbstractApplicationInfoResource : IResource
    {
        /// <summary>
        /// 显示名称。
        /// </summary>
        public string DisplayName { get; set; }
    }
}
