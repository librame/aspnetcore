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
    using Extensions;

    /// <summary>
    /// API 模型输入图形类型基类。
    /// </summary>
    /// <typeparam name="TModel">指定的模型类型。</typeparam>
    public class ApiModelInputGraphTypeBase<TModel> : InputObjectGraphType<TModel>
        where TModel : AbstractApiModel
    {
        /// <summary>
        /// 构造一个 <see cref="ApiModelInputGraphTypeBase{TModel}"/> 实例。
        /// </summary>
        protected ApiModelInputGraphTypeBase()
        {
            this.AddApiModelBaseFields();
        }


        /// <summary>
        /// 获取输入类型名称（格式通常为：ApiModelInputType => ApiModelInput）。
        /// </summary>
        /// <typeparam name="TInputType">指定的输入类型。</typeparam>
        /// <returns>返回字符串。</returns>
        public static string GetInputTypeName<TInputType>()
            where TInputType : IInputObjectGraphType
        {
            return typeof(TInputType).Name.TrimEnd("Type");
        }

    }
}
