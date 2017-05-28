#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Options;
using System;

namespace LibrameCore.Algorithm
{
    using Adaptation;
    using Utility;
    
    /// <summary>
    /// 算法适配器接口。
    /// </summary>
    public interface IAlgorithmAdapter : IAdapter
    {
        /// <summary>
        /// Librame 构建器。
        /// </summary>
        ILibrameBuilder Builder { get; }


        /// <summary>
        /// 明文编解码器。
        /// </summary>
        TextCodecs.IPlainTextCodec Plain { get; }

        /// <summary>
        /// 密文编解码器。
        /// </summary>
        TextCodecs.ICipherTextCodec Cipher { get; }


        /// <summary>
        /// 对称算法。
        /// </summary>
        Symmetries.ISymmetryAlgorithm Symmetry { get; }


        /// <summary>
        /// 非对称算法。
        /// </summary>
        Asymmetries.IRsaAsymmetryAlgorithm Asymmetry { get; }


        /// <summary>
        /// 散列算法。
        /// </summary>
        Hashes.IHashAlgorithm Hash { get; }


        /// <summary>
        /// 尝试添加算法模块。
        /// </summary>
        /// <returns>返回 Librame 构建器接口。</returns>
        ILibrameBuilder TryAddAlgorithm();
    }


    /// <summary>
    /// 算法适配器。
    /// </summary>
    public class AlgorithmAdapter : AbstractAdapter, IAlgorithmAdapter
    {
        /// <summary>
        /// 构造一个算法适配器实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="options">给定的选择项。</param>
        public AlgorithmAdapter(ILibrameBuilder builder, IOptions<LibrameOptions> options)
            : base(nameof(Algorithm), options)
        {
            Builder = builder.NotNull(nameof(builder));

            TryAddAlgorithm();
        }


        /// <summary>
        /// Librame 构建器。
        /// </summary>
        public ILibrameBuilder Builder { get; }


        /// <summary>
        /// 明文编解码器。
        /// </summary>
        public TextCodecs.IPlainTextCodec Plain => Builder.GetService<TextCodecs.IPlainTextCodec>();

        /// <summary>
        /// 密文编解码器。
        /// </summary>
        public TextCodecs.ICipherTextCodec Cipher => Builder.GetService<TextCodecs.ICipherTextCodec>();


        /// <summary>
        /// 对称算法。
        /// </summary>
        public Symmetries.ISymmetryAlgorithm Symmetry => Builder.GetService<Symmetries.ISymmetryAlgorithm>();


        /// <summary>
        /// 非对称算法。
        /// </summary>
        public Asymmetries.IRsaAsymmetryAlgorithm Asymmetry => Builder.GetService<Asymmetries.IRsaAsymmetryAlgorithm>();


        /// <summary>
        /// 散列算法。
        /// </summary>
        public Hashes.IHashAlgorithm Hash => Builder.GetService<Hashes.IHashAlgorithm>();


        /// <summary>
        /// 尝试添加算法模块。
        /// </summary>
        /// <returns>返回 Librame 构建器接口。</returns>
        public virtual ILibrameBuilder TryAddAlgorithm()
        {
            var options = Options.Algorithm;

            // 文本编解码器
            var basePlainTextCodecType = typeof(TextCodecs.IPlainTextCodec);
            var implPlainTextCodecType = Type.GetType(options.PlainTextCodecTypeName, throwOnError: true);
            Builder.TryAddSingleton(basePlainTextCodecType, implPlainTextCodecType);

            var baseCipherTextCodecType = typeof(TextCodecs.ICipherTextCodec);
            var implCipherTextCodecType = Type.GetType(options.CipherTextCodecTypeName, throwOnError: true);
            Builder.TryAddSingleton(baseCipherTextCodecType, implCipherTextCodecType);

            // 对称算法
            var baseSymmetryKeyGenerateType = typeof(Symmetries.ISymmetryKeyGenerator);
            var implSymmetryKeyGenerateType = Type.GetType(options.SymmetryKeyGeneratorTypeName, throwOnError: true);
            Builder.TryAddSingleton(baseSymmetryKeyGenerateType, implSymmetryKeyGenerateType);

            var baseSymmetryAlgorithmType = typeof(Symmetries.ISymmetryAlgorithm);
            var implSymmetryAlgorithmType = Type.GetType(options.SymmetryAlgorithmTypeName, throwOnError: true);
            Builder.TryAddSingleton(baseSymmetryAlgorithmType, implSymmetryAlgorithmType);

            // 非对称算法
            var baseRsaAsymmetryKeyGenerateType = typeof(Asymmetries.IRsaAsymmetryKeyGenerator);
            var implRsaAsymmetryKeyGenerateType = Type.GetType(options.RsaAsymmetryKeyGeneratorTypeName, throwOnError: true);
            Builder.TryAddSingleton(baseRsaAsymmetryKeyGenerateType, implRsaAsymmetryKeyGenerateType);

            var baseRsaAsymmetryAlgorithmType = typeof(Asymmetries.IRsaAsymmetryAlgorithm);
            var implRsaAsymmetryAlgorithmType = Type.GetType(options.RsaAsymmetryAlgorithmTypeName, throwOnError: true);
            Builder.TryAddSingleton(baseRsaAsymmetryAlgorithmType, implRsaAsymmetryAlgorithmType);

            // 散列算法
            var baseHashAlgorithmType = typeof(Hashes.IHashAlgorithm);
            var implHashAlgorithmType = Type.GetType(options.HashAlgorithmTypeName, throwOnError: true);
            Builder.TryAddSingleton(baseHashAlgorithmType, implHashAlgorithmType);

            return Builder;
        }

    }
}
