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
    /// 标识符模型类型基类。
    /// </summary>
    /// <typeparam name="TIdentifierModel">指定实现 <see cref="IIdentifierModel"/> 的标识符模型类型。</typeparam>
    public class IdentifierModelTypeBase<TIdentifierModel> : ModelTypeBase<TIdentifierModel>
        where TIdentifierModel : IIdentifierModel
    {
        /// <summary>
        /// 构造一个 <see cref="IdentifierModelTypeBase{TIdentifierModel}"/>。
        /// </summary>
        public IdentifierModelTypeBase()
            : base()
        {
            Field(f => f.Id);
        }

    }
}
