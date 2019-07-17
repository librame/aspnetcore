#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Api
{
    /// <summary>
    /// API 架构接口。
    /// </summary>
    public interface IApiSchema
    {
        /// <summary>
        /// API 查询。
        /// </summary>
        IApiQuery Query { get; }
    }
}
