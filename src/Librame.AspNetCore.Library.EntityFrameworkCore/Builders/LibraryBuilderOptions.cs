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

namespace Librame.AspNetCore.Library
{
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 文库构建器选项。
    /// </summary>
    public class LibraryBuilderOptions : AbstractBuilderOptions, IBuilderOptions
    {

        #region Common

        /// <summary>
        /// 种类表。
        /// </summary>
        public Func<Type, ITableSchema> CategoryTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 编者表。
        /// </summary>
        public Func<Type, ITableSchema> EditorTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 编者头衔表。
        /// </summary>
        public Func<Type, ITableSchema> EditorTitleTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 来源表。
        /// </summary>
        public Func<Type, ITableSchema> SourceTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 标签表。
        /// </summary>
        public Func<Type, ITableSchema> TagTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 网链表。
        /// </summary>
        public Func<Type, ITableSchema> WeblinkTableFactory { get; set; }
            = type => type.AsTableSchema();

        #endregion


        #region Library

        /// <summary>
        /// 专题表。
        /// </summary>
        public Func<Type, ITableSchema> SubjectTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 专题主体表。
        /// </summary>
        public Func<Type, ITableSchema> SubjectBodyTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 文章表。
        /// </summary>
        public Func<Type, ITableSchema> ArticleTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 文章主体表。
        /// </summary>
        public Func<Type, ITableSchema> ArticleBodyTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 文集表。
        /// </summary>
        public Func<Type, ITableSchema> AnthologyTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 图片表。
        /// </summary>
        public Func<Type, ITableSchema> PictureTableFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 相册表。
        /// </summary>
        public Func<Type, ITableSchema> AlbumTableFactory { get; set; }
            = type => type.AsTableSchema();

        #endregion

    }
}
