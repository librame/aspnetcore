#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Api.Models
{
    /// <summary>
    /// 模型类型接口。
    /// </summary>
    public interface IModelType
    {
        /// <summary>
        /// 名称。
        /// </summary>
        string Name { get; }
    }
}
