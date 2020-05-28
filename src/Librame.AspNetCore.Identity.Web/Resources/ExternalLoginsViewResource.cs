#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Web.Resources
{
    using AspNetCore.Web.Resources;

    /// <summary>
    /// 外部登入集合视图资源。
    /// </summary>
    public class ExternalLoginsViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 按钮标题。
        /// </summary>
        public string ButtonTitle { get; set; }

        /// <summary>
        /// 另一个描述。
        /// </summary>
        public string AnotherDescr { get; set; }

        /// <summary>
        /// 另一个登入标题。
        /// </summary>
        public string AnotherLoginTitle { get; set; }

        /// <summary>
        /// 暂无外部登入。
        /// </summary>
        public string NoneLogins { get; set; }


        /// <summary>
        /// 移除登入成功。
        /// </summary>
        public string RemoveLoginSuccess { get; set; }

        /// <summary>
        /// 添加登入成功。
        /// </summary>
        public string AddLoginSuccess { get; set; }

        /// <summary>
        /// 错误。
        /// </summary>
        public string Error { get; set; }
    }
}
