// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;
using System.Text;

namespace Librame.Security.Asymmetry
{
    class DSAAsymmetryAlgorithm : AsymmetryAlgorithmBase, IAsymmetryAlgorithm, ISignatureAlgorithm
    {
        public DSAAsymmetryAlgorithm(Encoding encoding = null)
            : base(encoding)
        {
        }

        public string SignHashAlgoName { get; set; }
            = "SHA1";

        public bool VerifySign(byte[] rgbHash, byte[] rgbSignature)
        {
            using (var dsa = new DSACryptoServiceProvider())
            {
                // 算法参数
                InitParameters(dsa);

                return dsa.VerifySignature(rgbHash, rgbSignature);
            }
        }

        private void InitParameters(DSA dsa)
        {
            if (ReferenceEquals(Parameters, null))
            {
                Parameters = dsa.ExportParameters(IncludePrivateParameters);
            }
            else
            {
                dsa.ImportParameters((DSAParameters)Parameters);
            }
        }

        public override byte[] ComputeEncoding(byte[] buffer)
        {
            using (var dsa = new DSACryptoServiceProvider())
            {
                // 算法参数
                InitParameters(dsa);

                // 签名
                DSASignatureFormatter formatter = new DSASignatureFormatter(dsa);

                // 支持如“SHA1”等哈希算法签名
                formatter.SetHashAlgorithm(SignHashAlgoName);

                return formatter.CreateSignature(buffer);
            }
        }

        public override byte[] ComputeDecoding(byte[] buffer)
        {
            // 不支持
            return buffer;
        }

    }
}