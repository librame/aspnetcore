#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Mappers
{
    using Extensions;
    using Extensions.Core.Mappers;
    using Extensions.Data.Mappers;

    /// <summary>
    /// 身份访问器类型参数映射器。
    /// </summary>
    public class IdentityAccessorTypeParameterMapper : AbstractTypeParameterMapper
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityAccessorTypeParameterMapper"/>。
        /// </summary>
        /// <param name="baseMapper">给定的基础 <see cref="AccessorTypeParameterMapper"/>。</param>
        /// <param name="mappings">给定的泛型类型映射字典集合。</param>
        public IdentityAccessorTypeParameterMapper(AccessorTypeParameterMapper baseMapper,
            TypeParameterMappingCollection mappings)
            : base(mappings)
        {
            BaseMapper = baseMapper.NotNull(nameof(baseMapper));
        }


        /// <summary>
        /// 基础 <see cref="AccessorTypeParameterMapper"/>。
        /// </summary>
        /// <value>返回 <see cref="AccessorTypeParameterMapper"/>。</value>
        public AccessorTypeParameterMapper BaseMapper { get; }


        /// <summary>
        /// 角色映射。
        /// </summary>
        public TypeParameterMapping Role
            => GetGenericMapping(nameof(Role));

        /// <summary>
        /// 角色声明映射。
        /// </summary>
        public TypeParameterMapping RoleClaim
            => GetGenericMapping(nameof(RoleClaim));

        /// <summary>
        /// 用户映射。
        /// </summary>
        public TypeParameterMapping User
            => GetGenericMapping(nameof(User));

        /// <summary>
        /// 用户声明映射。
        /// </summary>
        public TypeParameterMapping UserClaim
            => GetGenericMapping(nameof(UserClaim));

        /// <summary>
        /// 用户登入映射。
        /// </summary>
        public TypeParameterMapping UserLogin
            => GetGenericMapping(nameof(UserLogin));

        /// <summary>
        /// 用户角色映射。
        /// </summary>
        public TypeParameterMapping UserRole
            => GetGenericMapping(nameof(UserRole));

        /// <summary>
        /// 用户令牌映射。
        /// </summary>
        public TypeParameterMapping UserToken
            => GetGenericMapping(nameof(UserToken));
    }
}
