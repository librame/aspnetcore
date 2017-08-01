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
        /// 敏感词选项。
        /// </summary>
        public SensitiveWordOptions SensitiveWord { get; set; }
            = new SensitiveWordOptions();
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

}
