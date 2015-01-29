// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Librame.Security
{
    /// <summary>
    /// 向量标识。
    /// </summary>
    /// <author>Librame Pang</author>
    public class IvIdentity : SecurityIdentityBase
    {
        /// <summary>
        /// 构造一个向量标识实例。
        /// </summary>
        /// <param name="unique">给定的唯一标识。</param>
        /// <param name="maxSize">给定的最大向量大小。</param>
        public IvIdentity(UniqueIdentity unique, BitSize maxSize)
            : base(unique, maxSize)
        {
        }


        #region Generate

        /// <summary>
        /// 生成 64 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected override IEnumerable<byte> Generate64SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return base.Generate64SecretIdCode(baseSecretIdCode).Reverse();
        }
        /// <summary>
        /// 生成 128 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected override IEnumerable<byte> Generate128SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return base.Generate128SecretIdCode(baseSecretIdCode).Reverse();
        }
        /// <summary>
        /// 生成 160 位保密识别码。
        /// </summary>
        /// <remarks>
        /// 默认直接返回。
        /// </remarks>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected override IEnumerable<byte> Generate160SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return base.Generate160SecretIdCode(baseSecretIdCode).Reverse();
        }
        /// <summary>
        /// 生成 192 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected override IEnumerable<byte> Generate192SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return base.Generate192SecretIdCode(baseSecretIdCode).Reverse();
        }
        /// <summary>
        /// 生成 256 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected override IEnumerable<byte> Generate256SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return base.Generate256SecretIdCode(baseSecretIdCode).Reverse();
        }
        /// <summary>
        /// 生成 384 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected override IEnumerable<byte> Generate384SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return base.Generate384SecretIdCode(baseSecretIdCode).Reverse();
        }
        /// <summary>
        /// 生成 512 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected override IEnumerable<byte> Generate512SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return base.Generate512SecretIdCode(baseSecretIdCode).Reverse();
        }
        /// <summary>
        /// 生成 768 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected override IEnumerable<byte> Generate768SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return base.Generate768SecretIdCode(baseSecretIdCode).Reverse();
        }
        /// <summary>
        /// 生成 1024 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected override IEnumerable<byte> Generate1024SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return base.Generate1024SecretIdCode(baseSecretIdCode).Reverse();
        }

        #endregion

    }
}