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
    /// <typeparam name="TQuery">指定的查询类型。</typeparam>
    /// <typeparam name="TMutation">指定的变化类型。</typeparam>
    public interface IApiSchema<out TQuery, out TMutation>
        where TQuery : class
        where TMutation : class
    {
        /// <summary>
        /// 查询实例。
        /// </summary>
        TQuery Query { get; }
		
		/// <summary>
        /// 变化实例。
        /// </summary>
		TMutation Mutation { get; }
    }
}
