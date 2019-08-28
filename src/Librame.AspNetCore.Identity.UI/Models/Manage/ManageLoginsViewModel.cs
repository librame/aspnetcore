#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Librame.AspNetCore.Identity.UI
{
    /// <summary>
    /// 管理登入视图模型。
    /// </summary>
    public class ManageLoginsViewModel
    {
        /// <summary>
        /// 当前登入信息列表。
        /// </summary>
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        /// <summary>
        /// 其他登入方法列表。
        /// </summary>
        public IList<AuthenticationScheme> OtherLogins { get; set; }
    }
}
