#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Runtime.InteropServices;

namespace LibrameCore.Entity
{
    using Utility;

    /// <summary>
    /// 实体选项。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class EntityOptions : ILibrameOptions
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix = (nameof(Entity) + ":");


        #region AutomappingAssemblies

        /// <summary>
        /// 自映射程序集键。
        /// </summary>
        public static readonly string AutomappingAssembliesKey
            = (KeyPrefix + nameof(AutomappingAssemblies));

        /// <summary>
        /// 默认自映射程序集。
        /// </summary>
        public static readonly string DefaultAutomappingAssemblies
            = TypeUtil.GetAssemblyName<EntityOptions>().Name;

        /// <summary>
        /// 自映射程序集集合。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultAutomappingAssemblies"/>。
        /// </value>
        public string AutomappingAssemblies { get; set; } = DefaultAutomappingAssemblies;

        #endregion


        #region EnableAutomapping

        /// <summary>
        /// 启用自映射键。
        /// </summary>
        public static readonly string EnableAutomappingKey
            = (KeyPrefix + nameof(EnableAutomapping));

        /// <summary>
        /// 默认启用自映射。
        /// </summary>
        public static readonly bool DefaultEnableAutomapping = true;

        /// <summary>
        /// 启用自映射。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultEnableAutomapping"/>；
        /// 需要自行在实体上实现 <see cref="IAutomapping"/> 实体自映射接口。
        /// </value>
        public bool EnableAutomapping { get; set; } = DefaultEnableAutomapping;

        #endregion


        #region EntityProviderTypeName

        /// <summary>
        /// 实体提供程序类型名键。
        /// </summary>
        public static readonly string EntityProviderTypeNameKey
            = (KeyPrefix + nameof(EntityProviderTypeName));

        /// <summary>
        /// 默认实体提供程序类型名。
        /// </summary>
        public static readonly string DefaultEntityProviderTypeName
            = typeof(Providers.DbContextProvider).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 实体提供程序类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultEntityProviderTypeName"/>。
        /// </value>
        public string EntityProviderTypeName { get; set; } = DefaultEntityProviderTypeName;

        #endregion


        #region RepositoryTypeName

        /// <summary>
        /// 仓库类型名键。
        /// </summary>
        public static readonly string RepositoryTypeNameKey
            = (KeyPrefix + nameof(RepositoryTypeName));

        /// <summary>
        /// 默认仓库类型名。
        /// </summary>
        public static readonly string DefaultRepositoryTypeName
            = typeof(EntityRepository<>).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 仓库类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultRepositoryTypeName"/>。
        /// </value>
        public string RepositoryTypeName { get; set; } = DefaultRepositoryTypeName;

        #endregion

    }
}
