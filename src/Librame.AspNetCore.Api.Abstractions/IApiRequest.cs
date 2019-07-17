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
    /// API 请求接口。
    /// </summary>
    public interface IApiRequest
    {
        /// <summary>
        /// 请求查询。
        /// </summary>
        string Query { get; set; }
    }
}
