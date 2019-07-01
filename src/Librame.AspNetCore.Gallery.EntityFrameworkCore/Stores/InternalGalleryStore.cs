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
    /// 内部图库存储。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TCategory">指定的种类类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的种类标识类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TEditorTitle">指定的编者头衔类型。</typeparam>
    /// <typeparam name="TEditorTitleId">指定的编者头衔标识类型。</typeparam>
    internal class InternalGalleryStore<TAccessor, TCategory, TCategoryId, TEditor, TEditorId, TUserId, TEditorTitle, TEditorTitleId>
        : AbstractGalleryStore<TAccessor, TCategory, TCategoryId, TEditor, TEditorId, TUserId, TEditorTitle, TEditorTitleId>
        where TAccessor : IAccessor
        where TCategory : GalleryCategory<TCategoryId>
        where TEditor : GalleryEditor<TEditorId, TUserId>
        where TEditorTitle : GalleryEditor<TEditorTitleId, TUserId>
        where TCategoryId : IEquatable<TCategoryId>
        where TEditorId : IEquatable<TEditorId>
        where TUserId : IEquatable<TUserId>
        where TEditorTitleId : IEquatable<TEditorTitleId>
    {
        /// <summary>
        /// 构造一个 <see cref="InternalGalleryStore{TAccessor, TCategory, TCategoryId, TEditor, TEditorId, TUserId, TEditorTitle, TEditorTitleId}"/> 实例。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public InternalGalleryStore(IAccessor accessor)
            : base(accessor)
        {
        }

    }
}
