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
    /// 带用户泛型参数的页面应用模型约定。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    public class PageApplicationModelConventionWithUser<TUser> : IPageApplicationModelConvention
        where TUser : class
    {
        /// <summary>
        /// 应用约定。
        /// </summary>
        /// <param name="model">给定的 <see cref="PageApplicationModel"/>。</param>
        public void Apply(PageApplicationModel model)
        {
            if (model.ModelType.TryGetCustomAttribute(out PageApplicationModelWithUserAttribute attribute))
            {
                var templateInstance = attribute.ImplementationType.MakeGenericType(typeof(TUser));
                model.ModelType = templateInstance.GetTypeInfo();
            }
        }

    }
}
