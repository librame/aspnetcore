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
    /// 关联接口。
    /// </summary>
    public interface IAssociation
    {
        /// <summary>
        /// 关联标识。
        /// </summary>
        string AssocId { get; set; }
    }
}
