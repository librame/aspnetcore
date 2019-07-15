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

namespace Librame.AspNetCore.Portal
{
    using Extensions.Data;

    /// <summary>
    /// 门户数据库上下文访问器。
    /// </summary>
    public class PortalDbContextAccessor : PortalDbContextAccessor<PortalClaim, PortalCategory, PortalPane
        , PortalTag, PortalSource, PortalEditor, PortalSubject, string, int>
    {
        /// <summary>
        /// 构造一个 <see cref="PortalDbContextAccessor"/> 实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public PortalDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }
    }


    /// <summary>
    /// 门户数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TClaim">指定的声明类型。</typeparam>
    /// <typeparam name="TCategory">指定的分类类型。</typeparam>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TTag">指定的标签类型。</typeparam>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    public class PortalDbContextAccessor<TClaim, TCategory, TPane, TTag, TSource, TEditor, TSubject, TGenId, TIncremId>
        : PortalDbContextAccessor<TClaim, TCategory, TPane, PortalPaneClaim<TIncremId>
            , TTag, PortalTagClaim<TGenId, TIncremId>, TSource, TEditor, PortalEditorTitle<TIncremId>
            , TSubject, PortalSubjectBody<TIncremId>, PortalSubjectClaim<TIncremId>, TGenId, TIncremId>
        where TClaim : PortalClaim<TIncremId>
        where TCategory : PortalCategory<TIncremId>
        where TPane : PortalPane<TIncremId>
        where TTag : PortalTag<TGenId>
        where TSource : PortalSource<TIncremId>
        where TEditor : PortalEditor<TIncremId, TGenId>
        where TSubject : PortalSubject<TIncremId>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 构造一个门户数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public PortalDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }
    }


    /// <summary>
    /// 门户数据库上下文访问器。
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
    public class PortalDbContextAccessor<TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim
        , TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim, TGenId, TIncremId> : DbContextAccessor
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
        /// <summary>
        /// 构造一个门户数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public PortalDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }


        /// <summary>
        /// 声明数据集。
        /// </summary>
        public DbSet<TClaim> Claims { get; set; }

        /// <summary>
        /// 分类数据集。
        /// </summary>
        public DbSet<TCategory> Categories { get; set; }

        /// <summary>
        /// 窗格数据集。
        /// </summary>
        public DbSet<TPane> Panes { get; set; }

        /// <summary>
        /// 窗格声明数据集。
        /// </summary>
        public DbSet<TPaneClaim> PaneClaims { get; set; }

        /// <summary>
        /// 标签数据集。
        /// </summary>
        public DbSet<TTag> Tags { get; set; }

        /// <summary>
        /// 标签声明数据集。
        /// </summary>
        public DbSet<TTagClaim> TagClaims { get; set; }

        /// <summary>
        /// 来源数据集。
        /// </summary>
        public DbSet<TSource> Sources { get; set; }

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
        /// 专题主体数据集。
        /// </summary>
        public DbSet<TSubjectBody> SubjectBodies { get; set; }

        /// <summary>
        /// 专题声明数据集。
        /// </summary>
        public DbSet<TSubjectClaim> SubjectClaims { get; set; }


        /// <summary>
        /// 开始创建模型。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var options = ServiceProvider.GetRequiredService<IOptions<PortalBuilderOptions>>().Value;

            modelBuilder.ConfigurePortalEntities<TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource,
                TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim, TGenId, TIncremId>(options);
        }
    }
}
