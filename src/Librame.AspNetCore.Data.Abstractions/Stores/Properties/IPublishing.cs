#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Data.Stores
{
    /// <summary>
    /// 发布接口。
    /// </summary>
    /// <typeparam name="TDateTime">指定的日期与时间类型（提供对 DateTime 或 DateTimeOffset 的支持）。</typeparam>
    public interface IPublishing<TDateTime>
        where TDateTime : struct
    {
        /// <summary>
        /// 发布时间。
        /// </summary>
        TDateTime PublishTime { get; set; }

        /// <summary>
        /// 发布链接。
        /// </summary>
        string PublishLink { get; set; }
    }
}
