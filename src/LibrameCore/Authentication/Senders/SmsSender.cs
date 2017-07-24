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
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibrameCore.Authentication.Senders
{
    /// <summary>
    /// 短信发送器。
    /// </summary>
    public class SmsSender : ISmsSender
    {

        /// <summary>
        /// 异步发送。
        /// </summary>
        /// <param name="url">给定的短信网关 URL。</param>
        /// <param name="data">给定要发送的数据。</param>
        /// <param name="encoding">给定的字符编码（可选；默认为 UTF8）。</param>
        /// <param name="continueTimeoutMS">给定在接收到来自服务器的 100 次连续响应之前要等待的超时（以毫秒为单位）。</param>
        /// <returns>返回响应的字符串。</returns>
        public async Task<string> SendAsync(string url, string data,
            Encoding encoding = null, int continueTimeoutMS = 60000)
        {
            if (encoding == null) encoding = Encoding.UTF8;

            try
            {
                var hwr = WebRequest.CreateHttp(new Uri(url));
                hwr.ContentType = "application/x-www-form-urlencoded;charset=" + encoding.AsName();
                hwr.Method = "POST";
                hwr.Accept = "text/xml,text/javascript";
                hwr.ContinueTimeout = continueTimeoutMS;

                var buffer = encoding.GetBytes(data);
                using (var s = await hwr.GetRequestStreamAsync())
                {
                    s.Write(buffer, 0, buffer.Length);
                }

                using (var r = (HttpWebResponse)await hwr.GetResponseAsync())
                {
                    // 以字符流的方式读取 HTTP 响应
                    using (var rs = r.GetResponseStream())
                    {
                        var sr = new StreamReader(rs, encoding);

                        return sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
