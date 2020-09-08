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
    /// 创建模型类型基类。
    /// </summary>
    /// <typeparam name="TCreationModel">指定实现 <see cref="ICreationModel"/> 的创建模型类型。</typeparam>
    public class CreationModelTypeBase<TCreationModel> : ModelTypeBase<TCreationModel>
        where TCreationModel : ICreationModel
    {
        /// <summary>
        /// 构造一个 <see cref="CreationModelTypeBase{TCreationModel}"/>。
        /// </summary>
        protected CreationModelTypeBase()
            : base()
        {
            Field(f => f.CreatedTime);
            Field(f => f.CreatedBy);
        }

    }
}
