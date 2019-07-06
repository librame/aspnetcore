﻿#region License

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
    /// 外部登入视图资源。
    /// </summary>
    public class ExternalLoginConfirmationViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 信息。
        /// </summary>
        public string Info { get; set; }


        #region Model

        /// <summary>
        /// 称呼。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 邮箱。
        /// </summary>
        public string Email { get; set; }

        #endregion

    }
}