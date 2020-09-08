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
    /// 创建标识符模型类型基类。
    /// </summary>
    /// <typeparam name="TCreationIdentifierModel">指定实现 <see cref="ICreationIdentifierModel"/> 的创建标识符模型类型。</typeparam>
    public class CreationIdentifierModelTypeBase<TCreationIdentifierModel> : ModelTypeBase<TCreationIdentifierModel>
        where TCreationIdentifierModel : ICreationIdentifierModel
    {
        /// <summary>
        /// 构造一个 <see cref="CreationIdentifierModelTypeBase{TCreationIdentifierModel}"/>。
        /// </summary>
        public CreationIdentifierModelTypeBase()
            : base()
        {
            Field(f => f.Id);
            Field(f => f.CreatedTime);
            Field(f => f.CreatedBy);
        }

    }
}
