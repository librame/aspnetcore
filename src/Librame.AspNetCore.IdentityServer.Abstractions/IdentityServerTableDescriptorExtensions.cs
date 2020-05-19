#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Data
{
    /// <summary>
    /// <see cref="TableDescriptor"/> 静态扩展。
    /// </summary>
    public static class IdentityServerTableDescriptorExtensions
    {
        /// <summary>
        /// 插入身份标记前缀（如：IdentityServer_）。
        /// </summary>
        /// <param name="table">给定的 <see cref="TableDescriptor"/>。</param>
        /// <returns>返回 <see cref="TableDescriptor"/>。</returns>
        public static TableDescriptor InsertIdentityServerPrefix(this TableDescriptor table)
            => table.InsertPrefix(nameof(AspNetCore.IdentityServer));
    }
}
