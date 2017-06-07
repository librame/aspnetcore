#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameStandard.Authentication.Managers
{
    using Models;
    using Utilities;

    /// <summary>
    /// 令牌管理器接口。
    /// </summary>
    public interface ITokenManager
    {
        /// <summary>
        /// Librame 构建器。
        /// </summary>
        ILibrameBuilder Builder { get; }

        /// <summary>
        /// 令牌编解码器。
        /// </summary>
        ITokenCodec Codec { get; }


        /// <summary>
        /// 验证令牌。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="model">输出用户模型接口。</param>
        /// <returns>返回是否通过验证的布尔值。</returns>
        bool Validate(string name, out IUserModel model);
    }


    /// <summary>
    /// 令牌管理器。
    /// </summary>
    public class TokenManager : ITokenManager
    {
        /// <summary>
        /// 构造一个用户管理器实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public TokenManager(ILibrameBuilder builder)
        {
            Builder = builder.NotNull(nameof(builder));
        }


        /// <summary>
        /// Librame 构建器。
        /// </summary>
        public ILibrameBuilder Builder { get; }

        /// <summary>
        /// 令牌编解码器。
        /// </summary>
        public ITokenCodec Codec => Builder.GetService<ITokenCodec>();


        /// <summary>
        /// 验证令牌。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="model">输出用户模型接口。</param>
        /// <returns>返回是否通过验证的布尔值。</returns>
        public virtual bool Validate(string name, out IUserModel model)
        {
            if (string.IsNullOrEmpty(name))
            {
                model = null;
                return false;
            }

            return ValidateCore(name, out model);
        }

        /// <summary>
        /// 验证核心。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="model">输出用户模型接口。</param>
        /// <returns>返回是否通过验证的布尔值。</returns>
        protected virtual bool ValidateCore(string name, out IUserModel model)
        {
            // 解码令牌代替数据库验证
            var user = Codec.Decode(name);
            
            if (user != null)
            {
                model = new UserModel()
                {
                    UniqueId = user.UniqueId,
                    Name = user.Name
                };
                return true;
            }

            model = null;
            return false;
        }

    }
}
