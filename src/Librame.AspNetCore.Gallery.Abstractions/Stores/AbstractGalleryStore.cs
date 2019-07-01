#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Gallery
{
    using Extensions.Data;

    /// <summary>
    /// 抽象图库存储。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TSubjectArticle">指定的专题文章类型。</typeparam>
    /// <typeparam name="TArticle">指定的文章类型。</typeparam>
    /// <typeparam name="TArticleBody">指定的文章主体类型。</typeparam>
    public abstract class AbstractGalleryStore<TAccessor, TSubject, TSubjectArticle, TArticle, TArticleBody> : AbstractBaseStore<TAccessor>,
        IArticleStore<TAccessor, TSubject, TSubjectArticle, TArticle, TArticleBody>
        where TAccessor : IAccessor
        where TSubject : class
        where TSubjectArticle : class
        where TArticle : class
        where TArticleBody : class
    {
        /// <summary>
        /// 构造一个抽象图库存储实例。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public AbstractGalleryStore(IAccessor accessor)
            : base(accessor)
        {
        }

    }
}
