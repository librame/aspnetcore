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
    /// 生成恢复码视图资源。
    /// </summary>
    public class GenerateRecoveryCodesViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 备注。
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 信息。
        /// </summary>
        public string Info { get; set; }
    }
}
