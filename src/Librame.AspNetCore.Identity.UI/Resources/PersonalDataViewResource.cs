#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.UI
{
    /// <summary>
    /// 个人数据视图资源。
    /// </summary>
    public class PersonalDataViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 下载按钮文本。
        /// </summary>
        public string DownloadButtonText { get; set; }

        /// <summary>
        /// 信息。
        /// </summary>
        public string Info { get; set; }
    }
}
