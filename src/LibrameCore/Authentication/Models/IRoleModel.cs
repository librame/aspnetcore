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
    /// 角色模型接口。
    /// </summary>
    public interface IRoleModel
    {
        /// <summary>
        /// 名称。
        /// </summary>
        string Name { get; }
    }
}
