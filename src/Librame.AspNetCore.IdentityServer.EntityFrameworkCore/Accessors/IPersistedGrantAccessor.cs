#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;

namespace Librame.AspNetCore.IdentityServer.Accessors
{
    using Extensions.Data;
    using Extensions.Data.Accessors;

    /// <summary>
    /// 持久化授予访问器接口。
    /// </summary>
    public interface IPersistedGrantAccessor : IPersistedGrantDbContext, IAccessor
    {
        /// <summary>
        /// 持久化授予数据集管理器。
        /// </summary>
        DbSetManager<PersistedGrant> PersistedGrantsManager { get; }

        /// <summary>
        /// 设备流程代码数据集管理器。
        /// </summary>
        DbSetManager<DeviceFlowCodes> DeviceFlowCodesManager { get; }
    }
}
