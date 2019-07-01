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
    using Extensions.Core;

    /// <summary>
    /// 身份角色资源。
    /// </summary>
    public class IdentityRoleResource : IResource
    {
        /// <summary>
        /// 标识。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 标准化名称。
        /// </summary>
        public string NormalizedName { get; set; }
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 并发标志。
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}
