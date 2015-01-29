// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Librame.Security
{
    /// <summary>
    /// 安全标识抽象基类。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class SecurityIdentityBase
    {
        /// <summary>
        /// 构造一个安全标识抽象基类实例。
        /// </summary>
        /// <param name="unique">给定的唯一标识。</param>
        /// <param name="maxSize">给定的最大位大小。</param>
        public SecurityIdentityBase(UniqueIdentity unique, BitSize maxSize)
        {
            Unique = unique;
            MaxSize = maxSize;
        }

        /// <summary>
        /// 获取唯一标识。
        /// </summary>
        public UniqueIdentity Unique { get; private set; }
        /// <summary>
        /// 获取最大的位大小。
        /// </summary>
        public BitSize MaxSize { get; private set; }


        /// <summary>
        /// 获取当前标识对应的基础保密识别码（128 位）。
        /// </summary>
        /// <remarks>
        /// 保密识别码为字节数组，可用于密钥，向量等。
        /// </remarks>
        /// <returns>返回 16 元素字节数组。</returns>
        protected byte[] GetBaseSecretIdCode()
        {
            // 16 元素字节数组（即 128 位）
            return Unique.Guid.ToByteArray();
        }


        #region Generate

        /// <summary>
        /// 生成指定位大小的保密识别码。
        /// </summary>
        /// <remarks>
        /// 保密识别码为字节数组，可用于密钥，向量等。
        /// </remarks>
        /// <returns>返回字节数组。</returns>
        public virtual byte[] GenerateSecretIdCode()
        {
            var baseSecretIdCode = GetBaseSecretIdCode();

            switch (MaxSize)
            {
                case BitSize._64:
                    return Generate64SecretIdCode(baseSecretIdCode).ToArray();

                case BitSize._128:
                    return Generate128SecretIdCode(baseSecretIdCode).ToArray();

                case BitSize._160:
                    return Generate160SecretIdCode(baseSecretIdCode).ToArray();

                case BitSize._192:
                    return Generate192SecretIdCode(baseSecretIdCode).ToArray();

                case BitSize._256:
                    return Generate256SecretIdCode(baseSecretIdCode).ToArray();

                case BitSize._384:
                    return Generate384SecretIdCode(baseSecretIdCode).ToArray();

                case BitSize._512:
                    return Generate512SecretIdCode(baseSecretIdCode).ToArray();

                case BitSize._768:
                    return Generate768SecretIdCode(baseSecretIdCode).ToArray();

                case BitSize._1024:
                    return Generate1024SecretIdCode(baseSecretIdCode).ToArray();

                default:
                    return baseSecretIdCode;
            }
        }

        /// <summary>
        /// 生成 64 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected virtual IEnumerable<byte> Generate64SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return baseSecretIdCode.Half();
        }
        /// <summary>
        /// 生成 128 位保密识别码。
        /// </summary>
        /// <remarks>
        /// 默认直接返回。
        /// </remarks>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected virtual IEnumerable<byte> Generate128SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            // 默认为 128 位，直接返回
            return baseSecretIdCode;
        }
        /// <summary>
        /// 生成 160 位保密识别码。
        /// </summary>
        /// <remarks>
        /// 默认直接返回。
        /// </remarks>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected virtual IEnumerable<byte> Generate160SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return baseSecretIdCode.Concat(baseSecretIdCode.Take(4));
        }
        /// <summary>
        /// 生成 192 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected virtual IEnumerable<byte> Generate192SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return baseSecretIdCode.Concat(baseSecretIdCode.Half());
        }
        /// <summary>
        /// 生成 256 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected virtual IEnumerable<byte> Generate256SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return baseSecretIdCode.Multiple(1);
        }
        /// <summary>
        /// 生成 384 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected virtual IEnumerable<byte> Generate384SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return baseSecretIdCode.Multiple(2);
        }
        /// <summary>
        /// 生成 512 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected virtual IEnumerable<byte> Generate512SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return baseSecretIdCode.Multiple(3);
        }
        /// <summary>
        /// 生成 768 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected virtual IEnumerable<byte> Generate768SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return baseSecretIdCode.Multiple(5);
        }
        /// <summary>
        /// 生成 1024 位保密识别码。
        /// </summary>
        /// <param name="baseSecretIdCode">给定的基础保密识别码（128 位）。</param>
        /// <returns>返回字节集合。</returns>
        protected virtual IEnumerable<byte> Generate1024SecretIdCode(IEnumerable<byte> baseSecretIdCode)
        {
            return baseSecretIdCode.Multiple(7);
        }

        #endregion

    }
}