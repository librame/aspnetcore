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
    /// API 模型查询 Graph 类型基类。
    /// </summary>
    /// <typeparam name="TModel">指定的模型类型。</typeparam>
    public class ApiModelQueryGraphTypeBase<TModel> : ObjectGraphType<TModel>
        where TModel : AbstractApiModel
    {
        /// <summary>
        /// 构造一个 <see cref="ApiModelQueryGraphTypeBase{TModel}"/> 实例。
        /// </summary>
        protected ApiModelQueryGraphTypeBase()
        {
            Field(f => f.IsError, nullable: true);
            Field(f => f.Message, nullable: true);
            Field(f => f.RedirectUrl, nullable: true);
            Field<ListGraphType<ExceptionGraphType>>(nameof(AbstractApiModel.Errors));
        }


        /// <summary>
        /// 获取查询类型名称（格式通常为：ApiModelQueryType => ApiModelQuery）。
        /// </summary>
        /// <typeparam name="TQueryType">指定的查询类型。</typeparam>
        /// <returns>返回字符串。</returns>
        public static string GetQueryTypeName<TQueryType>()
            where TQueryType : IObjectGraphType
        {
            return typeof(TQueryType).Name.TrimEnd("Type");
        }

    }
}
