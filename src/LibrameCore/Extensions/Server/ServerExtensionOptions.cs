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
        /// 静态页。
        /// </summary>
        public StaticPageOptions StaticPage { get; set; }
            = new StaticPageOptions();


        #region Extensions

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
