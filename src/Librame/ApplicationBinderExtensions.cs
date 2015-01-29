// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame;
using Librame.Data.Context;
using Librame.Data.Context.Kendo;
using Librame.Security;
using Librame.Security.Hash;
using Librame.Security.Keying;
using Librame.Security.Symmetry;
using System;
using System.Text;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// 应用绑定器静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class ApplicationBinderExtensions
    {
        /// <summary>
        /// 获取指定类型的服务。
        /// </summary>
        /// <typeparam name="TService">指定的服务类型。</typeparam>
        /// <param name="binder">给定的 <see cref="IApplicationBinder"/>。</param>
        /// <returns>返回服务对象。</returns>
        /// <seealso cref="IServiceProvider.GetService(Type)"/>
        public static TService GetService<TService>(this IApplicationBinder binder)
        {
            return binder.Builder.GetService<TService>();
        }


        #region QueryInterceptor
        
        /// <summary>
        /// 绑定查询拦截器。
        /// </summary>
        /// <param name="binder">给定的 <see cref="IApplicationBinder"/>。</param>
        /// <param name="binderFactory">给定的应用绑定方法。</param>
        /// <returns>返回 <see cref="IApplicationBinder"/>。</returns>
        public static IApplicationBinder BindQueryInterceptor(this IApplicationBinder binder, Func<IApplicationContainer, IApplicationContainer> binderFactory = null)
        {
            if (ReferenceEquals(binderFactory, null))
            {
                binderFactory = b =>
                {
                    return b.RegisterQueryInterceptor<PagerQueryBase, KendoPagerQuery>(KendoQuery.PagerParameters)
                            .RegisterQueryInterceptor<FilterQueryBase, KendoFilterQuery>(KendoQuery.FilterParameters)
                            .RegisterQueryInterceptor<SorterQueryBase, KendoSorterQuery>(KendoQuery.SorterParameters);
                };
            }

            return binder.Bind(ApplicationBinder.QueryInterceptorDomain, binderFactory);
        }

        #endregion


        #region Security

        /// <summary>
        /// 绑定 SHA256 哈希算法。
        /// </summary>
        /// <param name="binder">给定的 <see cref="IApplicationBinder"/>。</param>
        /// <param name="sha256">给定的 SHA256 哈希算法（可选）。</param>
        /// <returns>返回 <see cref="IApplicationBinder"/>。</returns>
        public static IApplicationBinder BindSha256(this IApplicationBinder binder, IAlgorithm sha256 = null)
        {
            if (ReferenceEquals(sha256, null))
            {
                var unique = binder.GetService<UniqueIdentity>();
                var encoding = binder.GetService<Encoding>();

                sha256 = new SHA256HashAlgorithm(encoding);
            }

            return binder.Bind(ApplicationBinder.AlgorithmDomain, b => b.Register(sha256));
        }

        /// <summary>
        /// 绑定 HMAC SHA256 键控哈希算法。
        /// </summary>
        /// <param name="binder">给定的 <see cref="IApplicationBinder"/>。</param>
        /// <param name="hmacSha256">给定的 HMAC SHA256 键控哈希算法（可选）。</param>
        /// <returns>返回 <see cref="IApplicationBinder"/>。</returns>
        public static IApplicationBinder BindHmacSha256(this IApplicationBinder binder, IKeyedAlgorithm hmacSha256 = null)
        {
            if (ReferenceEquals(hmacSha256, null))
            {
                var unique = binder.GetService<UniqueIdentity>();
                var encoding = binder.GetService<Encoding>();

                hmacSha256 = new HMACSHA256KeyedAlgorithm(unique, encoding);
            }

            return binder.Bind(ApplicationBinder.AlgorithmDomain, b => b.Register(hmacSha256));
        }

        /// <summary>
        /// 绑定 AES 对称算法。
        /// </summary>
        /// <param name="binder">给定的 <see cref="IApplicationBinder"/>。</param>
        /// <param name="aes">给定的 AES 对称算法（可选）。</param>
        /// <returns>返回 <see cref="IApplicationBinder"/>。</returns>
        public static IApplicationBinder BindAes(this IApplicationBinder binder, ISymmetryAlgorithm aes = null)
        {
            if (ReferenceEquals(aes, null))
            {
                var unique = binder.GetService<UniqueIdentity>();
                var encoding = binder.GetService<Encoding>();

                aes = new AesSymmetryAlgorithm(unique, encoding);
            }

            return binder.Bind(ApplicationBinder.AlgorithmDomain, b => b.Register(aes));
        }

        ///// <summary>
        ///// 绑定算法域应用。
        ///// </summary>
        ///// <param name="binder">给定的 <see cref="IApplicationBinder"/>。</param>
        ///// <param name="binderFactory">给定的应用绑定方法（可选）。</param>
        ///// <returns>返回 <see cref="IApplicationBinder"/>。</returns>
        //public static IApplicationBinder BindAlgorithm(this IApplicationBinder binder, Func<IApplicationContainer, IApplicationContainer> binderFactory = null)
        //{
        //    if (ReferenceEquals(binderFactory, null))
        //    {
        //        return binder;
        //    }

        //    return binder.Bind(ApplicationBinder.AlgorithmDomain, binderFactory);
        //}

        #endregion

    }
}