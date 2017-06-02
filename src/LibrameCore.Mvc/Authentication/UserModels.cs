#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace LibrameStandard.Authentication
{
    /// <summary>
    /// 用户模型接口。
    /// </summary>
    public interface IUserModel
    {
        /// <summary>
        /// 唯一标识。
        /// </summary>
        string UniqueId { get; }

        /// <summary>
        /// 名称。
        /// </summary>
        string Name { get; }
    }


    /// <summary>
    /// 用户模型。
    /// </summary>
    public class UserModel : IUserModel
    {
        /// <summary>
        /// 默认用户名。
        /// </summary>
        internal const string DEFAULT_NAME = "LibrameUser";

        /// <summary>
        /// 默认密码。
        /// </summary>
        internal const string DEFAULT_PASSWD = "123456";


        /// <summary>
        /// 唯一标识。
        /// </summary>
        public string UniqueId { get; set; } = Guid.Empty.ToString();

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; } = DEFAULT_NAME;

        /// <summary>
        /// 密码。
        /// </summary>
        public string Passwd { get; set; } = DEFAULT_PASSWD;
    }

}
