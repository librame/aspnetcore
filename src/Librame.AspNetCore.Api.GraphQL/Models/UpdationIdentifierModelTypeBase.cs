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
    /// 更新标识符模型类型基类。
    /// </summary>
    /// <typeparam name="TUpdationIdentifierModel">指定实现 <see cref="IUpdationIdentifierModel"/> 的更新标识符模型类型。</typeparam>
    public class UpdationIdentifierModelTypeBase<TUpdationIdentifierModel> : CreationIdentifierModelTypeBase<TUpdationIdentifierModel>
        where TUpdationIdentifierModel : IUpdationIdentifierModel
    {
        /// <summary>
        /// 构造一个 <see cref="UpdationIdentifierModelTypeBase{TUpdationIdentifierModel}"/>。
        /// </summary>
        protected UpdationIdentifierModelTypeBase()
            : base()
        {
            Field(f => f.UpdatedTime);
            Field(f => f.UpdatedBy);
        }

    }
}
