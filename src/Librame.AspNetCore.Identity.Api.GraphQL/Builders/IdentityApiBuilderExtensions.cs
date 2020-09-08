#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Api;
using Librame.AspNetCore.Api.Builders;
using Librame.AspNetCore.Identity.Api;
using Librame.AspNetCore.Identity.Builders;
using Librame.Extensions.Core.Builders;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 身份 API 构建器静态扩展。
    /// </summary>
    public static class IdentityApiBuilderExtensions
    {
        /// <summary>
        /// 添加 Identity API 扩展。
        /// </summary>
        /// <param name="decorator">给定的 <see cref="IIdentityBuilderDecorator"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public static IApiBuilder AddIdentityApi(this IIdentityBuilderDecorator decorator,
            Action<IdentityApiBuilderDependency> configureDependency = null,
            Func<IExtensionBuilder, ApiBuilderDependency, IApiBuilder> builderFactory = null)
        {
            var builder = decorator.AddApi(configureDependency, builderFactory);

            var parameterMapper = decorator.AccessorTypeParameterMapper;

            var identityApiMutationType = typeof(IdentityGraphApiMutation<,,,>).MakeGenericType(
                parameterMapper.Role.ArgumentType,
                parameterMapper.User.ArgumentType,
                parameterMapper.BaseMapper.GenId.ArgumentType,
                parameterMapper.BaseMapper.CreatedBy.ArgumentType);

            var identityApiQueryType = typeof(IdentityGraphApiQuery<,,,,,,,,>).MakeGenericType(
                parameterMapper.Role.ArgumentType,
                parameterMapper.RoleClaim.ArgumentType,
                parameterMapper.User.ArgumentType,
                parameterMapper.UserClaim.ArgumentType,
                parameterMapper.UserLogin.ArgumentType,
                parameterMapper.UserRole.ArgumentType,
                parameterMapper.UserToken.ArgumentType,
                parameterMapper.BaseMapper.GenId.ArgumentType,
                parameterMapper.BaseMapper.CreatedBy.ArgumentType);

            decorator.Services.TryReplaceAll(typeof(IGraphApiMutation), identityApiMutationType);
            decorator.Services.TryReplaceAll(typeof(IGraphApiQuery), identityApiQueryType);

            return builder;
        }

    }
}
