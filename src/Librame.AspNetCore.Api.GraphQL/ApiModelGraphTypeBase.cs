#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;

namespace Librame.AspNetCore.Api
{
    using Models;

    /// <summary>
    /// API 模型图形类型基类。
    /// </summary>
    /// <typeparam name="TModel">指定的模型类型。</typeparam>
    public class ApiModelGraphTypeBase<TModel> : ObjectGraphType<TModel>
        where TModel : AbstractApiModel
    {
        /// <summary>
        /// 构造一个 <see cref="ApiModelGraphTypeBase{TModel}"/> 实例。
        /// </summary>
        protected ApiModelGraphTypeBase()
        {
            this.AddApiModelBaseFields();
        }

    }
}
