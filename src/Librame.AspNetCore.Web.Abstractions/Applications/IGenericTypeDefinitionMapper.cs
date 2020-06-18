#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.AspNetCore.Web.Applications
{
    using Builders;

    /// <summary>
    /// 泛型类型定义映射器接口。
    /// </summary>
    public interface IGenericTypeDefinitionMapper
    {
        /// <summary>
        /// 映射泛型类型参数集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <param name="genericTypeParameters">给定的泛型类型参数数组。</param>
        /// <returns>返回类型数组集合。</returns>
        Type[] MapGenericTypeArguments(IWebBuilder builder, Type[] genericTypeParameters);
    }
}
