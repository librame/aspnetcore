#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Entity.Descriptors
{
    /// <summary>
    /// 数据、主键描述符接口。
    /// </summary>
    /// <typeparam name="TId">指定的主键类型。</typeparam>
    public interface IDataIdDescriptor<TId> : IIdDescriptor<TId>
        where TId : struct
    {
        /// <summary>
        /// 数据排序。
        /// </summary>
        int DataRank { get; set; }

        /// <summary>
        /// 数据状态。
        /// </summary>
        DataStatus DataStatus { get; set; }
    }
}
