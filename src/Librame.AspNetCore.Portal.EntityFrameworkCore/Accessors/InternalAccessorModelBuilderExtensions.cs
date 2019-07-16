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
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="PortalBuilderOptions"/>。</param>
        public static void ConfigurePortalEntities<TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource,
            TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim, TGenId, TIncremId>
            (this ModelBuilder modelBuilder, PortalBuilderOptions options)
            where TClaim : PortalClaim<TIncremId>
            where TCategory : PortalCategory<TIncremId>
            where TPane : PortalPane<TIncremId>
            where TPaneClaim : PortalPaneClaim<TIncremId>
            where TTag : PortalTag<TGenId>
            where TTagClaim : PortalTagClaim<TGenId, TIncremId>
            where TSource : PortalSource<TIncremId>
            where TEditor : PortalEditor<TIncremId, TGenId>
            where TEditorTitle : PortalEditorTitle<TIncremId>
            where TSubject : PortalSubject<TIncremId>
            where TSubjectBody : PortalSubjectBody<TIncremId>
            where TSubjectClaim : PortalSubjectClaim<TIncremId>
            where TGenId : IEquatable<TGenId>
            where TIncremId : IEquatable<TIncremId>
        {
            var mapRelationship = options.Stores?.MapRelationship ?? true;
            var maxKeyLength = options.Stores?.MaxLengthForProperties ?? 0;

            modelBuilder.Entity<TClaim>(b =>
            {
                b.ToTable(options.TableSchemas.ClaimFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.Type, i.Model }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Type).HasMaxLength(256);
                b.Property(p => p.Model).HasMaxLength(256);

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.Title).HasMaxLength(maxKeyLength);
                }

                if (mapRelationship)
                {
                    b.HasMany<TPaneClaim>().WithOne().HasForeignKey(fk => fk.ClaimId).IsRequired();
                    b.HasMany<TTagClaim>().WithOne().HasForeignKey(fk => fk.ClaimId).IsRequired();
                    b.HasMany<TSubjectClaim>().WithOne().HasForeignKey(fk => fk.ClaimId).IsRequired();
                }
            });

            modelBuilder.Entity<TCategory>(b =>
            {
                b.ToTable(options.TableSchemas.CategoryFactory);

                b.HasKey(k => k.Id);
                
                b.HasIndex(i => new { i.ParentId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(256);

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.Descr).HasMaxLength(maxKeyLength);
                }

                if (mapRelationship)
                {
                    b.HasMany<TPane>().WithOne().HasForeignKey(fk => fk.CategoryId).IsRequired();
                    b.HasMany<TSource>().WithOne().HasForeignKey(fk => fk.CategoryId).IsRequired();
                    b.HasMany<TSubject>().WithOne().HasForeignKey(fk => fk.CategoryId).IsRequired();
                }
            });

            modelBuilder.Entity<TPane>(b =>
            {
                b.ToTable(options.TableSchemas.PaneFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.CategoryId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(256);

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.Path).HasMaxLength(maxKeyLength);
                }

                if (mapRelationship)
                {
                    b.HasMany<TPaneClaim>().WithOne().HasForeignKey(fk => fk.PaneId).IsRequired();
                }
            });
            modelBuilder.Entity<TPaneClaim>(b =>
            {
                b.ToTable(options.TableSchemas.PaneClaimFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.PaneId, i.ClaimId, i.AssocId }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.AssocId).HasMaxLength(256);
            });

            modelBuilder.Entity<TTag>(b =>
            {
                b.ToTable(options.TableSchemas.TagFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.Name).HasName().IsUnique();

                b.Property(p => p.Id).HasMaxLength(256);
                b.Property(p => p.Name).HasMaxLength(256);

                if (mapRelationship)
                {
                    b.HasMany<TTagClaim>().WithOne().HasForeignKey(fk => fk.TagId).IsRequired();
                }
            });
            modelBuilder.Entity<TTagClaim>(b =>
            {
                b.ToTable(options.TableSchemas.TagClaimFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.TagId, i.ClaimId }).HasName().IsUnique();

                b.Property(p => p.TagId).HasMaxLength(256);
            });

            modelBuilder.Entity<TSource>(b =>
            {
                b.ToTable(options.TableSchemas.SourceFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.CategoryId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(256);

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.Logo).HasMaxLength(maxKeyLength);
                    b.Property(p => p.Link).HasMaxLength(maxKeyLength);
                    b.Property(p => p.Descr).HasMaxLength(maxKeyLength);
                }
            });

            modelBuilder.Entity<TEditor>(b =>
            {
                b.ToTable(options.TableSchemas.EditorFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.UserId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.UserId).HasMaxLength(256);
                b.Property(p => p.Name).HasMaxLength(256);

                if (mapRelationship)
                {
                    b.HasMany<TEditorTitle>().WithOne().HasForeignKey(fk => fk.EditorId).IsRequired();
                }
            });
            modelBuilder.Entity<TEditorTitle>(b =>
            {
                b.ToTable(options.TableSchemas.EditorTitleFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.EditorId, i.Name }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<TSubject>(b =>
            {
                b.ToTable(options.TableSchemas.SubjectFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.CategoryId, i.Title }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Title).HasMaxLength(256);

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.PublishLink).HasMaxLength(maxKeyLength);
                    b.Property(p => p.Subtitle).HasMaxLength(maxKeyLength);
                    b.Property(p => p.Tags).HasMaxLength(maxKeyLength);
                }

                if (mapRelationship)
                {
                    b.HasMany<TSubjectBody>().WithOne().HasForeignKey(fk => fk.SubjectId).IsRequired();
                    b.HasMany<TSubjectClaim>().WithOne().HasForeignKey(fk => fk.SubjectId).IsRequired();
                }
            });
            modelBuilder.Entity<TSubjectBody>(b =>
            {
                b.ToTable(options.TableSchemas.SubjectBodyFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.SubjectId, i.TextHash }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.TextHash).HasMaxLength(256);
                b.Property(p => p.Text).HasMaxLength(4000);
            });
            modelBuilder.Entity<TSubjectClaim>(b =>
            {
                b.ToTable(options.TableSchemas.SubjectClaimFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.SubjectId, i.ClaimId, i.AssocId }).HasName().IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.AssocId).HasMaxLength(256);
            });
        }

    }
}
