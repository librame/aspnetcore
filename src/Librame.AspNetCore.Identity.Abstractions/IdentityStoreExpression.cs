#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Linq.Expressions;

namespace Librame.AspNetCore.Identity
{
    using AspNetCore.Identity.Stores;
    using Extensions;

    /// <summary>
    /// 存储表达式。
    /// </summary>
    public static class IdentityStoreExpression
    {
        /// <summary>
        /// 获取角色唯一索引表达式。
        /// </summary>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <param name="normalizedName">给定的标准化名称。</param>
        /// <returns>返回查询表达式。</returns>
        public static Expression<Func<TRole, bool>> GetRoleUniqueIndexExpression<TRole, TGenId>(string normalizedName)
            where TRole : DefaultIdentityRole<TGenId>
            where TGenId : IEquatable<TGenId>
        {
            normalizedName.NotEmpty(nameof(normalizedName));
            
            return p => p.NormalizedName == normalizedName;
        }

        /// <summary>
        /// 获取用户唯一索引表达式。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <param name="normalizedUserName">给定的标准化用户名称。</param>
        /// <returns>返回查询表达式。</returns>
        public static Expression<Func<TUser, bool>> GetUserUniqueIndexExpression<TUser, TGenId>(string normalizedUserName)
            where TUser : DefaultIdentityUser<TGenId>
            where TGenId : IEquatable<TGenId>
        {
            normalizedUserName.NotEmpty(nameof(normalizedUserName));

            return p => p.NormalizedUserName == normalizedUserName;
        }

    }
}
