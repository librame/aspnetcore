#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameCore
{
    /// <summary>
    /// Librame MVC 构建器适配静态扩展。
    /// </summary>
    public static class LibrameMvcBuilderAdaptationExtensions
    {
        /// <summary>
        /// 获取认证适配器。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="tokenGenerate">给定的令牌生成选项（可选）。</param>
        /// <returns>返回认证适配器。</returns>
        public static Authentication.IAuthenticationAdapter GetAuthenticationAdapter(this ILibrameBuilder builder,
            Authentication.TokenGenerateOptions tokenGenerate = null)
        {
            var adapter = builder.GetAdapter<Authentication.IAuthenticationAdapter>();

            // 增强令牌生成器
            adapter.BuildUpTokenGenerator(tokenGenerate);

            return adapter;
        }

    }
}
