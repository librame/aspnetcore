#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Data
{
    /// <summary>
    /// <see cref="TableDescriptor"/> 静态扩展。
    /// </summary>
    public static class IdentityTableDescriptorExtensions
    {
        // 因 Abstractions.Stores 的实体都添加了 DefaultIdentity 前缀，故需要清除
        private const string DefaultIdentityPrefix = "DefaultIdentity";


        /// <summary>
        /// 插入身份标记前缀（如：Identity_）。
        /// </summary>
        /// <param name="table">给定的 <see cref="TableDescriptor"/>。</param>
        /// <returns>返回 <see cref="TableDescriptor"/>。</returns>
        public static TableDescriptor InsertIdentityPrefix(this TableDescriptor table)
            => table.InsertPrefix(nameof(AspNetCore.Identity), name => name.TrimStart(DefaultIdentityPrefix));
    }
}
