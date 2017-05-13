#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Adaptation
{
    /// <summary>
    /// 适配器信息。
    /// </summary>
    public class AdapterInfo
    {
        /// <summary>
        /// 构造一个 <see cref="AdapterInfo"/> 实例。
        /// </summary>
        /// <param name="name">给定的架构名称。</param>
        /// <param name="author">给定的架构作者。</param>
        /// <param name="version">给定的发布版本。</param>
        /// <param name="pubdate">给定的发布日期。</param>
        public AdapterInfo(string name,
            string author = LibrameConstants.AUTHOR,
            string version = LibrameConstants.VERSION,
            string pubdate = LibrameConstants.DEFAULT_PUBDATE)
        {
            Name = name;
            Author = author;
            Version = version;
            Pubdate = pubdate;
        }


        /// <summary>
        /// 架构名称。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 架构作者。
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// 发布版本。
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// 发布日期。
        /// </summary>
        public string Pubdate { get; }
    }
}
