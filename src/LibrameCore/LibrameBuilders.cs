#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace LibrameCore
{
    using Utility;

    /// <summary>
    /// Librame 构建器接口。
    /// </summary>
    public interface ILibrameBuilder
    {
        /// <summary>
        /// 服务集合。
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// 服务提供程序。
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 记录器。
        /// </summary>
        ILogger<ILibrameBuilder> Logger { get; }

        /// <summary>
        /// 选项。
        /// </summary>
        LibrameOptions Options { get; }


        /// <summary>
        /// 配置。
        /// </summary>
        IConfiguration Configuration { get; set; }


        /// <summary>
        /// 生成授权标识。
        /// </summary>
        /// <returns>返回标识字符串。</returns>
        string GenerateAuthId();

        /// <summary>
        /// 还原授权标识。
        /// </summary>
        /// <param name="authId">给定的标识字符串（可选；默认为当前框架授权标识）。</param>
        /// <returns>返回字节数组。</returns>
        byte[] FromAuthId(string authId = null);
    }


    /// <summary>
    /// Librame 构建器。
    /// </summary>
    public class LibrameBuilder : ILibrameBuilder
    {
        /// <summary>
        /// 构造一个 Librame 构建器实例。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        public LibrameBuilder(IServiceCollection services)
        {
            Services = services.NotNull(nameof(services));

            // 添加自身
            Services.AddSingleton<ILibrameBuilder>(this);
        }


        /// <summary>
        /// 服务集合。
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 服务提供程序。
        /// </summary>
        public IServiceProvider ServiceProvider => Services.BuildServiceProvider();
        
        /// <summary>
        /// 记录器。
        /// </summary>
        public ILogger<ILibrameBuilder> Logger => ServiceProvider.GetService<ILogger<LibrameBuilder>>();
        
        /// <summary>
        /// 选项。
        /// </summary>
        public LibrameOptions Options => ServiceProvider.GetService<IOptions<LibrameOptions>>().Value;


        /// <summary>
        /// 配置。
        /// </summary>
        public IConfiguration Configuration { get; set; }


        /// <summary>
        /// 生成授权标识。
        /// </summary>
        /// <returns>返回标识字符串。</returns>
        public virtual string GenerateAuthId()
        {
            return Guid.NewGuid().ToByteArray().AsHex();
        }

        /// <summary>
        /// 还原授权标识。
        /// </summary>
        /// <param name="authId">给定的标识字符串（可选；默认为当前框架授权标识）。</param>
        /// <returns>返回字节数组。</returns>
        public virtual byte[] FromAuthId(string authId = null)
        {
            authId = authId.AsOrDefault(Options.AuthId);

            return authId.FromHex();
        }

    }
}
