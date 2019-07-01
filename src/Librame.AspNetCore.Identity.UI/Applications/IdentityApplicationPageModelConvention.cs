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
using System;
using System.Reflection;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;
    using Extensions;

    /// <summary>
    /// 身份应用程序页面模型约定。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    internal class IdentityApplicationPageModelConvention<TUser> : IPageApplicationModelConvention where TUser : class
    {
        /// <summary>
        /// 应用约定。
        /// </summary>
        /// <param name="model">给定的 <see cref="PageApplicationModel"/>。</param>
        public void Apply(PageApplicationModel model)
        {
            var defaultUIAttribute = model.ModelType.GetCustomAttribute<ThemepackTemplateAttribute>();
            if (defaultUIAttribute == null) return;

            ValidateTemplate(defaultUIAttribute.PageType);

            var templateInstance = defaultUIAttribute.PageType.MakeGenericType(typeof(TUser));
            model.ModelType = templateInstance.GetTypeInfo();
        }


        private void ValidateTemplate(Type template)
        {
            if (template.IsAbstract || !template.IsGenericTypeDefinition)
                throw new InvalidOperationException("Implementation type can't be abstract or non generic.");

            var genericArguments = template.GetGenericArguments();
            if (genericArguments.Length != 1)
                throw new InvalidOperationException("Implementation type contains wrong generic arity.");
        }

    }
}
