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

namespace Librame.AspNetCore.Portal
{
    using Extensions.Data;

    /// <summary>
    /// 内部访问器模型构建器静态扩展。
    /// </summary>
    internal static class InternalAccessorModelBuilderExtensions
    {
        /// <summary>
        /// 配置公共实体集合。
        /// </summary>
        /// <typeparam name="TClaim">指定的声明类型。</typeparam>
        /// <typeparam name="TCategory">指定的分类类型。</typeparam>
        /// <typeparam name="TPane">指定的窗格类型。</typeparam>
        /// <typeparam name="TPaneClaim">指定的窗格声明类型。</typeparam>
        /// <typeparam name="TTag">指定的标签类型。</typeparam>
        /// <typeparam name="TTagClaim">指定的标签声明类型。</typeparam>
        /// <typeparam name="TSource">指定的来源类型。</typeparam>
        /// <typeparam name="TEditor">指定的编者类型。</typeparam>
        /// <typeparam name="TEditorTitle">指定的编者头衔类型。</typeparam>
        /// <typeparam name="TSubject">指定的专题类型。</typeparam>
        /// <typeparam name="TSubjectBody">指定的专题主体类型。</typeparam>
        /// <typeparam name="TSubjectClaim">指定的专题声明类型。</typeparam>
        /// <typeparam name="TClaimId">指定的声明标识类型。</typeparam>
        /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
        /// <typeparam name="TPaneId">指定的窗格标识类型。</typeparam>
        /// <typeparam name="TPaneClaimId">指定的窗格声明标识类型。</typeparam>
        /// <typeparam name="TTagId">指定的标签标识类型。</typeparam>
        /// <typeparam name="TTagClaimId">指定的标签声明标识类型。</typeparam>
        /// <typeparam name="TSourceId">指定的来源标识类型。</typeparam>
        /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
        /// <typeparam name="TEditorTitleId">指定的编者头衔标识类型。</typeparam>
        /// <typeparam name="TSubjectId">指定的专题标识类型。</typeparam>
        /// <typeparam name="TSubjectBodyId">指定的专题主体标识类型。</typeparam>
        /// <typeparam name="TSubjectClaimId">指定的专题声明标识类型。</typeparam>
        /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
        /// <typeparam name="TDateTime">指定的日期与时间类型（提供对 DateTime 或 DateTimeOffset 的支持）。</typeparam>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="PortalBuilderOptions"/>。</param>
        public static void ConfigurePortalEntities<TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim,
            TClaimId, TCategoryId, TPaneId, TPaneClaimId, TTagId, TTagClaimId, TSourceId, TEditorId, TEditorTitleId, TSubjectId, TSubjectBodyId, TSubjectClaimId, TUserId, TDateTime>
            (this ModelBuilder modelBuilder, PortalBuilderOptions options)
            where TClaim : PortalClaim<TClaimId>
            where TCategory : PortalCategory<TCategoryId>
            where TPane : PortalPane<TPaneId, TCategoryId>
            where TPaneClaim : PortalPaneClaim<TPaneClaimId, TPaneId, TClaimId>
            where TTag : PortalTag<TTagId>
            where TTagClaim : PortalTagClaim<TTagClaimId, TTagId, TClaimId>
            where TSource : PortalSource<TSourceId, TCategoryId>
            where TEditor : PortalEditor<TEditorId, TUserId>
            where TEditorTitle : PortalEditorTitle<TEditorTitleId, TEditorId>
            where TSubject : PortalSubject<TSubjectId, TCategoryId, TDateTime>
            where TSubjectBody : PortalSubjectBody<TSubjectBodyId, TSubjectId>
            where TSubjectClaim : PortalSubjectClaim<TSubjectClaimId, TSubjectId, TClaimId>
            where TClaimId : IEquatable<TClaimId>
            where TCategoryId : IEquatable<TCategoryId>
            where TPaneId : IEquatable<TPaneId>
            where TPaneClaimId : IEquatable<TPaneClaimId>
            where TTagId : IEquatable<TTagId>
            where TTagClaimId : IEquatable<TTagClaimId>
            where TSourceId : IEquatable<TSourceId>
            where TEditorId : IEquatable<TEditorId>
            where TEditorTitleId : IEquatable<TEditorTitleId>
            where TSubjectId : IEquatable<TSubjectId>
            where TSubjectBodyId : IEquatable<TSubjectBodyId>
            where TSubjectClaimId : IEquatable<TSubjectClaimId>
            where TUserId : IEquatable<TUserId>
            where TDateTime : struct
        {
            modelBuilder.Entity<TClaim>(b =>
            {
                b.ToTable(options.TableSchemas.ClaimFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.Type, i.Model }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Type).HasMaxLength(100);
                b.Property(p => p.Model).HasMaxLength(200);
                b.Property(p => p.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<TCategory>(b =>
            {
                b.ToTable(options.TableSchemas.CategoryFactory);

                b.HasKey(k => k.Id);
                
                b.HasIndex(i => new { i.ParentId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<TPane>(b =>
            {
                b.ToTable(options.TableSchemas.PaneFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.CategoryId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(100);
                b.Property(p => p.Path).HasMaxLength(200);

                //b.HasMany<TPaneClaim>().WithOne().HasForeignKey(fk => fk.PaneId).IsRequired();
            });
            modelBuilder.Entity<TPaneClaim>(b =>
            {
                b.ToTable(options.TableSchemas.PaneClaimFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.PaneId, i.ClaimId, i.AssocId }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.AssocId).HasMaxLength(100);
            });

            modelBuilder.Entity<TTag>(b =>
            {
                b.ToTable(options.TableSchemas.TagFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.Name).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(50);

                //b.HasMany<TTagClaim>().WithOne().HasForeignKey(fk => fk.TagId).IsRequired();
            });
            modelBuilder.Entity<TTagClaim>(b =>
            {
                b.ToTable(options.TableSchemas.TagClaimFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.TagId, i.ClaimId }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TSource>(b =>
            {
                b.ToTable(options.TableSchemas.SourceFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.CategoryId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(100);
                b.Property(p => p.Logo).HasMaxLength(100);
                b.Property(p => p.Link).HasMaxLength(100);
                b.Property(p => p.Descr).HasMaxLength(200);
            });

            modelBuilder.Entity<TEditor>(b =>
            {
                b.ToTable(options.TableSchemas.EditorFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.UserId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedNever();
                b.Property(p => p.Name).HasMaxLength(100);

                //b.HasMany<TEditorTitle>().WithOne().HasForeignKey(fk => fk.EditorId).IsRequired();
            });
            modelBuilder.Entity<TEditorTitle>(b =>
            {
                b.ToTable(options.TableSchemas.EditorTitleFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.EditorId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<TSubject>(b =>
            {
                b.ToTable(options.TableSchemas.SubjectFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.CategoryId, i.Title }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedNever();
                b.Property(p => p.PublishLink).HasMaxLength(100);
                b.Property(p => p.Title).HasMaxLength(100);
                b.Property(p => p.Subtitle).HasMaxLength(100);
                b.Property(p => p.Tags).HasMaxLength(100);

                //b.HasMany<TSubjectClaim>().WithOne().HasForeignKey(fk => fk.SubjectId).IsRequired();
            });
            modelBuilder.Entity<TSubjectBody>(b =>
            {
                b.ToTable(options.TableSchemas.SubjectBodyFactory);

                b.HasKey(k => k.Id);

                //b.HasIndex(i => new { i.SubjectId, i.Body }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedNever();
                b.Property(p => p.Body).HasMaxLength(4000);
            });
            modelBuilder.Entity<TSubjectClaim>(b =>
            {
                b.ToTable(options.TableSchemas.SubjectClaimFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.SubjectId, i.ClaimId, i.AssocId }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.AssocId).HasMaxLength(100);
            });
        }

    }
}
