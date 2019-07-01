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
using System;

namespace Librame.AspNetCore.Library
{
    /// <summary>
    /// 内部访问器模型构建器静态扩展。
    /// </summary>
    internal static class InternalAccessorModelBuilderExtensions
    {
        /// <summary>
        /// 配置文章实体集合。
        /// </summary>
        /// <typeparam name="TSubject">指定的专题类型。</typeparam>
        /// <typeparam name="TSubjectId">指定的专题标识类型。</typeparam>
        /// <typeparam name="TSubjectArticle">指定的专题文章类型。</typeparam>
        /// <typeparam name="TArticle">指定的文章类型。</typeparam>
        /// <typeparam name="TArticleId">指定的文章标识类型。</typeparam>
        /// <typeparam name="TArticleBody">指定的文章主体类型。</typeparam>
        /// <typeparam name="TArticleBodyId">指定的文章主体标识类型。</typeparam>
        /// <typeparam name="TCategory">指定的种类类型。</typeparam>
        /// <typeparam name="TCategoryId">指定的种类标识类型。</typeparam>
        /// <typeparam name="TEditor">指定的编者类型。</typeparam>
        /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="LibraryBuilderOptions"/>。</param>
        public static void ConfigureArticleEntities<TSubject, TSubjectId, TSubjectArticle, TArticle, TArticleId, TArticleBody, TArticleBodyId, TCategory, TCategoryId, TEditor, TEditorId>
            (this ModelBuilder modelBuilder, LibraryBuilderOptions options)
            where TSubject : LibrarySubject<TSubjectId, TCategoryId>
            where TSubjectId : IEquatable<TSubjectId>
            where TSubjectArticle : LibrarySubjectBody<TSubjectId, TArticleId>
            where TArticle : LibraryArticle<TArticleId, TCategoryId, TEditorId>
            where TArticleId : IEquatable<TArticleId>
            where TArticleBody : LibraryArticleBody<TArticleBodyId, TArticleId>
            where TArticleBodyId : IEquatable<TArticleBodyId>
            where TCategory : LibraryCategory<TCategoryId>
            where TCategoryId : IEquatable<TCategoryId>
            where TEditor : class
            where TEditorId : IEquatable<TEditorId>
        {
            modelBuilder.Entity<TSubject>(b =>
            {
                b.ToTable(options.SubjectTableFactory?.Invoke(typeof(TSubject)));

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.Title).HasName("SubjectTitleIndex").IsUnique();

                b.Property(p => p.Tags).HasMaxLength(100);
                b.Property(p => p.Title).HasMaxLength(200);
                b.Property(p => p.Subtitle).HasMaxLength(256);

                b.HasMany<TSubjectArticle>().WithOne().HasForeignKey(fk => fk.SubjectId).IsRequired();
            });

            modelBuilder.Entity<TSubjectArticle>(b =>
            {
                b.ToTable(options.SubjectBodyTableFactory?.Invoke(typeof(TSubjectArticle)));

                b.HasKey(k => new { k.SubjectId, k.ArticleId });
            });

            modelBuilder.Entity<TArticle>(b =>
            {
                b.ToTable(options.ArticleTableFactory?.Invoke(typeof(TArticle)));

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.Title).HasName("ArticleTitleIndex").IsUnique();

                b.Property(p => p.Tags).HasMaxLength(100);
                b.Property(p => p.Title).HasMaxLength(200);
                b.Property(p => p.Subtitle).HasMaxLength(256);

                b.HasMany<TArticleBody>().WithOne().HasForeignKey(fk => fk.ArticleId).IsRequired();
                b.HasMany<TSubjectArticle>().WithOne().HasForeignKey(fk => fk.ArticleId).IsRequired();
            });

            modelBuilder.Entity<TArticleBody>(b =>
            {
                b.ToTable(options.ArticleBodyTableFactory?.Invoke(typeof(TArticleBody)));

                b.HasKey(k => k.Id);

                b.Property(p => p.Body);
            });

            modelBuilder.Entity<TCategory>(b =>
            {
                b.HasMany<TSubject>().WithOne().HasForeignKey(fk => fk.CategoryId).IsRequired();
                b.HasMany<TArticle>().WithOne().HasForeignKey(fk => fk.CategoryId).IsRequired();
            });

            modelBuilder.Entity<TEditor>(b =>
            {
                b.HasMany<TArticle>().WithOne().HasForeignKey(fk => fk.EditorId).IsRequired();
            });
        }

        /// <summary>
        /// 配置公共实体集合。
        /// </summary>
        /// <typeparam name="TCategory">指定的种类类型。</typeparam>
        /// <typeparam name="TCategoryId">指定的种类标识类型。</typeparam>
        /// <typeparam name="TEditor">指定的编者类型。</typeparam>
        /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
        /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
        /// <typeparam name="TEditorTitle">指定的编者头衔类型。</typeparam>
        /// <typeparam name="TEditorTitleId">指定的编者头衔标识类型。</typeparam>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="LibraryBuilderOptions"/>。</param>
        public static void ConfigureCommonEntities<TCategory, TCategoryId, TEditor, TEditorId, TUserId, TEditorTitle, TEditorTitleId>
            (this ModelBuilder modelBuilder, LibraryBuilderOptions options)
            where TCategory : LibraryCategory<TCategoryId>
            where TCategoryId : IEquatable<TCategoryId>
            where TEditor : LibraryEditor<TEditorId, TUserId>
            where TEditorId : IEquatable<TEditorId>
            where TUserId : IEquatable<TUserId>
            where TEditorTitle : LibraryEditorTitle<TEditorTitleId, TEditorId>
            where TEditorTitleId : IEquatable<TEditorTitleId>
        {
            modelBuilder.Entity<TCategory>(b =>
            {
                b.ToTable(options.CategoryTableFactory?.Invoke(typeof(TCategory)));

                b.HasKey(k => k.Id);
                
                b.HasIndex(i => i.Name).HasName("CategoryNameIndex").IsUnique();

                b.Property(p => p.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<TEditor>(b =>
            {
                b.ToTable(options.EditorTableFactory?.Invoke(typeof(TEditor)));

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.Name).HasName("EditorNameIndex").IsUnique();

                b.Property(p => p.Name).HasMaxLength(100);

                b.HasMany<TEditorTitle>().WithOne().HasForeignKey(fk => fk.EditorId).IsRequired();
            });

            modelBuilder.Entity<TEditorTitle>(b =>
            {
                b.ToTable(options.EditorTitleTableFactory?.Invoke(typeof(TEditorTitle)));

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.Name).HasName("EditorTitleNameIndex").IsUnique();

                b.Property(p => p.Name).HasMaxLength(100);
            });
        }

    }
}
