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

namespace Librame.AspNetCore.Api.Models
{
    using Extensions.Data.Stores;

    /// <summary>
    /// 更新模型接口。
    /// </summary>
    public interface IUpdationModel : ICreationModel
    {
        /// <summary>
        /// 更新时间。
        /// </summary>
        string UpdatedTime { get; set; }

        /// <summary>
        /// 更新者。
        /// </summary>
        string UpdatedBy { get; set; }


        /// <summary>
        /// 填充更新模型。
        /// </summary>
        /// <typeparam name="TUpdatedBy">指定的更新者类型。</typeparam>
        /// <typeparam name="TUpdatedTime">指定的更新时间类型。</typeparam>
        /// <param name="updation">给定的 <see cref="IUpdationIdentifier{TUpdatedBy, TUpdatedTime}"/>。</param>
        void Populate<TUpdatedBy, TUpdatedTime>
            (IUpdation<TUpdatedBy, TUpdatedTime> updation)
            where TUpdatedBy : IEquatable<TUpdatedBy>
            where TUpdatedTime : struct;
    }
}
