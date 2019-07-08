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
    public class PortalDbContextAccessor : PortalDbContextAccessor<PortalClaim, PortalCategory, PortalPane, PortalTag, PortalSource, PortalEditor, PortalSubject,
        int, int, int, string, int, int, int, string>
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
    /// <typeparam name="TClaimId">指定的声明标识类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
    /// <typeparam name="TPaneId">指定的窗格标识类型。</typeparam>
    /// <typeparam name="TTagId">指定的标签标识类型。</typeparam>
    /// <typeparam name="TSourceId">指定的来源标识类型。</typeparam>
    /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
    /// <typeparam name="TSubjectId">指定的专题标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    public class PortalDbContextAccessor<TClaim, TCategory, TPane, TTag, TSource, TEditor, TSubject,
        TClaimId, TCategoryId, TPaneId, TTagId, TSourceId, TEditorId, TSubjectId, TUserId>
        : PortalDbContextAccessor<TClaim, TCategory, TPane, PortalPaneClaim<int, TPaneId, TClaimId>, TTag, PortalTagClaim<string, TTagId, TClaimId>, TSource, TEditor, PortalEditorTitle<int, TEditorId>, TSubject, PortalSubjectBody<int, TSubjectId>, PortalSubjectClaim<int, TSubjectId, TClaimId>,
            TClaimId, TCategoryId, TPaneId, int, TTagId, string, TSourceId, TEditorId, int, TSubjectId, int, int, TUserId, DateTimeOffset>
        where TClaim : PortalClaim<TClaimId>
        where TCategory : PortalCategory<TCategoryId>
        where TPane : PortalPane<TPaneId, TCategoryId>
        where TTag : PortalTag<TTagId>
        where TSource : PortalSource<TSourceId, TCategoryId>
        where TEditor : PortalEditor<TEditorId, TUserId>
        where TSubject : PortalSubject<TSubjectId, TCategoryId, DateTimeOffset>
        where TClaimId : IEquatable<TClaimId>
        where TCategoryId : IEquatable<TCategoryId>
        where TPaneId : IEquatable<TPaneId>
        where TTagId : IEquatable<TTagId>
        where TSourceId : IEquatable<TSourceId>
        where TEditorId : IEquatable<TEditorId>
        where TSubjectId : IEquatable<TSubjectId>
        where TUserId : IEquatable<TUserId>
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
    public class PortalDbContextAccessor<TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim,
        TClaimId, TCategoryId, TPaneId, TPaneClaimId, TTagId, TTagClaimId, TSourceId, TEditorId, TEditorTitleId, TSubjectId, TSubjectBodyId, TSubjectClaimId, TUserId, TDateTime>
        : DbContextAccessor
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

            modelBuilder.ConfigurePortalEntities<TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim,
                TClaimId, TCategoryId, TPaneId, TPaneClaimId, TTagId, TTagClaimId, TSourceId, TEditorId, TEditorTitleId, TSubjectId, TSubjectBodyId, TSubjectClaimId, TUserId, TDateTime>(options);
        }
    }
}
