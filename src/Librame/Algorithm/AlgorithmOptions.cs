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

namespace Librame.Algorithm
{
    using Utility;

    /// <summary>
    /// 算法选项。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AlgorithmOptions
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix = (nameof(Algorithm) + ":");


        #region ByteConverterTypeName

        /// <summary>
        /// 字节转换器类型名键。
        /// </summary>
        public static readonly string ByteConverterTypeNameKey
            = (KeyPrefix + nameof(ByteConverterTypeName));

        /// <summary>
        /// 默认字节转换器类型名。
        /// </summary>
        public static readonly string DefaultByteConverterTypeName
            = typeof(HexByteConverter).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 字节转换器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultByteConverterTypeName"/>。
        /// </value>
        public string ByteConverterTypeName { get; set; } = DefaultByteConverterTypeName;

        #endregion


        #region HashAlgorithmTypeName

        /// <summary>
        /// 散列算法类型名键。
        /// </summary>
        public static readonly string HashAlgorithmTypeNameKey
            = (KeyPrefix + nameof(HashAlgorithmTypeName));

        /// <summary>
        /// 默认散列算法类型名。
        /// </summary>
        public static readonly string DefaultHashAlgorithmTypeName
            = typeof(Hashes.HashAlgorithm).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 散列算法类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultHashAlgorithmTypeName"/>。
        /// </value>
        public string HashAlgorithmTypeName { get; set; } = DefaultHashAlgorithmTypeName;

        #endregion


        #region SAKeyGeneratorTypeName

        /// <summary>
        /// 对称算法密钥生成器类型名键。
        /// </summary>
        public static readonly string SAKeyGeneratorTypeNameKey
            = (KeyPrefix + nameof(SAKeyGeneratorTypeName));

        /// <summary>
        /// 默认对称算法密钥生成器类型名。
        /// </summary>
        public static readonly string DefaultSAKeyGeneratorTypeName
            = typeof(Symmetries.AuthIdSymmetryAlgorithmKeyGenerator).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 对称算法密钥生成器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultSAKeyGeneratorTypeName"/>。
        /// </value>
        public string SAKeyGeneratorTypeName { get; set; } = DefaultSAKeyGeneratorTypeName;

        #endregion


        #region SymmetryAlgorithmTypeName

        /// <summary>
        /// 对称算法类型名键。
        /// </summary>
        public static readonly string SymmetryAlgorithmTypeNameKey
            = (KeyPrefix + nameof(SymmetryAlgorithmTypeName));

        /// <summary>
        /// 默认对称算法类型名。
        /// </summary>
        public static readonly string DefaultSymmetryAlgorithmTypeName
            = typeof(Symmetries.SymmetryAlgorithm).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 对称算法类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultSymmetryAlgorithmTypeName"/>。
        /// </value>
        public string SymmetryAlgorithmTypeName { get; set; } = DefaultSymmetryAlgorithmTypeName;

        #endregion

    }
}
