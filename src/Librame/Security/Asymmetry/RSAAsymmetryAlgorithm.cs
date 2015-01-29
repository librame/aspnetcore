// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;
using System.Text;

namespace Librame.Security.Asymmetry
{
    class RSAAsymmetryAlgorithm : AsymmetryAlgorithmBase, IAsymmetryAlgorithm
    {
        public RSAAsymmetryAlgorithm(Encoding encoding = null)
            : base(encoding)
        {
        }

        private void InitParameters(RSA rsa)
        {
            if (ReferenceEquals(Parameters, null))
            {
                Parameters = rsa.ExportParameters(IncludePrivateParameters);
            }
            else
            {
                rsa.ImportParameters((RSAParameters)Parameters);
            }
        }

        public override byte[] ComputeEncoding(byte[] buffer)
        {
            // 加密
            using (var rsa = new RSACryptoServiceProvider())
            {
                InitParameters(rsa);

                return rsa.EncryptValue(buffer);

                //// 签名
                //RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(rsa);

                //// 支持如“SHA1”等哈希算法签名
                //formatter.SetHashAlgorithm(SignHashAlgoName);

                //return formatter.CreateSignature(buffer);
            }
        }

        public override byte[] ComputeDecoding(byte[] buffer)
        {
            // 解密
            using (var rsa = new RSACryptoServiceProvider())
            {
                InitParameters(rsa);

                return rsa.DecryptValue(buffer);
            }
        }

    }
}