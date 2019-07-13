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

namespace Librame.AspNetCore.Portal
{
    using Extensions.Data;

    /// <summary>
    /// 门户构建器选项。
    /// </summary>
    public class PortalBuilderOptions : DataBuilderOptionsBase<PortalTableSchemaOptions>
    {
    }


    /// <summary>
    /// 表架构选项集合。
    /// </summary>
    public class PortalTableSchemaOptions : ITableSchemaOptions
    {
        /// <summary>
        /// 声明工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> ClaimFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 分类工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> CategoryFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 窗格工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> PaneFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 窗格声明工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> PaneClaimFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 标签工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> TagFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 标签声明工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> TagClaimFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 来源工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> SourceFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 编者工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> EditorFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 编者头衔工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> EditorTitleFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 专题工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> SubjectFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 专题主体工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> SubjectBodyFactory { get; set; }
            = type => type.AsTableSchema();

        /// <summary>
        /// 专题声明工厂方法。
        /// </summary>
        public Func<Type, ITableSchema> SubjectClaimFactory { get; set; }
            = type => type.AsTableSchema();
    }
}
