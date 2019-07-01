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
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 门户编者头衔。
    /// </summary>
    public class PortalEditorTitle : PortalEditorTitle<int, string>
    {
        /// <summary>
        /// 构造一个 <see cref="PortalEditorTitle"/> 实例。
        /// </summary>
        public PortalEditorTitle()
            : base()
        {
            // 默认使用空标识符，新增推荐使用服务注入
            EditorId = GuIdentifier.Empty;
        }
    }


    /// <summary>
    /// 门户编者头衔。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
    public class PortalEditorTitle<TId, TEditorId> : AbstractEntity<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 编者标识。
        /// </summary>
        public virtual TEditorId EditorId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public virtual string Name { get; set; }
    }
}
