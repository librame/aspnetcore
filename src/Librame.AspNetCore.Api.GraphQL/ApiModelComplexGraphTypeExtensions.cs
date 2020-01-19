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

    internal static class ApiModelComplexGraphTypeExtensions
    {
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
