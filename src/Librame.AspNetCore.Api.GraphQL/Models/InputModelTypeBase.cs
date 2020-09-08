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
    /// 输入模型类型基类。
    /// </summary>
    /// <typeparam name="TModel">指定的模型类型。</typeparam>
    public class InputModelTypeBase<TModel> : InputObjectGraphType<TModel>, IModelType
    {
        /// <summary>
        /// 构造一个 <see cref="InputModelTypeBase{TModel}"/>。
        /// </summary>
        protected InputModelTypeBase()
        {
            Name = ApiSettings.Preference.InputModelTypeNameFactory.Invoke(GetType());
        }

    }
}
