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

namespace Librame.AspNetCore.Identity.Api.Models
{
    using AspNetCore.Api.Models;

    /// <summary>
    /// 角色模型。
    /// </summary>
    public class RoleModel : AbstractCreationIdentifierModel
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 角色声明列表。
        /// </summary>
        public IReadOnlyList<RoleClaimModel> RoleClaims { get; set; }
    }
}
