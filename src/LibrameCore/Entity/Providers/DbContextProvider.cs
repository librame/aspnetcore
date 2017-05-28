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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace LibrameCore.Entity.Providers
{
    using Utility;

    /// <summary>
    /// 数据库上下文提供程序。
    /// </summary>
    public class DbContextProvider : DbContext
    {
        private static readonly string _cacheKey = TypeUtil.AsKey<DbContextProvider>();

        private readonly ILogger _logger;
        private readonly LibrameOptions _options;

        //private readonly ICoreConventionSetBuilder _builder;
        //private readonly IMemoryCache _cache;

        /// <summary>
        /// 构造一个 <see cref="DbContextProvider"/> 实例。
        /// </summary>
        /// <param name="dbContextOptions">给定的数据上下文选择项。</param>
        /// <param name="logger">给定的记录器接口。</param>
        /// <param name="options">给定的选择项。</param>
        public DbContextProvider(DbContextOptions<DbContextProvider> dbContextOptions,
            ILogger<DbContextProvider> logger, IOptions<LibrameOptions> options)
            : base(dbContextOptions)
        {
            _logger = logger.NotNull(nameof(logger));
            _options = options.NotNull(nameof(options)).Value;
            
            //_builder = builder.NotNullInstance(nameof(builder));
            //_cache = cache.NotNullInstance(nameof(cache));
        }

        ///// <summary>
        ///// 开始配置。
        ///// </summary>
        ///// <param name="optionsBuilder">给定的数据上下文选择项构建器。</param>
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // 如果启用实体自映射
        //    if (_config.Entity.EnableEntityAutomapping)
        //    {
        //        var model = _cache.GetOrCreate(_cacheKey, entry =>
        //        {
        //            var modelBuilder = new ModelBuilder(_builder.CreateConventionSet());

        //            // 映射模型
        //            OnModelMapping(modelBuilder);

        //            //entry.Value = modelBuilder.Model;
        //            //_cache.Set(entry.Key, modelBuilder.Model);

        //            return modelBuilder.Model;
        //        });

        //        // 完成注册
        //        optionsBuilder.UseModel(model);
        //    }

        //    base.OnConfiguring(optionsBuilder);
        //}

        /// <summary>
        /// 开始创建模型。
        /// </summary>
        /// <param name="modelBuilder">给定的模型构建器。</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 映射模型
            OnModelMapping(modelBuilder);
        }

        /// <summary>
        /// 开始映射模型。
        /// </summary>
        /// <param name="modelBuilder">给定的模型构建器。</param>
        protected virtual void OnModelMapping(ModelBuilder modelBuilder)
        {
            // 解析自映射程序集实例集合
            var assemblies = ResolveMappingAssemblies(_options.Entity.AutomappingAssemblies);
            assemblies.NotNull(nameof(assemblies)).Invoke(a =>
            {
                _logger.LogDebug("Register assembly: " + a.FullName);

                // 提取要映射的实体类型集合
                var types = TypeUtil.EnumerableTypesByAssignableFrom<IAutomapping>(a);
                types.NotNull(nameof(types)).Invoke((t) =>
                {
                    _logger.LogDebug("Register entity type: " + t.FullName);

                    // 添加实体类型
                    var annotations = modelBuilder.Model.AddEntityType(t).SqlServer();

                    // 默认表名规范，复数形式
                    var tableName = StringUtil.AsPluralize(t.Name);

                    // 自定义表名规范，属性特性优先
                    var tableAttribute = t.Attribute<TableAttribute>();
                    if (tableAttribute != null)
                    {
                        tableName = tableAttribute.Name;

                        if (!string.IsNullOrEmpty(tableAttribute.Schema))
                            annotations.Schema = tableAttribute.Schema;
                    }
                    
                    _logger.LogDebug("Mapping entity type {0} to table {1}.{2}.",
                        t?.FullName, annotations.Schema, annotations.TableName);

                    // 设定表名
                    annotations.TableName = tableName;
                });
            });
        }

        /// <summary>
        /// 解析映射的程序集集合。
        /// </summary>
        /// <param name="automappingAssemblies">给定配置的自映射程序集字符串。</param>
        /// <returns>返回程序集集合。</returns>
        protected virtual IEnumerable<Assembly> ResolveMappingAssemblies(string automappingAssemblies)
        {
            return automappingAssemblies.NotEmpty(nameof(automappingAssemblies)).Split(',').Select(assembly =>
            {
                var assemblyName = new AssemblyName(assembly);

                return Assembly.Load(assemblyName);
            });
        }

    }
}
