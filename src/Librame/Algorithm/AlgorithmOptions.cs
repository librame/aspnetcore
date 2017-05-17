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
            = typeof(Base64ByteConverter).AssemblyQualifiedNameWithoutVcp();

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


        #region SymmetryAlgorithmKeyGeneratorTypeName

        /// <summary>
        /// 对称算法密钥生成器类型名键。
        /// </summary>
        public static readonly string SymmetryAlgorithmKeyGeneratorTypeNameKey
            = (KeyPrefix + nameof(SymmetryAlgorithmKeyGeneratorTypeName));

        /// <summary>
        /// 默认对称算法密钥生成器类型名。
        /// </summary>
        public static readonly string DefaultSymmetryAlgorithmKeyGeneratorTypeName
            = typeof(Symmetries.AuthIdSymmetryAlgorithmKeyGenerator).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 对称算法密钥生成器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultSymmetryAlgorithmKeyGeneratorTypeName"/>。
        /// </value>
        public string SymmetryAlgorithmKeyGeneratorTypeName { get; set; } = DefaultSymmetryAlgorithmKeyGeneratorTypeName;

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


        #region AsymmetryAlgorithmKeyGeneratorTypeName

        /// <summary>
        /// 非对称算法密钥生成器类型名键。
        /// </summary>
        public static readonly string AsymmetryAlgorithmKeyGeneratorTypeNameKey
            = (KeyPrefix + nameof(AsymmetryAlgorithmKeyGeneratorTypeName));

        /// <summary>
        /// 默认非对称算法密钥生成器类型名。
        /// </summary>
        public static readonly string DefaultAsymmetryAlgorithmKeyGeneratorTypeName
            = typeof(Asymmetries.RsaAsymmetryAlgorithmKeyGenerator).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 非对称算法密钥生成器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultAsymmetryAlgorithmKeyGeneratorTypeName"/>。
        /// </value>
        public string AsymmetryAlgorithmKeyGeneratorTypeName { get; set; } = DefaultAsymmetryAlgorithmKeyGeneratorTypeName;

        #endregion


        #region AsymmetryAlgorithmTypeName

        /// <summary>
        /// 非对称算法类型名键。
        /// </summary>
        public static readonly string AsymmetryAlgorithmTypeNameKey
            = (KeyPrefix + nameof(AsymmetryAlgorithmTypeName));

        /// <summary>
        /// 默认非对称算法类型名。
        /// </summary>
        public static readonly string DefaultAsymmetryAlgorithmTypeName
            = typeof(Asymmetries.AsymmetryAlgorithm).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 非对称算法类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultAsymmetryAlgorithmTypeName"/>。
        /// </value>
        public string AsymmetryAlgorithmTypeName { get; set; } = DefaultAsymmetryAlgorithmTypeName;

        #endregion


        #region RsaPublicKeyString

        /// <summary>
        /// RSA 公钥字符串键。
        /// </summary>
        public static readonly string RsaPublicKeyStringKey
            = (KeyPrefix + nameof(RsaPublicKeyString));

        /// <summary>
        /// 默认 RSA 公钥字符串。
        /// </summary>
        public static readonly string DefaultRsaPublicKeyString
            = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC0xP5HcfThSQr43bAMoopbzcCyZWE0xfUeTA4Nx4PrXEfDvybJEIjbU/rgANAty1yp7g20J7+wVMPCusxftl/d0rPQiCLjeZ3HtlRKld+9htAZtHFZosV29h/hNE9JkxzGXstaSeXIUIWquMZQ8XyscIHhqoOmjXaCv58CSRAlAQIDAQAB";

        /// <summary>
        /// RSA 公钥字符串。
        /// </summary>
        public string RsaPublicKeyString { get; set; } = DefaultRsaPublicKeyString;

        #endregion


        #region RsaPrivateKeyString

        /// <summary>
        /// RSA 私钥字符串键。
        /// </summary>
        public static readonly string RsaPrivateKeyStringKey
            = (KeyPrefix + nameof(RsaPrivateKeyString));

        /// <summary>
        /// 默认 RSA 私钥字符串。
        /// </summary>
        public static readonly string DefaultRsaPrivateKeyString
            = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC0xP5HcfThSQr43bAMoopbzcCyZWE0xfUeTA4Nx4PrXEfDvybJEIjbU/rgANAty1yp7g20J7+wVMPCusxftl/d0rPQiCLjeZ3HtlRKld+9htAZtHFZosV29h/hNE9JkxzGXstaSeXIUIWquMZQ8XyscIHhqoOmjXaCv58CSRAlAQIDAQAB";

        /// <summary>
        /// RSA 私钥字符串。
        /// </summary>
        public string RsaPrivateKeyString { get; set; } = DefaultRsaPrivateKeyString;

        #endregion

    }
}
