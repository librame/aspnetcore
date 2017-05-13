#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Utility;

namespace System.Collections.Generic
{
    /// <summary>
    /// 分页列表。
    /// </summary>
    /// <typeparam name="T">要分页的对象类型。</typeparam>
    public class PagingList<T> : IPagingable<T>
    {
        /// <summary>
        /// 获取行列表。
        /// </summary>
        public virtual IList<T> Rows { get; }

        /// <summary>
        /// 获取分页信息。
        /// </summary>
        public virtual PagingInfo Info { get; }

        
        /// <summary>
        /// 构造一个 <see cref="PagingList{T}"/> 实例。
        /// </summary>
        /// <param name="rows">给定的行集合。</param>
        /// <param name="info">给定的分页信息。</param>
        public PagingList(IList<T> rows, PagingInfo info)
        {
            Rows = rows.NotNull(nameof(rows));
            Info = info.NotNull(nameof(info));
        }


        /// <summary>
        /// 返回一个循环访问集合的枚举器。
        /// </summary>
        /// <returns>返回枚举器。</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
