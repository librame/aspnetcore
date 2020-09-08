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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Api.Models
{
    using Extensions;
    using Extensions.Data.Stores;

    /// <summary>
    /// 抽象创建模型。
    /// </summary>
    public abstract class AbstractCreationModel
        : ICreationModel
    {
        /// <summary>
        /// 创建时间。
        /// </summary>
        public string CreatedTime { get; set; }

        /// <summary>
        /// 创建者。
        /// </summary>
        public string CreatedBy { get; set; }


        /// <summary>
        /// 填充创建模型。
        /// </summary>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <typeparam name="TCreatedTime">指定的创建时间类型。</typeparam>
        /// <param name="creation">给定的 <see cref="ICreationIdentifier{TId, TCreatedBy, TCreatedTime}"/>。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public virtual void Populate<TCreatedBy, TCreatedTime>
            (ICreation<TCreatedBy, TCreatedTime> creation)
            where TCreatedBy : IEquatable<TCreatedBy>
            where TCreatedTime : struct
        {
            creation.NotNull(nameof(creation));

            CreatedTime = creation.CreatedTime.ToString();
            CreatedBy = creation.CreatedBy.ToString();
        }

    }
}
