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
    /// 令牌模型接口。
    /// </summary>
    public interface ITokenModel
    {
        /// <summary>
        /// 名称。
        /// </summary>
        string Name { get; }
    }


    /// <summary>
    /// 令牌模型。
    /// </summary>
    public class TokenModel : ITokenModel
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }

}
