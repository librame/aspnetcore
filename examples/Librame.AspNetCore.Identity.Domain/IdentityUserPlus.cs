using System;

namespace Librame.AspNetCore.Identity
{
    /// <summary>
    /// 身份用户增强。
    /// </summary>
    public class IdentityUserPlus : IdentityUser
    {
        /// <summary>
        /// 年龄。
        /// </summary>
        public int Age { get; set; }
    }
}
