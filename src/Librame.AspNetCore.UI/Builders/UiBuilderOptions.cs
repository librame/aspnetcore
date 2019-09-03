#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    /// <summary>
    /// 用户界面构建器选项。
    /// </summary>
    public class UiBuilderOptions : AbstractExtensionBuilderOptions
    {
        /// <summary>
        /// 激活视图键名。
        /// </summary>
        public string ActiveViewKey { get; set; }
            = "ActivePage";

        /// <summary>
        /// 有外部认证方案的键名。
        /// </summary>
        public string HasExternalAuthenticationSchemesKey { get; set; }
            = $"{typeof(UiBuilderOptions).Namespace}.HasExternalLogins";


        /// <summary>
        /// 安全。
        /// </summary>
        public SafetyOptions Safety { get; set; }
            = new SafetyOptions();
    }


    /// <summary>
    /// 安全选项。
    /// </summary>
    public class SafetyOptions
    {
        /// <summary>
        /// 登入区域相对路径。
        /// </summary>
        public string LoginAreaRelativePath { get; set; }
            = "/Account/Login";

        /// <summary>
        /// 登出区域相对路径。
        /// </summary>
        public string LogoutAreaRelativePath { get; set; }
            = "/Account/Logout";

        /// <summary>
        /// 禁止访问区域相对路径。
        /// </summary>
        public string AccessAreaDeniedRelativePath { get; set; }
            = "/Account/AccessDenied";

        /// <summary>
        /// 管理区域相对路径。
        /// </summary>
        public string ManageAreaRelativePath { get; set; }
            = "/Account/Logout";
    }
}
