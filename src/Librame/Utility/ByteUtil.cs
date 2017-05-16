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
using System.Runtime.InteropServices;
using System.Text;

namespace Librame.Utility
{
    /// <summary>
    /// <see cref="Byte"/> 实用工具。
    /// </summary>
    public static class ByteUtil
    {
        /// <summary>
        /// 将对象转换为字节数组。
        /// </summary>
        /// <param name="obj">给定的对象。</param>
        /// <returns>返回字节数组。</returns>
        public static byte[] AsBytes(this object obj)
        {
            obj.NotNull(nameof(obj));

            try
            {
                // 对象类名需设置 [StructLayout(LayoutKind.Sequential)] 属性特性，否则会抛出异常
                var buffer = new byte[Marshal.SizeOf(obj)];
                var ip = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);

                // 此方法比 [Serializable] 序列化方法生成的字节数组短了很多，非常节省空间
                Marshal.StructureToPtr(obj, ip, true);

                return buffer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将字节数组还原为对象。
        /// </summary>
        /// <typeparam name="T">给定的类型。</typeparam>
        /// <param name="bytes">给定的字节数组。</param>
        /// <returns>返回对象。</returns>
        public static T FromBytes<T>(this byte[] bytes)
        {
            return (T)bytes.FromBytes(typeof(T));
        }
        /// <summary>
        /// 将字节数组还原为对象。
        /// </summary>
        /// <param name="bytes">给定的字节数组。</param>
        /// <param name="type">给定的类型。</param>
        /// <returns>返回对象。</returns>
        public static object FromBytes(this byte[] bytes, Type type)
        {
            try
            {
                var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);

                return Marshal.PtrToStructure(ptr, type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 转换字节序列为 BASE64。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回 BASE64 字符串。</returns>
        public static string AsBase64(this byte[] buffer)
        {
            buffer.NotNull(nameof(buffer));

            try
            {
                return Convert.ToBase64String(buffer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 还原 BASE64 为字节序列。
        /// </summary>
        /// <param name="base64">给定的 BASE64 字符串。</param>
        /// <returns>返回字节数组。</returns>
        public static byte[] FromBase64(this string base64)
        {
            base64.NotNullOrEmpty(nameof(base64));

            try
            {
                return Convert.FromBase64String(base64);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 转换字节序列为十六进制。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回十六进制字符串。</returns>
        public static string AsHex(this byte[] buffer)
        {
            buffer.NotNull(nameof(buffer));

            try
            {
                var sb = new StringBuilder();

                if (buffer != null || buffer.Length == 0)
                {
                    // 同 BitConverter.ToString(buffer).Replace("-", string.Empty);
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        sb.Append(buffer[i].ToString("X2"));
                    }
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 还原十六进制为字节序列。
        /// </summary>
        /// <param name="hex">给定的十六进制字符串。</param>
        /// <returns>返回字节数组。</returns>
        public static byte[] FromHex(this string hex)
        {
            hex.NotNull(nameof(hex));

            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException("hex length must be in multiples of 2.");
            }

            try
            {
                int length = hex.Length / 2;
                var buffer = new byte[length];

                for (int i = 0; i < length; i++)
                {
                    buffer[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }

                return buffer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
