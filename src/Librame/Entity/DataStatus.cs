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

namespace Librame.Entity
{
    /// <summary>
    /// 数据状态。
    /// </summary>
    [Description("数据状态")]
    public enum DataStatus
    {
        /// <summary>
        /// 默认。
        /// </summary>
        [Description("默认")]
        Default = 0,

        /// <summary>
        /// 公开的。
        /// </summary>
        [Description("公开的")]
        Public = 1,

        /// <summary>
        /// 内部的。
        /// </summary>
        [Description("内部的")]
        Internal = 2,

        /// <summary>
        /// 私有的。
        /// </summary>
        [Description("私有的")]
        Private = 4,

        /// <summary>
        /// 受保护。
        /// </summary>
        [Description("受保护")]
        Protected = 8,

        /// <summary>
        /// 已删除。
        /// </summary>
        [Description("已删除")]
        Deleted = 16,

        /// <summary>
        /// 已过时。
        /// </summary>
        [Description("已过时")]
        Obsoleted = 32,

        /// <summary>
        /// 已锁定。
        /// </summary>
        [Description("已锁定")]
        Locked = 64
    }
}