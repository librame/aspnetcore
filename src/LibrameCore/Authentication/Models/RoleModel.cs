#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameStandard.Authentication.Models
{
    /// <summary>
    /// 角色模型。
    /// </summary>
    public class RoleModel : IRoleModel
    {
        /// <summary>
        /// 默认名称。
        /// </summary>
        internal const string DEFAULT_NAME = "Administrator";


        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; } = DEFAULT_NAME;
    }
}
