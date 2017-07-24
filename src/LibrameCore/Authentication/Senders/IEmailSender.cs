#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using MimeKit;
using System;
using System.Threading.Tasks;

namespace LibrameCore.Authentication.Senders
{
    /// <summary>
    /// 邮箱发送器接口。
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// 异步发送。
        /// </summary>
        /// <param name="subject">给定的主题。</param>
        /// <param name="message">给定的消息。</param>
        /// <param name="toName">给定的收件人名称。</param>
        /// <param name="toAddress">给定的收件人地址。</param>
        /// <param name="fromAction">给定的发件人动作方法（可选）。</param>
        /// <returns>返回异步操作。</returns>
        Task SendAsync(string subject, string message,
            string toName, string toAddress,
            Action<InternetAddressList> fromAction = null);

        /// <summary>
        /// 异步发送。
        /// </summary>
        /// <param name="subject">给定的主题。</param>
        /// <param name="message">给定的消息。</param>
        /// <param name="toAction">给定的收件人动作方法。</param>
        /// <param name="fromAction">给定的发件人动作方法（可选）。</param>
        /// <returns>返回异步操作。</returns>
        Task SendAsync(string subject, string message,
            Action<InternetAddressList> toAction,
            Action<InternetAddressList> fromAction = null);
    }
}
