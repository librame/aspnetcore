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
    /// 抽象更新标识符模型。
    /// </summary>
    public abstract class AbstractUpdationIdentifierModel
        : AbstractCreationIdentifierModel, IUpdationIdentifierModel
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
        /// 填充更新标识符模型。
        /// </summary>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <typeparam name="TUpdatedBy">指定的更新者类型。</typeparam>
        /// <typeparam name="TUpdatedTime">指定的更新时间类型。</typeparam>
        /// <param name="updation">给定的 <see cref="IUpdationIdentifier{TId, TUpdatedBy, TUpdatedTime}"/>。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public virtual void Populate<TId, TUpdatedBy, TUpdatedTime>
            (IUpdationIdentifier<TId, TUpdatedBy, TUpdatedTime> updation)
            where TId : IEquatable<TId>
            where TUpdatedBy : IEquatable<TUpdatedBy>
            where TUpdatedTime : struct
        {
            base.Populate(updation);

            UpdatedTime = updation.UpdatedTime.ToString();
            UpdatedBy = updation.UpdatedBy.ToString();
        }

        /// <summary>
        /// 填充更新模型。
        /// </summary>
        /// <typeparam name="TUpdatedBy">指定的更新者类型。</typeparam>
        /// <typeparam name="TUpdatedTime">指定的更新时间类型。</typeparam>
        /// <param name="updation">给定的 <see cref="IUpdation{TUpdatedBy, TUpdatedTime}"/>。</param>
        void IUpdationModel.Populate<TUpdatedBy, TUpdatedTime>(IUpdation<TUpdatedBy, TUpdatedTime> updation)
        {
            base.Populate(updation);

            UpdatedTime = updation.UpdatedTime.ToString();
            UpdatedBy = updation.UpdatedBy.ToString();
        }

    }
}
