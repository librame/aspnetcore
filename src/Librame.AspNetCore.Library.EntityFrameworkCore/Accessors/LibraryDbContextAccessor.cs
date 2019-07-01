#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Librame.AspNetCore.Library
{
    using Extensions.Data;

    /// <summary>
    /// 文库数据库上下文访问器。
    /// </summary>
    public class LibraryDbContextAccessor : LibraryDbContextAccessor<LibrarySubject, string, LibraryArticle, string>
    {
        /// <summary>
        /// 构造一个 <see cref="LibraryDbContextAccessor"/> 实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public LibraryDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }
    }


    /// <summary>
    /// 文库数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TSubjectId">指定的专题标识类型。</typeparam>
    /// <typeparam name="TArticle">指定的文章类型。</typeparam>
    /// <typeparam name="TArticleId">指定的文章标识类型。</typeparam>
    public class LibraryDbContextAccessor<TSubject, TSubjectId, TArticle, TArticleId>
        : LibraryDbContextAccessor<LibraryCategory, int, LibraryEditor, string, string, LibraryEditorTitle, int,
            TSubject, TSubjectId, LibrarySubjectBody<TSubjectId, TArticleId>, TArticle, TArticleId, LibraryArticleBody<string, TArticleId>, string>
        where TSubject : LibrarySubject<TSubjectId, int>
        where TSubjectId : IEquatable<TSubjectId>
        where TArticle : LibraryArticle<TArticleId, int, string>
        where TArticleId : IEquatable<TArticleId>
    {
        /// <summary>
        /// 构造一个文库数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public LibraryDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }
    }


    /// <summary>
    /// 文库数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TCategory">指定的种类类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的种类标识类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TEditorTitle">指定的编者头衔类型。</typeparam>
    /// <typeparam name="TEditorTitleId">指定的编者头衔标识类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TSubjectId">指定的专题标识类型。</typeparam>
    /// <typeparam name="TSubjectArticle">指定的专题文章类型。</typeparam>
    /// <typeparam name="TArticle">指定的文章类型。</typeparam>
    /// <typeparam name="TArticleId">指定的文章标识类型。</typeparam>
    /// <typeparam name="TArticleBody">指定的文章主体类型。</typeparam>
    /// <typeparam name="TArticleBodyId">指定的文章主体标识类型。</typeparam>
    public class LibraryDbContextAccessor<TCategory, TCategoryId, TEditor, TEditorId, TUserId, TEditorTitle, TEditorTitleId,
        TSubject, TSubjectId, TSubjectArticle, TArticle, TArticleId, TArticleBody, TArticleBodyId> : DbContextAccessor
        where TCategory : LibraryCategory<TCategoryId>
        where TCategoryId : IEquatable<TCategoryId>
        where TEditor : LibraryEditor<TEditorId, TUserId>
        where TEditorId : IEquatable<TEditorId>
        where TUserId : IEquatable<TUserId>
        where TEditorTitle : LibraryEditorTitle<TEditorTitleId, TEditorId>
        where TEditorTitleId : IEquatable<TEditorTitleId>
        where TSubject : LibrarySubject<TSubjectId, TCategoryId>
        where TSubjectId : IEquatable<TSubjectId>
        where TSubjectArticle : LibrarySubjectBody<TSubjectId, TArticleId>
        where TArticle : LibraryArticle<TArticleId, TCategoryId, TEditorId>
        where TArticleId : IEquatable<TArticleId>
        where TArticleBody : LibraryArticleBody<TArticleBodyId, TArticleId>
        where TArticleBodyId : IEquatable<TArticleBodyId>
    {
        /// <summary>
        /// 构造一个文库数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public LibraryDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }


        /// <summary>
        /// 种类数据集。
        /// </summary>
        public DbSet<TCategory> Categories { get; set; }

        /// <summary>
        /// 编者数据集。
        /// </summary>
        public DbSet<TEditor> Editors { get; set; }

        /// <summary>
        /// 编者头衔数据集。
        /// </summary>
        public DbSet<TEditorTitle> EditorTitles { get; set; }


        /// <summary>
        /// 专题数据集。
        /// </summary>
        public DbSet<TSubject> Subjects { get; set; }

        /// <summary>
        /// 专题文章数据集。
        /// </summary>
        public DbSet<TSubjectArticle> SubjectArticles { get; set; }

        /// <summary>
        /// 文章数据集。
        /// </summary>
        public DbSet<TArticle> Articles { get; set; }

        /// <summary>
        /// 文章主体数据集。
        /// </summary>
        public DbSet<TArticleBody> ArticleBodies { get; set; }


        /// <summary>
        /// 开始创建模型。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var options = ServiceProvider.GetRequiredService<IOptions<LibraryBuilderOptions>>().Value;

            modelBuilder.ConfigureCommonEntities<TCategory, TCategoryId, TEditor, TEditorId, TUserId,
                TEditorTitle, TEditorTitleId>(options);

            modelBuilder.ConfigureArticleEntities<TSubject, TSubjectId, TSubjectArticle,
                TArticle, TArticleId, TArticleBody, TArticleBodyId,
                TCategory, TCategoryId, TEditor, TEditorId>(options);
        }
    }
}
