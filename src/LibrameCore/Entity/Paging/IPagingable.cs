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
    /// 公开分页数，该分页数支持在指定类型的集合上进行简单分页。
    /// </summary>
    /// <typeparam name="T">要分页的对象的类型。</typeparam>
    public interface IPagingable<T> : IEnumerable<T>
    {
        /// <summary>
        /// 获取行列表。
        /// </summary>
        IList<T> Rows { get; }

        /// <summary>
        /// 获取分页信息。
        /// </summary>
        PagingInfo Info { get; }
    }
}
