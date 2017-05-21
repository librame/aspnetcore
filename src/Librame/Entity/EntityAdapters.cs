#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Librame.Entity
{
    using Adaptation;
    using Utility;

    /// <summary>
    /// 实体适配器接口。
    /// </summary>
    public interface IEntityAdapter : IAdapter
    {
        /// <summary>
        /// Librame 构建器。
        /// </summary>
        ILibrameBuilder Builder { get; }


        /// <summary>
        /// 尝试添加实体模块。
        /// </summary>
        /// <returns>返回 Librame 构建器。</returns>
        ILibrameBuilder TryAddEntity();


        /// <summary>
        /// 获取实体仓库。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <returns>返回仓库实例。</returns>
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;
    }

    /// <summary>
    /// 实体适配器。
    /// </summary>
    public class EntityAdapter : AbstractAdapter, IEntityAdapter
    {
        /// <summary>
        /// 构造一个算法适配器实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="options">给定的选择项。</param>
        public EntityAdapter(ILibrameBuilder builder, IOptions<LibrameOptions> options)
            : base(nameof(Algorithm), options)
        {
            Builder = builder.NotNull(nameof(builder));

            TryAddEntity();
        }


        /// <summary>
        /// Librame 构建器。
        /// </summary>
        public ILibrameBuilder Builder { get; }


        /// <summary>
        /// 尝试添加实体模块。
        /// </summary>
        /// <returns>返回 Librame 构建器。</returns>
        public virtual ILibrameBuilder TryAddEntity()
        {
            var options = Options.Entity;

            var providerType = Type.GetType(options.EntityProviderTypeName, throwOnError: true);
            typeof(DbContext).CanAssignableFromType(providerType);

            if (!Builder.ContainsService(providerType))
            {
                // DbContext 非线程安全
                Builder.Services.AddScoped(providerType);
            }
            
            var repositoryType = Type.GetType(options.RepositoryTypeName, throwOnError: true);
            Builder.TryAddTransientService(typeof(IRepository<>), repositoryType);

            return Builder;
        }


        /// <summary>
        /// 获取实体仓库。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <returns>返回仓库实例。</returns>
        public virtual IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            return Builder.GetService<IRepository<TEntity>>();
        }

    }
}
