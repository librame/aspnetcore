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
    /// <typeparam name="TPublicationIdentifierModel">指定实现 <see cref="IPublicationIdentifierModel"/> 的更新标识符模型类型。</typeparam>
    public class PublicationIdentifierModelTypeBase<TPublicationIdentifierModel> : CreationIdentifierModelTypeBase<TPublicationIdentifierModel>
        where TPublicationIdentifierModel : IPublicationIdentifierModel
    {
        /// <summary>
        /// 构造一个 <see cref="PublicationIdentifierModelTypeBase{TPublicationIdentifierModel}"/>。
        /// </summary>
        protected PublicationIdentifierModelTypeBase()
            : base()
        {
            Field(f => f.PublishedTime);
            Field(f => f.PublishedBy);
            Field(f => f.PublishedAs);
        }

    }
}
