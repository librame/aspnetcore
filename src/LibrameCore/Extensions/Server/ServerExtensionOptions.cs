#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Abstractions;

namespace LibrameCore.Extensions.Server
{
    /// <summary>
    /// 服务器扩展选项。
    /// </summary>
    public class ServerExtensionOptions : IExtensionOptions
    {
        /// <summary>
        /// 敏感词。
        /// </summary>
        public SensitiveWordOptions SensitiveWord { get; set; } = new SensitiveWordOptions();

        /// <summary>
        /// 静态页。
        /// </summary>
        public StaticPageOptions StaticPage { get; set; } = new StaticPageOptions();


        #region Extensions

        /// <summary>
        /// 敏感词选项。
        /// </summary>
        public class SensitiveWordOptions
        {
            /// <summary>
            /// 是否启用。
            /// </summary>
            public bool Enabled { get; set; } = true;

            /// <summary>
            /// 替换内容。
            /// </summary>
            public string Replacement { get; set; } = "【敏感词】";

            /// <summary>
            /// 单词集合。
            /// </summary>
            public string Words { get; set; }
                = "肉棒,被干,潮吹,吃精,大波,荡妇,荡女,龟头,李宏志,法轮,法伦,中南海,台独,港独,藏独,疆独,戒严,大跃进,文革,民主,独裁,枪支,弹药,炸药,大刀";
        }


        /// <summary>
        /// 静态页选项。
        /// </summary>
        public class StaticPageOptions
        {
            /// <summary>
            /// 是否启用。
            /// </summary>
            public bool Enabled { get; set; } = true;

            /// <summary>
            /// 是否附加时间戳。
            /// </summary>
            public bool AppendTimestamp { get; set; } = true;

            /// <summary>
            /// 替换文件夹名格式。
            /// </summary>
            public string FolderName { get; set; } = "html";

            /// <summary>
            /// 替换文件名格式。
            /// </summary>
            public string FileNameFormat { get; set; } = "{controller}-{action}{id}{extension}";
        }

        #endregion

    }
}
