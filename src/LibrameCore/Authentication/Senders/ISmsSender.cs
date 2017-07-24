#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Text;
using System.Threading.Tasks;

namespace LibrameCore.Authentication.Senders
{
    /// <summary>
    /// 短信发送器接口。
    /// </summary>
    public interface ISmsSender
    {
        /// <summary>
        /// 异步发送。
        /// </summary>
        /// <param name="url">给定的短信网关 URL。</param>
        /// <param name="data">给定要发送的数据。</param>
        /// <param name="encoding">给定的字符编码（可选；默认为 UTF8）。</param>
        /// <param name="continueTimeoutMS">给定在接收到来自服务器的 100 次连续响应之前要等待的超时（以毫秒为单位）。</param>
        /// <returns>返回响应的字符串。</returns>
        Task<string> SendAsync(string url, string data,
            Encoding encoding = null, int continueTimeoutMS = 60000);
    }
}
