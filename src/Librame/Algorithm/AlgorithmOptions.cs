#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Algorithm
{
    using Utility;

    /// <summary>
    /// 算法选项。
    /// </summary>
    public class AlgorithmOptions
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix = (nameof(Algorithm) + ":");


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
            = typeof(Hashes.Base64HashAlgorithm).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 散列算法类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultHashAlgorithmTypeName"/>。
        /// </value>
        public string HashAlgorithmTypeName { get; set; } = DefaultHashAlgorithmTypeName;

        #endregion
		
		
		//#region KeyGeneratorTypeName

  //      /// <summary>
  //      /// 散列算法类型名键。
  //      /// </summary>
  //      public static readonly string KeyGeneratorTypeNameKey
  //          = (KeyPrefix + nameof(KeyGeneratorTypeName));

  //      /// <summary>
  //      /// 默认散列算法类型名。
  //      /// </summary>
  //      public static readonly string DefaultKeyGeneratorTypeName
  //          = typeof(HexKeyGenerator).AssemblyQualifiedNameWithoutVcp();

  //      /// <summary>
  //      /// 散列算法类型名。
  //      /// </summary>
  //      /// <value>
  //      /// 默认为 <see cref="DefaultKeyGeneratorTypeName"/>。
  //      /// </value>
  //      public string KeyGeneratorTypeName { get; set; } = DefaultKeyGeneratorTypeName;

  //      #endregion

    }
}
