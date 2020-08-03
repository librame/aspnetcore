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
    /// <summary>
    /// API 输入类型基类。
    /// </summary>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    public class ApiInputTypeBase<TSource> : InputObjectGraphType<TSource>
    {
        /// <summary>
        /// 构造一个 <see cref="ApiInputTypeBase{TSource}"/>。
        /// </summary>
        protected ApiInputTypeBase()
        {
            Name = ApiSettings.Preference.ApiInputTypeNameFactory.Invoke(GetType());
        }

    }
}
