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

namespace Librame.AspNetCore.Gallery
{
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 图库相册。
    /// </summary>
    public class GalleryAlbum : GalleryAlbum<string, string, int, string, DateTimeOffset>
    {
        /// <summary>
        /// 构造一个 <see cref="GalleryAlbum"/> 实例。
        /// </summary>
        public GalleryAlbum()
            : base()
        {
            // 默认使用空标识符，新增推荐使用服务注入
            EditorId = CoverId = Id = UniqueIdentifier.Empty;
        }
    }


    /// <summary>
    /// 图库相册。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    /// <typeparam name="TPictureId">指定的图片标识。</typeparam>
    /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
    /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
    /// <typeparam name="TDateTime">指定的日期与时间类型（提供对 DateTime 或 DateTimeOffset 的支持）。</typeparam>
    public class GalleryAlbum<TId, TPictureId, TCategoryId, TEditorId, TDateTime> : AbstractEntity<TId>, IPublishing<TDateTime>
        where TId : IEquatable<TId>
        where TPictureId : IEquatable<TPictureId>
        where TCategoryId : IEquatable<TCategoryId>
        where TEditorId : IEquatable<TEditorId>
        where TDateTime : struct
    {
        /// <summary>
        /// 封面标识。
        /// </summary>
        public virtual TPictureId CoverId { get; set; }

        /// <summary>
        /// 分类标识。
        /// </summary>
        public virtual TCategoryId CategoryId { get; set; }

        /// <summary>
        /// 编者标识。
        /// </summary>
        public virtual TEditorId EditorId { get; set; }

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
