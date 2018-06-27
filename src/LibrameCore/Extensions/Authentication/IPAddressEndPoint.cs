#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Utilities;
using System.Net;
using System.Text;

namespace LibrameCore.Extensions.Authentication
{
    /// <summary>
    /// IP 地址端点。
    /// </summary>
    public class IPAddressEndPoint
    {
        /// <summary>
        /// 默认主机。
        /// </summary>
        public const string DEFAULT_HOST = "localhost";

        /// <summary>
        /// 默认端口。
        /// </summary>
        public const int DEFAULT_PORT = 80;


        /// <summary>
        /// 构造一个 <see cref="IPAddressEndPoint"/> 默认实例。
        /// </summary>
        public IPAddressEndPoint()
            : this(DEFAULT_HOST)
        {
        }
        /// <summary>
        /// 构造一个 <see cref="IPAddressEndPoint"/> 实例。
        /// </summary>
        /// <param name="host">给定的主机。</param>
        public IPAddressEndPoint(string host)
            : this(host, DEFAULT_PORT)
        {
        }
        /// <summary>
        /// 构造一个 <see cref="IPAddressEndPoint"/> 实例。
        /// </summary>
        /// <param name="host">给定的主机。</param>
        /// <param name="port">给定的端口号。</param>
        public IPAddressEndPoint(string host, int port)
        {
            Host = host;
            Port = port;
        }


        private string _host = null;
        /// <summary>
        /// 主机。
        /// </summary>
        public string Host
        {
            get { return _host.AsOrDefault(DEFAULT_HOST); }
            set { _host = value.NotEmpty(nameof(value)); }
        }

        private int _port = 0;
        /// <summary>
        /// 端口。
        /// </summary>
        public int Port
        {
            get { return _port.AsOrDefault(DEFAULT_PORT, p => p == 0); }
            set { _port = value.NotOutOfRange(IPEndPoint.MinPort, IPEndPoint.MaxPort, nameof(value)); }
        }


        /// <summary>
        /// 指定的端点对象是否相等。
        /// </summary>
        /// <param name="obj">给定的端点对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is IPAddressEndPoint))
                return false;

            var ep = (obj as IPAddressEndPoint);

            return (ep.Host == Host && ep.Port == Port);
        }


        /// <summary>
        /// 获取哈希代码。
        /// </summary>
        /// <returns>返回整数。</returns>
        public override int GetHashCode()
        {
            return (Host.GetHashCode() ^ Port.GetHashCode());
        }


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
        {
            var sb = new StringBuilder(Host);

            if (Port != DEFAULT_PORT)
            {
                sb.Append(":").Append(Port);
            }

            return sb.ToString();
        }


        /// <summary>
        /// 解析指定的端点字符串。
        /// </summary>
        /// <param name="endPoint">给定的端点字符串。</param>
        /// <returns>返回端点选项。</returns>
        public static IPAddressEndPoint Parse(string endPoint)
        {
            endPoint.NotEmpty(nameof(endPoint));

            if (endPoint.Contains(":"))
            {
                var pair = endPoint.SplitPair(":");

                return new IPAddressEndPoint(pair.Key, pair.Value.AsOrDefault(DEFAULT_PORT, v => int.Parse(v)));
            }

            return new IPAddressEndPoint(endPoint);
        }


        #region Operators

        /// <summary>
        /// 判断两个端点选项是否相等。
        /// </summary>
        /// <param name="ep1">给定的端点选项1。</param>
        /// <param name="ep2">给定的端点选项2。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator ==(IPAddressEndPoint ep1, IPAddressEndPoint ep2)
        {
            if (ep1.Equals(null) || ep2.Equals(null))
                return false;

            return (ep1.ToString() == ep2.ToString());
        }

        /// <summary>
        /// 判断两个端点选项是否不相等。
        /// </summary>
        /// <param name="ep1">给定的端点选项1。</param>
        /// <param name="ep2">给定的端点选项2。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator !=(IPAddressEndPoint ep1, IPAddressEndPoint ep2)
        {
            return !(ep1 == ep2);
        }


        /// <summary>
        /// 判断端点选项是否有效。
        /// </summary>
        /// <param name="ep">给定的端点选项。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator true(IPAddressEndPoint ep)
        {
            return (!ep.Equals(null));
        }

        /// <summary>
        /// 判断端点选项是否无效。
        /// </summary>
        /// <param name="ep">给定的端点选项。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator false(IPAddressEndPoint ep)
        {
            return (ep.Equals(null));
        }

        #endregion

    }
}
