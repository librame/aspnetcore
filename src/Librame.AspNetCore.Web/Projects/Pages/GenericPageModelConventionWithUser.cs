#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Librame.AspNetCore.Web.Projects
{
    using Builders;
    using Extensions;

    /// <summary>
    /// 带用户的泛型页面模型约定。
    /// </summary>
    public class GenericPageModelConventionWithUser : IPageApplicationModelConvention
    {
        private readonly IWebBuilder _builder;


        /// <summary>
        /// 构造一个 <see cref="GenericPageModelConventionWithUser"/>。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        public GenericPageModelConventionWithUser(IWebBuilder builder)
        {
            _builder = builder.NotNull(nameof(builder));
        }


        /// <summary>
        /// 应用约定。
        /// </summary>
        /// <param name="model">给定的 <see cref="PageApplicationModel"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public void Apply(PageApplicationModel model)
        {
            if (model.ModelType.TryGetCustomAttribute(out GenericApplicationModelAttribute attribute))
            {
                var implementationModelType = attribute.BuildImplementationType(_builder.UserType);
                model.ModelType = implementationModelType.GetTypeInfo();
            }
        }

    }
}
