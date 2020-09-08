#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;

namespace Librame.AspNetCore.Api.Models
{
    /// <summary>
    /// 模型类型基类。
    /// </summary>
    /// <typeparam name="TModel">指定的模型类型。</typeparam>
    public class ModelTypeBase<TModel> : ObjectGraphType<TModel>, IModelType
    {
        /// <summary>
        /// 构造一个 <see cref="ModelTypeBase{TModel}"/>。
        /// </summary>
        protected ModelTypeBase()
        {
            Name = ApiSettings.Preference.ModelTypeNameFactory.Invoke(GetType());
        }

    }
}
