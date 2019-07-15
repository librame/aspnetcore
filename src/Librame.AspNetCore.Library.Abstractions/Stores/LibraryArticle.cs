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
    /// 文库文章。
    /// </summary>
    public class LibraryArticle : LibraryArticle<string, int, string, int, DateTimeOffset>
    {
        /// <summary>
        /// 构造一个 <see cref="LibraryArticle"/> 实例。
        /// </summary>
        public LibraryArticle()
            : base()
        {
            // 默认使用空标识符，新增推荐使用服务注入
            EditorId = Id = UniqueIdentifier.Empty;
        }
    }


    /// <summary>
    /// 文库文章。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
    /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
    /// <typeparam name="TSourceId">指定的来源标识类型。</typeparam>
    /// <typeparam name="TDateTime">指定的日期与时间类型（提供对 DateTime 或 DateTimeOffset 的支持）。</typeparam>
    public class LibraryArticle<TId, TCategoryId, TEditorId, TSourceId, TDateTime> : AbstractEntity<TId>, IPublishing<TDateTime>
        where TId : IEquatable<TId>
        where TCategoryId : IEquatable<TCategoryId>
        where TEditorId : IEquatable<TEditorId>
        where TSourceId : IEquatable<TSourceId>
        where TDateTime : struct
    {
        /// <summary>
        /// 分类标识。
        /// </summary>
        public virtual TCategoryId CategoryId { get; set; }

        /// <summary>
        /// 编者标识。
        /// </summary>
        public virtual TEditorId EditorId { get; set; }

        /// <summary>
        /// 来源标识。
        /// </summary>
        public virtual TSourceId SourceId { get; set; }

        /// <summary>
        /// 发布时间。
        /// </summary>
        public virtual TDateTime PublishTime { get; set; }

        /// <summary>
        /// 发布链接。
        /// </summary>
        public virtual string PublishLink { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 副标题。
        /// </summary>
        public virtual string Subtitle { get; set; }

        /// <summary>
        /// 标签集合。
        /// </summary>
        public virtual string Tags { get; set; }
    }
}
