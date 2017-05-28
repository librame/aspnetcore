#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Security.Cryptography;

namespace LibrameCore.Algorithm.Asymmetries
{
    using Utility;

    /// <summary>
    /// RSA 参数描述符。
    /// </summary>
    public class RsaParametersDescriptor
    {
        /// <summary>
        /// 表示 RSA 算法的私钥指数。
        /// </summary>
        public string D { get; set; } = string.Empty;

        /// <summary>
        /// 表示 RSA 算法的私钥指数模数（P-1）。
        /// </summary>
        public string DP { get; set; } = string.Empty;

        /// <summary>
        /// 表示 RSA 算法的私钥指数模数（Q-1）。
        /// </summary>
        public string DQ { get; set; } = string.Empty;

        /// <summary>
        /// 表示 RSA 算法的公钥指数。
        /// </summary>
        public string Exponent { get; set; } = string.Empty;

        /// <summary>
        /// 表示 RSA 算法的私钥指数模数反转（1-Q）。
        /// </summary>
        public string InverseQ { get; set; } = string.Empty;

        /// <summary>
        /// 表示 RSA 算法的 Modulus 参数。
        /// </summary>
        public string Modulus { get; set; } = string.Empty;

        /// <summary>
        /// 表示 RSA 算法的 P 参数。
        /// </summary>
        public string P { get; set; } = string.Empty;

        /// <summary>
        /// 表示 RSA 算法的 Q 参数。
        /// </summary>
        public string Q { get; set; } = string.Empty;


        /// <summary>
        /// 从字符串中解析 RSA 参数。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="parseFactory">给定从字符串到字节数组的解析工厂方法。</param>
        /// <returns>返回参数。</returns>
        public static RSAParameters FromString(string str, Func<string, byte[]> parseFactory)
        {
            str.NotEmpty(nameof(str));
            parseFactory.NotNull(nameof(parseFactory));

            var parameters = new RSAParameters();

            var descriptor = str.FromPairsString<RsaParametersDescriptor>();

            if (descriptor.D != null && descriptor.D.Length > 0)
                parameters.D = parseFactory.Invoke(descriptor.D);

            if (descriptor.DP != null && descriptor.DP.Length > 0)
                parameters.DP = parseFactory.Invoke(descriptor.DP);

            if (descriptor.DQ != null && descriptor.DQ.Length > 0)
                parameters.DQ = parseFactory.Invoke(descriptor.DQ);

            if (descriptor.Exponent != null && descriptor.Exponent.Length > 0)
                parameters.Exponent = parseFactory.Invoke(descriptor.Exponent);

            if (descriptor.InverseQ != null && descriptor.InverseQ.Length > 0)
                parameters.InverseQ = parseFactory.Invoke(descriptor.InverseQ);

            if (descriptor.Modulus != null && descriptor.Modulus.Length > 0)
                parameters.Modulus = parseFactory.Invoke(descriptor.Modulus);

            if (descriptor.P != null && descriptor.P.Length > 0)
                parameters.P = parseFactory.Invoke(descriptor.P);

            if (descriptor.Q != null && descriptor.Q.Length > 0)
                parameters.Q = parseFactory.Invoke(descriptor.Q);

            return parameters;
        }

        /// <summary>
        /// 将 RSA 参数转换为字符串。
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="parseFactory">给定从字节数组到字符串的解析工厂方法。</param>
        /// <returns>返回字符串。</returns>
        public static string ToString(RSAParameters parameters, Func<byte[], string> parseFactory)
        {
            parameters.NotNull(nameof(parameters));
            parseFactory.NotNull(nameof(parseFactory));

            var descriptor = new RsaParametersDescriptor();

            if (parameters.D != null && parameters.D.Length > 0)
                descriptor.D = parseFactory.Invoke(parameters.D);

            if (parameters.DP != null && parameters.DP.Length > 0)
                descriptor.DP = parseFactory.Invoke(parameters.DP);

            if (parameters.DQ != null && parameters.DQ.Length > 0)
                descriptor.DQ = parseFactory.Invoke(parameters.DQ);

            if (parameters.Exponent != null && parameters.Exponent.Length > 0)
                descriptor.Exponent = parseFactory.Invoke(parameters.Exponent);

            if (parameters.InverseQ != null && parameters.InverseQ.Length > 0)
                descriptor.InverseQ = parseFactory.Invoke(parameters.InverseQ);

            if (parameters.Modulus != null && parameters.Modulus.Length > 0)
                descriptor.Modulus = parseFactory.Invoke(parameters.Modulus);

            if (parameters.P != null && parameters.P.Length > 0)
                descriptor.P = parseFactory.Invoke(parameters.P);

            if (parameters.Q != null && parameters.Q.Length > 0)
                descriptor.Q = parseFactory.Invoke(parameters.Q);

            return descriptor.AsPairsString();
        }

    }
}
