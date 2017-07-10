#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameCore.Authentication.Models
{
    /// <summary>
    /// 用户模型接口。
    /// </summary>
    public interface IUserModel
    {
        /// <summary>
        /// 名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 密码。
        /// </summary>
        string Passwd { get; }
    }
}
