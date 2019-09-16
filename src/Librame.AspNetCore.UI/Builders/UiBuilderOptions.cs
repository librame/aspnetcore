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
    ///  UI 构建器选项。
    /// </summary>
    public class UiBuilderOptions : AbstractExtensionBuilderOptions
    {
        /// <summary>
        /// 激活视图键名。
        /// </summary>
        public string ActiveViewKey { get; set; }
            = "ActiveView";

        /// <summary>
        /// 有外部认证方案的键名。
        /// </summary>
        public string HasExternalAuthenticationSchemesKey { get; set; }
            = $"{typeof(UiBuilderOptions).Namespace}.HasExternalLogins";
    }
}
