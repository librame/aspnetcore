#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;

namespace Librame.AspNetCore.Api.Types
{
    using Extensions;

    /// <summary>
    /// API 类型基类。
    /// </summary>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    public class ApiTypeBase<TSource> : ObjectGraphType<TSource>, IApiType
    {
        /// <summary>
        /// 构造一个 <see cref="ApiTypeBase{TSource}"/> 实例。
        /// </summary>
        protected ApiTypeBase()
        {
            Name = ApiSettings.Preference.ApiTypeNameFactory.Invoke(GetType());
        }

    }
}
