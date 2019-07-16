#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Data
{
    /// <summary>
    /// 浏览统计接口。
    /// </summary>
    public interface IVisitStatistic
    {
        /// <summary>
        /// 浏览数。
        /// </summary>
        int ViewCount { get; set; }

        /// <summary>
        /// 顶数。
        /// </summary>
        int UpCount { get; set; }

        /// <summary>
        /// 踩数。
        /// </summary>
        int DownCount { get; set; }

        /// <summary>
        /// 喜欢数。
        /// </summary>
        int FavoriteCount { get; set; }
    }
}
