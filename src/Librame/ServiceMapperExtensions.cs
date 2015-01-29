// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame;
using System.Text;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// 服务映射器静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class ServiceMapperExtensions
    {
        #region Global

        /// <summary>
        /// 映射唯一标识。
        /// </summary>
        /// <param name="mapper">给定的 <see cref="IServiceMapper"/>。</param>
        /// <param name="guid">给定的 GUID 字符串。</param>
        /// <returns>返回 <see cref="IServiceMapper"/>。</returns>
        public static IServiceMapper MapId(this IServiceMapper mapper, string guid)
        {
            return mapper.MapId(new UniqueIdentity(guid));
        }
        /// <summary>
        /// 映射唯一标识。
        /// </summary>
        /// <param name="mapper">给定的 <see cref="IServiceMapper"/>。</param>
        /// <param name="unique">给定的唯一标识。</param>
        /// <returns>返回 <see cref="IServiceMapper"/>。</returns>
        public static IServiceMapper MapId(this IServiceMapper mapper, UniqueIdentity unique = null)
        {
            if (ReferenceEquals(unique, null))
            {
                unique = new UniqueIdentity();
            }

            return mapper.Map(unique);
        }

        /// <summary>
        /// 映射字符编码。
        /// </summary>
        /// <remarks>
        /// 如果 encoding 为空，则默认映射 <see cref="Encoding.UTF8"/>。
        /// </remarks>
        /// <param name="mapper">给定的 <see cref="IServiceMapper"/>。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        /// <returns>返回 <see cref="IServiceMapper"/>。</returns>
        public static IServiceMapper MapEncoding(this IServiceMapper mapper, Encoding encoding = null)
        {
            if (ReferenceEquals(encoding, null))
            {
                encoding = Encoding.UTF8;
            }

            return mapper.Map(encoding);
        }

        #endregion


        #region AccessorFactory

        /// <summary>
        /// 映射访问器工厂。
        /// </summary>
        /// <remarks>
        /// 默认映射 <see cref="Librame.Data.Accessors.DbContextAccessorFactory"/>。
        /// </remarks>
        /// <param name="mapper">给定的 <see cref="IServiceMapper"/>。</param>
        /// <returns>返回 <see cref="IServiceMapper"/>。</returns>
        public static IServiceMapper MapAccessorFactory(this IServiceMapper mapper)
        {
            return mapper.MapAccessorFactory<Librame.Data.Accessors.DbContextAccessorFactory>();
        }
        /// <summary>
        /// 映射访问器工厂。
        /// </summary>
        /// <typeparam name="TAccessorFactory">指定的访问器工厂。</typeparam>
        /// <param name="mapper">给定的 <see cref="IServiceMapper"/>。</param>
        /// <returns>返回 <see cref="IServiceMapper"/>。</returns>
        public static IServiceMapper MapAccessorFactory<TAccessorFactory>(this IServiceMapper mapper)
            where TAccessorFactory : Librame.Data.AccessorFactory
        {
            return mapper.Map<Librame.Data.AccessorFactory, TAccessorFactory>();
        }

        #endregion
    }
}