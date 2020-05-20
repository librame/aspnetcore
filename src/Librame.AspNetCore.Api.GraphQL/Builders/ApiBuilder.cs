#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL;
using GraphQL.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Api.Builders
{
    using Extensions.Core.Builders;
    using Extensions.Core.Services;

    internal class ApiBuilder : AbstractExtensionBuilder, IApiBuilder
    {
        public ApiBuilder(IExtensionBuilder parentBuilder, ApiBuilderDependency dependency)
            : base(parentBuilder, dependency)
        {
            Services.AddSingleton<IApiBuilder>(this);

            AddApiServices();
        }


        public override ServiceCharacteristics GetServiceCharacteristics(Type serviceType)
            => ApiBuilderServiceCharacteristicsRegistration.Register.GetOrDefault(serviceType);


        private void AddApiServices()
        {
            AddService<IDocumentExecuter, DocumentExecuter>();
            AddService<IDocumentWriter, DocumentWriter>();
            AddService<IGraphApiMutation, GraphApiMutation>();
            AddService<IGraphApiQuery, GraphApiQuery>();
            AddService<IGraphApiSubscription, GraphApiSubscription>();
            AddService<IGraphApiSchema, GraphApiSchema>();
        }

    }
}
