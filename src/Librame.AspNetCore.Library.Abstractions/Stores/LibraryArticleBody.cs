#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.AspNetCore.Library
{
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 文库文章主体。
    /// </summary>
    public class LibraryArticleBody : LibraryArticleBody<string, string>
    {
        /// <summary>
        /// 构造一个 <see cref="LibraryArticleBody"/> 实例。
        /// </summary>
        public LibraryArticleBody()
            : base()
        {
            // 默认使用空标识符，新增推荐使用服务注入
            ArticleId = Id = UniqueIdentifier.Empty;
        }
    }

    /// <summary>
    /// 文库文章主体。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    /// <typeparam name="TArticleId">指定的文章标识类型。</typeparam>
    public class LibraryArticleBody<TId, TArticleId> : AbstractId<TId>
        where TId : IEquatable<TId>
        where TArticleId : IEquatable<TArticleId>
    {
        /// <summary>
        /// 文章标识。
        /// </summary>
        public virtual TArticleId ArticleId { get; set; }

        /// <summary>
        /// 正文。
        /// </summary>
        public virtual string Body { get; set; }
    }
}
