#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
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

    /// <summary>
    /// API 构建器。
    /// </summary>
    public class ApiBuilder : AbstractExtensionBuilder, IApiBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="ApiBuilder"/>。
        /// </summary>
        /// <param name="parentBuilder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependency">给定的 <see cref="ApiBuilderDependency"/>。</param>
        public ApiBuilder(IExtensionBuilder parentBuilder, ApiBuilderDependency dependency)
            : base(parentBuilder, dependency)
        {
            Services.AddSingleton<IApiBuilder>(this);

            AddInternalServices();
        }


        private void AddInternalServices()
        {
            AddService<IDocumentExecuter, DocumentExecuter>();
            AddService<IDocumentWriter, DocumentWriter>();

            AddService<IGraphApiMutation, DefaultGraphApiMutation>();
            AddService<IGraphApiQuery, DefaultGraphApiQuery>();
            AddService<IGraphApiSubscription, DefaultGraphApiSubscription>();
            AddService<IGraphApiSchema, GraphApiSchema>();

            AddService<IApiRequest, GraphApiRequest>();
        }


        /// <summary>
        /// 获取指定服务类型的特征。
        /// </summary>
        /// <param name="serviceType">给定的服务类型。</param>
        /// <returns>返回 <see cref="ServiceCharacteristics"/>。</returns>
        public override ServiceCharacteristics GetServiceCharacteristics(Type serviceType)
            => ApiBuilderServiceCharacteristicsRegistration.Register.GetOrDefault(serviceType);

    }
}
