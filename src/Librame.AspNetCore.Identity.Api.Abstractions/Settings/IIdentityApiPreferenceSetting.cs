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
    /// 身份 API 偏好设置接口。
    /// </summary>
    public interface IIdentityApiPreferenceSetting : IPreferenceSetting
    {
        /// <summary>
        /// 支持查询所有角色。
        /// </summary>
        bool SupportsQueryRoles { get; }

        /// <summary>
        /// 支持查询所有用户。
        /// </summary>
        bool SupportsQueryUsers { get; }
    }
}
