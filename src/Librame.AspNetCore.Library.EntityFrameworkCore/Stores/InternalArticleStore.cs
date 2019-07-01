#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Library
{
    using Extensions.Data;

    /// <summary>
    /// 内部文章存储。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TSubjectArticle">指定的专题文章类型。</typeparam>
    /// <typeparam name="TArticle">指定的文章类型。</typeparam>
    /// <typeparam name="TArticleBody">指定的文章主体类型。</typeparam>
    internal class InternalArticleStore<TAccessor, TSubject, TSubjectArticle, TArticle, TArticleBody>
        : AbstractLibraryStore<TAccessor, TSubject, TSubjectArticle, TArticle, TArticleBody>
        where TAccessor : IAccessor
        where TSubject : class
        where TSubjectArticle : class
        where TArticle : class
        where TArticleBody : class
    {
        /// <summary>
        /// 构造一个抽象文章存储实例。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public InternalArticleStore(IAccessor accessor)
            : base(accessor)
        {
        }

    }
}
