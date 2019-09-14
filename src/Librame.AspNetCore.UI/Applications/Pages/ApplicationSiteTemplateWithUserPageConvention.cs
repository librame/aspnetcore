#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 带用户泛型参数的应用站点模板页面模型约定。
    /// </summary>
    public class ApplicationSiteTemplateWithUserPageConvention : IPageApplicationModelConvention
    {
        private readonly IUiBuilder _builder;


        /// <summary>
        /// 构造一个 <see cref="ApplicationSiteTemplateWithUserPageConvention"/>。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUiBuilder"/>。</param>
        public ApplicationSiteTemplateWithUserPageConvention(IUiBuilder builder)
        {
            _builder = builder.NotNull(nameof(builder));
        }


        /// <summary>
        /// 应用约定。
        /// </summary>
        /// <param name="model">给定的 <see cref="PageApplicationModel"/>。</param>
        public void Apply(PageApplicationModel model)
        {
            if (model.ModelType.TryGetCustomAttribute(out ApplicationSiteTemplateWithUserAttribute attribute))
            {
                var implementationModelType = attribute.MakeGenericTypeInfo(_builder.UserType);
                model.ModelType = implementationModelType.GetTypeInfo();
            }
        }

    }
}
