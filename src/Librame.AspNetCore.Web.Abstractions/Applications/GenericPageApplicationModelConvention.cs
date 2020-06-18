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

namespace Librame.AspNetCore.Web.Applications
{
    using AspNetCore.Web.Builders;
    using Extensions;

    /// <summary>
    /// 泛型页面应用模型约定。
    /// </summary>
    public class GenericPageApplicationModelConvention : IPageApplicationModelConvention
    {
        /// <summary>
        /// 构造一个 <see cref="GenericPageApplicationModelConvention"/>。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        public GenericPageApplicationModelConvention(IWebBuilder builder)
        {
            Builder = builder.NotNull(nameof(builder));
        }


        /// <summary>
        /// Web 构建器。
        /// </summary>
        /// <value>返回 <see cref="IWebBuilder"/>。</value>
        public IWebBuilder Builder { get; }


        /// <summary>
        /// 应用约定。
        /// </summary>
        /// <param name="model">给定的 <see cref="PageApplicationModel"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public void Apply(PageApplicationModel model)
        {
            if (model.ModelType.TryGetCustomAttribute(out GenericApplicationModelAttribute attribute))
            {
                var implementationType = attribute.BuildImplementationType(Builder);
                model.ModelType = implementationType.GetTypeInfo();
            }
        }

    }
}
