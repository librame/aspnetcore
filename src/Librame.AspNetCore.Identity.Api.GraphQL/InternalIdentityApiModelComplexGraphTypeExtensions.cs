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

namespace Librame.AspNetCore.Identity.Api
{
    /// <summary>
    /// 内部身份 API 模型复合图形类型静态扩展。
    /// </summary>
    internal static class InternalIdentityApiModelComplexGraphTypeExtensions
    {
        /// <summary>
        /// 添加登入字段集合。
        /// </summary>
        /// <typeparam name="TModel">指定的模型类型。</typeparam>
        /// <param name="graphType">给定的 <see cref="ComplexGraphType{TModel}"/>。</param>
        /// <returns>返回 <see cref="ComplexGraphType{TModel}"/>。</returns>
        public static ComplexGraphType<TModel> AddLoginApiModelFields<TModel>(this ComplexGraphType<TModel> graphType)
            where TModel : LoginApiModel
        {
            graphType.Field(f => f.Name);
            graphType.Field(f => f.Password);
            graphType.Field(f => f.RememberMe, nullable: true);
            graphType.Field(f => f.UserId, nullable: true);
            graphType.Field(f => f.Token, nullable: true);

            return graphType;
        }

        /// <summary>
        /// 添加注册字段集合。
        /// </summary>
        /// <typeparam name="TModel">指定的模型类型。</typeparam>
        /// <param name="graphType">给定的 <see cref="ComplexGraphType{TModel}"/>。</param>
        /// <returns>返回 <see cref="ComplexGraphType{TModel}"/>。</returns>
        public static ComplexGraphType<TModel> AddRegisterApiModelFields<TModel>(this ComplexGraphType<TModel> graphType)
            where TModel : RegisterApiModel
        {
            graphType.Field(f => f.Email);
            graphType.Field(f => f.Name);
            graphType.Field(f => f.Password);
            graphType.Field(f => f.ConfirmEmailUrl);
            graphType.Field(f => f.UserId, nullable: true);

            return graphType;
        }

    }
}
