#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard;
using System.Runtime.InteropServices;

namespace LibrameCore.Filtration
{
    /// <summary>
    /// 过滤选项。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class FiltrationOptions : ILibrameOptions
    {
        /// <summary>
        /// 键名。
        /// </summary>
        internal static readonly string Key = nameof(Filtration);

        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix = (Key + ":");


        /// <summary>
        /// 敏感词。
        /// </summary>
        public SensitiveWordOptions SensitiveWord { get; set; }
            = new SensitiveWordOptions();


        /// <summary>
        /// 静态化 HTML。
        /// </summary>
        public StaticalHtmlOptions StaticalHtml { get; set; }
            = new StaticalHtmlOptions();
    }


    /// <summary>
    /// 敏感词选项。
    /// </summary>
    public class SensitiveWordOptions
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix
            = (nameof(FiltrationOptions.SensitiveWord) + ":");


        #region Enabled

        /// <summary>
        /// 是否启用键。
        /// </summary>
        public static readonly string EnabledKey
            = KeyPrefix + nameof(Enabled);

        /// <summary>
        /// 默认启用。
        /// </summary>
        public static readonly bool DefaultEnabled = true;

        /// <summary>
        /// 是否启用。
        /// </summary>
        public bool Enabled { get; set; } = DefaultEnabled;

        #endregion


        #region Replacement

        /// <summary>
        /// 替换内容键。
        /// </summary>
        public static readonly string ReplacementKey
            = KeyPrefix + nameof(Replacement);

        /// <summary>
        /// 默认替换内容。
        /// </summary>
        public static readonly string DefaultReplacement = "【敏感词】";

        /// <summary>
        /// 替换内容。
        /// </summary>
        public string Replacement { get; set; } = DefaultReplacement;

        #endregion


        #region Words

        /// <summary>
        /// 单词集合键。
        /// </summary>
        public static readonly string WordsKey
            = KeyPrefix + nameof(Words);

        /// <summary>
        /// 默认单词集合。
        /// </summary>
        public static readonly string DefaultWords = "肉棒,被干,潮吹,吃精,大波,荡妇,荡女,龟头,李宏志,法轮,法伦,中南海,台独,港独,藏独,疆独,戒严,大跃进,文革,民主,独裁,枪支,弹药,炸药,大刀";

        /// <summary>
        /// 单词集合。
        /// </summary>
        public string Words { get; set; } = DefaultWords;

        #endregion

    }


    /// <summary>
    /// 静态化 HTML 选项。
    /// </summary>
    public class StaticalHtmlOptions
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix
            = (nameof(FiltrationOptions.StaticalHtml) + ":");


        #region Enabled

        /// <summary>
        /// 是否启用键。
        /// </summary>
        public static readonly string EnabledKey
            = KeyPrefix + nameof(Enabled);

        /// <summary>
        /// 默认启用。
        /// </summary>
        public static readonly bool DefaultEnabled = true;

        /// <summary>
        /// 是否启用。
        /// </summary>
        public bool Enabled { get; set; } = DefaultEnabled;

        #endregion


        #region AppendTimestamp

        /// <summary>
        /// 是否附加时间戳键。
        /// </summary>
        public static readonly string AppendTimestampKey
            = KeyPrefix + nameof(AppendTimestamp);

        /// <summary>
        /// 默认附加时间戳。
        /// </summary>
        public static readonly bool DefaultAppendTimestamp = true;

        /// <summary>
        /// 是否附加时间戳。
        /// </summary>
        public bool AppendTimestamp { get; set; } = DefaultAppendTimestamp;

        #endregion


        #region FolderName

        /// <summary>
        /// 替换文件夹名格式键。
        /// </summary>
        public static readonly string FolderNameKey
            = KeyPrefix + nameof(FolderName);

        /// <summary>
        /// 默认替换文件夹名格式。
        /// </summary>
        public static readonly string DefaultFolderName = "html";

        /// <summary>
        /// 替换文件夹名格式。
        /// </summary>
        public string FolderName { get; set; } = DefaultFolderName;

        #endregion


        #region FileNameFormat

        /// <summary>
        /// 替换文件名格式键。
        /// </summary>
        public static readonly string FileNameFormatKey
            = KeyPrefix + nameof(FileNameFormat);

        /// <summary>
        /// 默认替换文件名格式。
        /// </summary>
        public static readonly string DefaultFileNameFormat = "{controller}-{action}{id}{extension}";

        /// <summary>
        /// 替换文件名格式。
        /// </summary>
        public string FileNameFormat { get; set; } = DefaultFileNameFormat;

        #endregion

    }

}
