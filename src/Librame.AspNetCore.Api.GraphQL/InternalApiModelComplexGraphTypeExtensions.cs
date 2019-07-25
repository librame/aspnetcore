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
    /// 内部 API 模型复合图形类型静态扩展。
    /// </summary>
    internal static class InternalApiModelComplexGraphTypeExtensions
    {
        /// <summary>
        /// 添加 API 模型基类字段集合。
        /// </summary>
        /// <typeparam name="TModel">指定的模型类型。</typeparam>
        /// <param name="graphType">给定的 <see cref="ComplexGraphType{TModel}"/>。</param>
        /// <returns>返回 <see cref="ComplexGraphType{TModel}"/>。</returns>
        public static ComplexGraphType<TModel> AddApiModelBaseFields<TModel>(this ComplexGraphType<TModel> graphType)
            where TModel : AbstractApiModel
        {
            graphType.Field(f => f.IsError, nullable: true);
            graphType.Field(f => f.Message, nullable: true);
            graphType.Field(f => f.RedirectUrl, nullable: true);
            graphType.Field<ListGraphType<ExceptionGraphType>>(nameof(AbstractApiModel.Errors));

            return graphType;
        }

    }
}
