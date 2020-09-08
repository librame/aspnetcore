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
    /// 更新模型类型基类。
    /// </summary>
    /// <typeparam name="TUpdationModel">指定实现 <see cref="IUpdationModel"/> 的更新模型类型。</typeparam>
    public class UpdationModelTypeBase<TUpdationModel> : CreationModelTypeBase<TUpdationModel>
        where TUpdationModel : IUpdationModel
    {
        /// <summary>
        /// 构造一个 <see cref="UpdationModelTypeBase{TUpdationModel}"/>。
        /// </summary>
        protected UpdationModelTypeBase()
            : base()
        {
            Field(f => f.UpdatedTime);
            Field(f => f.UpdatedBy);
        }

    }
}
