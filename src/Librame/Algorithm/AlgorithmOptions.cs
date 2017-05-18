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


        #region Codecs

        /// <summary>
        /// 明文编解码器类型名键。
        /// </summary>
        public static readonly string PlainTextCodecTypeNameKey
            = (KeyPrefix + nameof(PlainTextCodecTypeName));

        /// <summary>
        /// 默认明文编解码器类型名。
        /// </summary>
        public static readonly string DefaultPlainTextCodecTypeName
            = typeof(Codecs.PlainTextCodec).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 明文编解码器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultPlainTextCodecTypeName"/>。
        /// </value>
        public string PlainTextCodecTypeName { get; set; } = DefaultPlainTextCodecTypeName;


        /// <summary>
        /// 密文编解码器类型名键。
        /// </summary>
        public static readonly string CipherTextCodecTypeNameKey
            = (KeyPrefix + nameof(CipherTextCodecTypeName));

        /// <summary>
        /// 默认密文编解码器类型名。
        /// </summary>
        public static readonly string DefaultCipherTextCodecTypeName
            = typeof(Codecs.Base64CipherTextCodec).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 密文编解码器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultCipherTextCodecTypeName"/>。
        /// </value>
        public string CipherTextCodecTypeName { get; set; } = DefaultCipherTextCodecTypeName;

        #endregion


        #region Asymmetries

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


        /// <summary>
        /// RSA 非对称算法密钥生成器类型名键。
        /// </summary>
        public static readonly string RsaAsymmetryKeyGeneratorTypeNameKey
            = (KeyPrefix + nameof(RsaAsymmetryKeyGeneratorTypeName));

        /// <summary>
        /// 默认 RSA 非对称算法密钥生成器类型名。
        /// </summary>
        public static readonly string DefaultRsaAsymmetryKeyGeneratorTypeName
            = typeof(Asymmetries.RsaAsymmetryKeyGenerator).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// RSA 非对称算法密钥生成器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultRsaAsymmetryKeyGeneratorTypeName"/>。
        /// </value>
        public string RsaAsymmetryKeyGeneratorTypeName { get; set; } = DefaultRsaAsymmetryKeyGeneratorTypeName;


        /// <summary>
        /// RSA 非对称算法类型名键。
        /// </summary>
        public static readonly string RsaAsymmetryAlgorithmTypeNameKey
            = (KeyPrefix + nameof(RsaAsymmetryAlgorithmTypeName));

        /// <summary>
        /// 默认 RSA 非对称算法类型名。
        /// </summary>
        public static readonly string DefaultRsaAsymmetryAlgorithmTypeName
            = typeof(Asymmetries.RsaAsymmetryAlgorithm).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// RSA 非对称算法类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultRsaAsymmetryAlgorithmTypeName"/>。
        /// </value>
        public string RsaAsymmetryAlgorithmTypeName { get; set; } = DefaultRsaAsymmetryAlgorithmTypeName;

        #endregion


        #region Hashes

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


        #region Symmetries

        /// <summary>
        /// 对称算法密钥生成器类型名键。
        /// </summary>
        public static readonly string SymmetryKeyGeneratorTypeNameKey
            = (KeyPrefix + nameof(SymmetryKeyGeneratorTypeName));

        /// <summary>
        /// 默认对称算法密钥生成器类型名。
        /// </summary>
        public static readonly string DefaultSymmetryKeyGeneratorTypeName
            = typeof(Symmetries.AuthIdSymmetryKeyGenerator).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 对称算法密钥生成器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultSymmetryKeyGeneratorTypeName"/>。
        /// </value>
        public string SymmetryKeyGeneratorTypeName { get; set; } = DefaultSymmetryKeyGeneratorTypeName;
        

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
