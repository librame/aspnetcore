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
    /// 发表模型类型基类。
    /// </summary>
    /// <typeparam name="TPublicationModel">指定实现 <see cref="IPublicationModel"/> 的发表模型类型。</typeparam>
    public class PublicationModelTypeBase<TPublicationModel> : CreationModelTypeBase<TPublicationModel>
        where TPublicationModel : IPublicationModel
    {
        /// <summary>
        /// 构造一个 <see cref="PublicationModelTypeBase{TPublicationModel}"/>。
        /// </summary>
        protected PublicationModelTypeBase()
            : base()
        {
            Field(f => f.PublishedTime);
            Field(f => f.PublishedBy);
            Field(f => f.PublishedAs);
        }

    }
}
