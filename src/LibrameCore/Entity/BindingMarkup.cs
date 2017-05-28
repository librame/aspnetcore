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

namespace LibrameCore.Entity
{
    /// <summary>
    /// 绑定标记。
    /// </summary>
    [Description("绑定标记")]
    public enum BindingMarkup
    {
        /// <summary>
        /// 所有。
        /// </summary>
        [Description("所有")]
        All = 0,

        /// <summary>
        /// 创建。
        /// </summary>
        [Description("创建")]
        Create = 1,

        /// <summary>
        /// 修改。
        /// </summary>
        [Description("修改")]
        Edit = 2,

        /// <summary>
        /// 删除。
        /// </summary>
        [Description("删除")]
        Delete = 4
    }
}
