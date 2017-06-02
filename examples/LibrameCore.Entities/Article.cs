#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Entity.Descriptors;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibrameStandard.Entities
{
    /// <summary>
    /// 文章。
    /// </summary>
    [Description("文章")]
    public class Article : AbstractCreateDataIdDescriptor<int>
    {
        /// <summary>
        /// 标题。
        /// </summary>
        [DisplayName("标题")]
        public virtual string Title { get; set; }

        /// <summary>
        /// 内容。
        /// </summary>
        [DisplayName("内容")]
        [DataType(DataType.MultilineText)]
        public virtual string Descr { get; set; }
    }
}
