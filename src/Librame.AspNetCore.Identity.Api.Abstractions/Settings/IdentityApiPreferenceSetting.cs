#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api
{
    using Extensions;

    /// <summary>
    /// 身份 API 偏好设置。
    /// </summary>
    public class IdentityApiPreferenceSetting : AbstractPreferenceSetting, IIdentityApiPreferenceSetting
    {
        /// <summary>
        /// 支持查询所有角色（默认启用）。
        /// </summary>
        public virtual bool SupportsQueryRoles
            => true;

        /// <summary>
        /// 支持查询所有用户（默认启用）。
        /// </summary>
        public virtual bool SupportsQueryUsers
            => true;
    }
}
