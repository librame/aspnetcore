#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace System.Collections.Generic
{
    /// <summary>
    /// 分页信息。
    /// </summary>
    public class PagingInfo
    {
        /// <summary>
        /// 构造一个 <see cref="PagingInfo"/> 默认实例。
        /// </summary>
        public PagingInfo()
            : this(10, -1)
        {
        }
        /// <summary>
        /// 构造一个 <see cref="PagingInfo"/> 实例。
        /// </summary>
        /// <param name="size">给定的页大小。</param>
        /// <param name="index">给定的页索引。</param>
        public PagingInfo(int size, int index)
        {
            Total = Pages = Skip = 0;
            Size = size;
            Index = index;
        }


        /// <summary>
        /// 获取总条数。
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 获取总页数。
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// 获取跳过的条数。
        /// </summary>
        public int Skip { get; set; }
        
        /// <summary>
        /// 获取页大小。
        /// </summary>
        public int Size { get; set; }
        
        /// <summary>
        /// 获取页索引。
        /// </summary>
        public int Index { get; set; }
    }
}
