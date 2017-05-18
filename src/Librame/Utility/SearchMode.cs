#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.ComponentModel;

namespace Librame.Utility
{
    /// <summary>
    /// 搜索方式。
    /// </summary>
    [Description("搜索方式")]
    public enum SearchMode
    {
        /// <summary>
        /// 默认。
        /// </summary>
        [Description("默认")]
        Default = 0,

        /// <summary>
        /// 包含。
        /// </summary>
        [Description("包含")]
        Include = 1,

        /// <summary>
        /// 排除。
        /// </summary>
        [Description("排除")]
        Exclude = 2
    }
}
