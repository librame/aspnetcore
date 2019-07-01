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
    using Extensions.Data;

    /// <summary>
    /// 图库图片声明。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    /// <typeparam name="TPictureId">指定的图片标识类型。</typeparam>
    /// <typeparam name="TClaimId">指定的声明标识类型。</typeparam>
    public class GalleryPictureClaim<TId, TPictureId, TClaimId> : AbstractId<TId>, IAssociation, IVisitStatistic
        where TId : IEquatable<TId>
        where TPictureId : IEquatable<TPictureId>
        where TClaimId : IEquatable<TClaimId>
    {
        /// <summary>
        /// 图片标识。
        /// </summary>
        public virtual TPictureId PictureId { get; set; }

        /// <summary>
        /// 声明标识。
        /// </summary>
        public virtual TClaimId ClaimId { get; set; }

        /// <summary>
        /// 关联标识。
        /// </summary>
        public virtual string AssocId { get; set; }

        /// <summary>
        /// 浏览数。
        /// </summary>
        public virtual int ViewCount { get; set; }

        /// <summary>
        /// 顶数。
        /// </summary>
        public virtual int UpCount { get; set; }

        /// <summary>
        /// 踩数。
        /// </summary>
        public virtual int DownCount { get; set; }

        /// <summary>
        /// 喜欢数。
        /// </summary>
        public virtual int FavoriteCount { get; set; }
    }
}
