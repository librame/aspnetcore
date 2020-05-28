#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;

namespace Librame.AspNetCore.Web.Builders
{
    using AspNetCore.Web.Options;
    using Extensions.Core.Builders;

    /// <summary>
    /// Web 构建器选项。
    /// </summary>
    public class WebBuilderOptions : IExtensionBuilderOptions
    {
        /// <summary>
        /// 激活视图键名。
        /// </summary>
        public string ActiveViewKey { get; set; }
            = "ActiveView";

        /// <summary>
        /// 默认用户头像路径。
        /// </summary>
        public string DefaultUserPortraitPath { get; set; }
            = "/manage/img/profile.jpg";

        /// <summary>
        /// 有外部认证方案的键名。
        /// </summary>
        public string HasExternalAuthenticationSchemesKey { get; set; }
            = $"{typeof(WebBuilderOptions).Namespace}.HasExternalLogins";

        /// <summary>
        /// 登陆栏项目名称。
        /// </summary>
        public string LoginbarProjectName { get; set; }
            = "Identity";


        /// <summary>
        /// 查找应用程序集模式列表。
        /// </summary>
        public List<string> SearchApplicationAssemblyPatterns { get; }
            = new List<string>
            {
                $@"^{nameof(Librame)}.{nameof(AspNetCore)}.(\w+).{nameof(Web)}$"
            };


        /// <summary>
        /// 主题包。
        /// </summary>
        public ThemepackOptions Themepack { get; }
            = new ThemepackOptions();
    }
}
