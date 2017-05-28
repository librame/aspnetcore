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

namespace LibrameCore.Algorithm
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


        #region TextCodecs

        /// <summary>
        /// 明文编解码器类型名键。
        /// </summary>
        public static readonly string PlainTextCodecTypeNameKey
            = (KeyPrefix + nameof(PlainTextCodecTypeName));

        /// <summary>
        /// 默认明文编解码器类型名。
        /// </summary>
        public static readonly string DefaultPlainTextCodecTypeName
            = typeof(TextCodecs.PlainTextCodec).AssemblyQualifiedNameWithoutVcp();

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
            = typeof(TextCodecs.Base64CipherTextCodec).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 密文编解码器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultCipherTextCodecTypeName"/>。
        /// </value>
        public string CipherTextCodecTypeName { get; set; } = DefaultCipherTextCodecTypeName;

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


        #region Asymmetries
        
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


        /// <summary>
        /// RSA 公钥字符串键。
        /// </summary>
        public static readonly string RsaPublicKeyStringKey
            = (KeyPrefix + nameof(RsaPublicKeyString));

        /// <summary>
        /// 默认 RSA 公钥字符串。
        /// </summary>
        public static readonly string DefaultRsaPublicKeyString
            = "DjxV6/ElNXePW4msmxx/yzhIlpKrrnocwRpknqGGKyg/ZA9MqlYWHJhMGpfXYoPu7U1IapT5m50BxwMJZjO9bpLM6EOzM0wzTFvNr8X3aFGkOh0BQwWMbG4sIfs4dGnw/uNrI6qAqgG8dKq/bMukNNFyHDsxjqjEZTrYcfpZm4qCjIUlKIsyXN1aXOPskSGIFB/Ah5aI8nXIB6EK5RLaUitfADD4kEZLJVAUfzNcjpdlzNMgB5R8OMJmSHrjGrbKaz6W3NhYK+ptEZ0dELLEh2yAM7+7AyfkIHzhiocz58bYzU9zLqtsSZfDsL+BOZxs3z6GKGUgRGVnk6EHslHxlPlBjwe47WdYnCFSFbHxlsVyqgn4G1/0nWPAIvO/rIGnm/AW+dhNRkSFeez7qEWzTmyDy/brlYoq53JuM2rXRRKQNlkfURx+s5wK70ASLq0CVDmwYZywhFQZGOnB1X7LolODHKgqpg/I6h25wRmrJhBUQTI1XLaCJWP/6gnRi854lMtjkBy26FCoU+m06D3VtQ==";

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
            = "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";

        /// <summary>
        /// RSA 私钥字符串。
        /// </summary>
        public string RsaPrivateKeyString { get; set; } = DefaultRsaPrivateKeyString;

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

    }
}
