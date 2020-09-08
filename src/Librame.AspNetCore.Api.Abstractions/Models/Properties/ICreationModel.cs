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
    /// 创建模型接口。
    /// </summary>
    public interface ICreationModel
    {
        /// <summary>
        /// 创建时间。
        /// </summary>
        string CreatedTime { get; set; }

        /// <summary>
        /// 创建者。
        /// </summary>
        string CreatedBy { get; set; }


        /// <summary>
        /// 填充创建模型。
        /// </summary>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <typeparam name="TCreatedTime">指定的创建时间类型。</typeparam>
        /// <param name="creation">给定的 <see cref="ICreation{TCreatedBy, TCreatedTime}"/>。</param>
        void Populate<TCreatedBy, TCreatedTime>
            (ICreation<TCreatedBy, TCreatedTime> creation)
            where TCreatedBy : IEquatable<TCreatedBy>
            where TCreatedTime : struct;
    }
}
