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
    using Utilities;

    /// <summary>
    /// 抽象管理器。
    /// </summary>
    public class AbstractManager : IManager
    {
        /// <summary>
        /// 构造一个用户管理器实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public AbstractManager(ILibrameBuilder builder)
        {
            Builder = builder.NotNull(nameof(builder));
        }


        /// <summary>
        /// Librame 构建器。
        /// </summary>
        public ILibrameBuilder Builder { get; }

        /// <summary>
        /// 认证设置。
        /// </summary>
        public AuthenticationSettings Settings => (Builder.Options as LibrameCoreOptions).Authentication;
    }
}
