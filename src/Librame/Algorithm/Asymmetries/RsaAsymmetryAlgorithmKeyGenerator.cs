#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Librame.Algorithm.Asymmetries
{
    using Utility;

    /// <summary>
    /// RSA 非对称算法密钥生成器。
    /// </summary>
    public class RsaAsymmetryAlgorithmKeyGenerator : AbstractAsymmetryAlgorithmKeyGenerator
    {
        private readonly string _defaultPublicKeyString;
        private readonly string _defaultPrivateKeyString;

        /// <summary>
        /// 构造一个 RSA 非对称算法密钥生成器实例。
        /// </summary>
        /// <param name="byteConverter">给定的字节转换器接口。</param>
        /// <param name="logger">给定的记录器接口。</param>
        /// <param name="options">给定的选择项。</param>
        public RsaAsymmetryAlgorithmKeyGenerator(ICiphertextCodec byteConverter,
            ILogger<RsaAsymmetryAlgorithmKeyGenerator> logger, IOptions<LibrameOptions> options)
            : base(byteConverter, logger)
        {
            var algoOptions = options.NotNull(nameof(options)).Value.Algorithm;

            _defaultPublicKeyString = algoOptions.RsaPublicKeyString.NotEmpty(nameof(AlgorithmOptions.RsaPublicKeyString));
            _defaultPrivateKeyString = algoOptions.RsaPrivateKeyString.NotEmpty(nameof(AlgorithmOptions.RsaPrivateKeyString));
        }


        /// <summary>
        /// 比较两个字节数组（遍历每一个字节进行比较）。
        /// </summary>
        /// <param name="left">给定左边的字节数组。</param>
        /// <param name="right">给定右边的字节数组。</param>
        /// <returns>返回布尔值。</returns>
        protected virtual bool CompareBytes(byte[] left, byte[] right)
        {
            if (left.Length != right.Length)
                return false;

            int i = 0;
            foreach (byte c in left)
            {
                if (c != right[i])
                    return false;

                i++;
            }

            return true;
        }

        /// <summary>
        /// 获取整数大小。
        /// </summary>
        /// <param name="br">给定的二进制读取器。</param>
        /// <returns>返回整数。</returns>
        protected virtual int GetIntegerSize(BinaryReader br)
        {
            byte b = 0;
            byte lowByte = 0x00;
            byte highByte = 0x00;
            int count = 0;

            b = br.ReadByte();
            if (b != 0x02)
                return 0;

            b = br.ReadByte();
            if (b == 0x81)
            {
                count = br.ReadByte();
            }
            else if (b == 0x82)
            {
                highByte = br.ReadByte();
                lowByte = br.ReadByte();

                byte[] modint = { lowByte, highByte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = b;
            }

            while (br.ReadByte() == 0x00)
            {
                count -= 1;
            }

            br.BaseStream.Seek(-1, SeekOrigin.Current);

            return count;
        }


        /// <summary>
        /// 生成 RSA 公钥。
        /// </summary>
        /// <param name="publicKeyString">给定的公钥字符串（可选）。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        /// <returns>返回参数。</returns>
        public override RSAParameters GenerateRsaPublicKey(string publicKeyString = null,
            Encoding encoding = null)
        {
            byte[] seqOID =
            {
                0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86,
                0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00
            };

            byte[] seq = new byte[15];

            // x509
            byte[] buffer;
            int x509size;

            buffer = ByteConverter.FromString(publicKeyString.As(_defaultPublicKeyString));
            x509size = buffer.Length;

            var parameters = new RSAParameters();

            using (var ms = new MemoryStream(buffer))
            {
                using (var br = new BinaryReader(ms, encoding.As(Encoding.UTF8)))
                {
                    byte b = 0;
                    ushort twoBytes = 0;

                    twoBytes = br.ReadUInt16();
                    if (twoBytes == 0x8130)
                        br.ReadByte();
                    else if (twoBytes == 0x8230)
                        br.ReadInt16();
                    else
                        return parameters;

                    seq = br.ReadBytes(15);
                    if (!CompareBytes(seq, seqOID))
                        return parameters;

                    twoBytes = br.ReadUInt16();
                    if (twoBytes == 0x8103)
                        br.ReadByte();
                    else if (twoBytes == 0x8203)
                        br.ReadInt16();
                    else
                        return parameters;

                    b = br.ReadByte();
                    if (b != 0x00)
                        return parameters;

                    twoBytes = br.ReadUInt16();
                    if (twoBytes == 0x8130)
                        br.ReadByte();
                    else if (twoBytes == 0x8230)
                        br.ReadInt16();
                    else
                        return parameters;

                    twoBytes = br.ReadUInt16();
                    byte lowbyte = 0x00;
                    byte highbyte = 0x00;

                    if (twoBytes == 0x8102)
                    {
                        lowbyte = br.ReadByte();
                    }
                    else if (twoBytes == 0x8202)
                    {
                        highbyte = br.ReadByte();
                        lowbyte = br.ReadByte();
                    }
                    else
                    {
                        return parameters;
                    }

                    byte[] modBytes = { lowbyte, highbyte, 0x00, 0x00 };
                    int modSize = BitConverter.ToInt32(modBytes, 0);

                    int firstByte = br.PeekChar();
                    if (firstByte == 0x00)
                    {
                        br.ReadByte();
                        modSize -= 1;
                    }

                    var modulus = br.ReadBytes(modSize);

                    if (br.ReadByte() != 0x02)
                        return parameters;

                    int expbytes = br.ReadByte();

                    parameters.Modulus = modulus;
                    parameters.Exponent = br.ReadBytes(expbytes);
                }
            }

            return parameters;
        }

        /// <summary>
        /// 生成 RSA 私钥。
        /// </summary>
        /// <param name="privateKeyString">给定的私钥字符串（可选）。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        /// <returns>返回参数。</returns>
        public override RSAParameters GenerateRsaPrivateKey(string privateKeyString = null,
            Encoding encoding = null)
        {
            var parameters = new RSAParameters();

            var buffer = ByteConverter.FromString(privateKeyString.As(_defaultPrivateKeyString));
            using (var ms = new MemoryStream(buffer))
            {
                using (var br = new BinaryReader(ms, encoding.As(Encoding.UTF8)))
                {
                    byte b = 0;
                    ushort twoBytes = 0;

                    twoBytes = br.ReadUInt16();
                    if (twoBytes == 0x8130)
                        br.ReadByte();
                    else if (twoBytes == 0x8230)
                        br.ReadInt16();
                    else
                        throw new Exception("Unexpected value read br.ReadUInt16()");

                    twoBytes = br.ReadUInt16();
                    if (twoBytes != 0x0102)
                        throw new Exception("Unexpected version");

                    b = br.ReadByte();
                    if (b != 0x00)
                        throw new Exception("Unexpected value read binr.ReadByte()");

                    parameters.Modulus = br.ReadBytes(GetIntegerSize(br));
                    parameters.Exponent = br.ReadBytes(GetIntegerSize(br));
                    parameters.D = br.ReadBytes(GetIntegerSize(br));
                    parameters.P = br.ReadBytes(GetIntegerSize(br));
                    parameters.Q = br.ReadBytes(GetIntegerSize(br));
                    parameters.DP = br.ReadBytes(GetIntegerSize(br));
                    parameters.DQ = br.ReadBytes(GetIntegerSize(br));
                    parameters.InverseQ = br.ReadBytes(GetIntegerSize(br));
                }
            }

            return parameters;
        }

    }
}
