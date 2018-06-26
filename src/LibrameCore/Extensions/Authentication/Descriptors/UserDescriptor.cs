#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Extensions.Entity.Descriptors;

namespace LibrameCore.Extensions.Authentication.Descriptors
{
    /// <summary>
    /// 用户描述符。
    /// </summary>
    /// <typeparam name="TId">指定的主键类型。</typeparam>
    public class UserDescriptor<TId> : AbstractIdDescriptor<TId>, IUserDescriptor<TId>
    {
        /// <summary>
        /// 默认名称。
        /// </summary>
        internal const string DEFAULT_NAME = "LibrameUser";

        /// <summary>
        /// 默认密码。
        /// </summary>
        internal const string DEFAULT_PASSWD = "123456";

        
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; } = DEFAULT_NAME;

        /// <summary>
        /// 密码。
        /// </summary>
        public string Passwd { get; set; } = string.Empty;
    }
}
