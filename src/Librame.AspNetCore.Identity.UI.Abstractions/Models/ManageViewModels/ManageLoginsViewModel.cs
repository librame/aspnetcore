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

namespace Librame.AspNetCore.Identity.UI.Models.ManageViewModels
{
    /// <summary>
    /// 管理登录集合视图模型。
    /// </summary>
    public class ManageLoginsViewModel
    {
        /// <summary>
        /// 当前登录信息列表。
        /// </summary>
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        /// <summary>
        /// 认证方案列表。
        /// </summary>
        public IList<AuthenticationScheme> OtherLogins { get; set; }
    }
}
