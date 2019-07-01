#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.UI
{
    using Extensions.Core;

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
