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
    /// <summary>
    /// API 模型输入类型基类。
    /// </summary>
    /// <typeparam name="TModel">指定的模型类型。</typeparam>
    public class ApiModelInputTypeBase<TModel> : InputObjectGraphType<TModel>
        where TModel : AbstractApiModel
    {
        /// <summary>
        /// 构造一个 <see cref="ApiModelInputTypeBase{TModel}"/> 实例。
        /// </summary>
        public ApiModelInputTypeBase()
        {
            Field(f => f.Errors, true);
            Field(f => f.IsError, true);
            Field(f => f.Message, true);
            Field(f => f.RedirectUrl, true);
        }
    }
}
