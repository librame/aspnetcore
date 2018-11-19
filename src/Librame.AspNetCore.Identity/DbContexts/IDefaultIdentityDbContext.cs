#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity
{
    /// <summary>
    /// 默认身份数据库上下文接口。
    /// </summary>
    public interface IDefaultIdentityDbContext : IIdentityDbContext<IdentityRole, IdentityUser>
    {
    }
}
