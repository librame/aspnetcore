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
    using Extensions.Data.Stores;

    /// <summary>
    /// 抽象更新模型。
    /// </summary>
    public abstract class AbstractUpdationModel
        : AbstractCreationModel, IUpdationModel
    {
        /// <summary>
        /// 更新时间。
        /// </summary>
        public string UpdatedTime { get; set; }

        /// <summary>
        /// 更新者。
        /// </summary>
        public string UpdatedBy { get; set; }


        /// <summary>
        /// 填充更新模型。
        /// </summary>
        /// <typeparam name="TUpdatedBy">指定的更新者类型。</typeparam>
        /// <typeparam name="TUpdatedTime">指定的更新时间类型。</typeparam>
        /// <param name="updation">给定的 <see cref="IUpdationIdentifier{TId, TUpdatedBy, TUpdatedTime}"/>。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public virtual void Populate<TUpdatedBy, TUpdatedTime>
            (IUpdation<TUpdatedBy, TUpdatedTime> updation)
            where TUpdatedBy : IEquatable<TUpdatedBy>
            where TUpdatedTime : struct
        {
            base.Populate(updation);

            UpdatedTime = updation.UpdatedTime.ToString();
            UpdatedBy = updation.UpdatedBy.ToString();
        }

    }
}
