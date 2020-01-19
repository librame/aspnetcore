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
    using Models;

    internal static class IdentityApiModelComplexGraphTypeExtensions
    {
        public static ComplexGraphType<TModel> AddLoginApiModelFields<TModel>(this ComplexGraphType<TModel> graphType)
            where TModel : LoginApiModel
        {
            graphType.Field(f => f.Email);
            graphType.Field(f => f.Password);
            graphType.Field(f => f.RememberMe, nullable: true);
            graphType.Field(f => f.UserId, nullable: true);
            graphType.Field(f => f.Token, nullable: true);

            return graphType;
        }

        public static ComplexGraphType<TModel> AddRegisterApiModelFields<TModel>(this ComplexGraphType<TModel> graphType)
            where TModel : RegisterApiModel
        {
            graphType.Field(f => f.Email);
            graphType.Field(f => f.Password);
            graphType.Field(f => f.ConfirmEmailUrl);
            graphType.Field(f => f.UserId, nullable: true);

            return graphType;
        }

    }
}
