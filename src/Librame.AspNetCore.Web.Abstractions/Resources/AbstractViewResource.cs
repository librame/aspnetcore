#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Web.Resources
{
    using Extensions.Core.Resources;

    /// <summary>
    /// 抽象视图资源。
    /// </summary>
    public abstract class AbstractViewResource : IResource
    {
        /// <summary>
        /// 标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        public string Descr { get; set; }
    }
}
