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
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace LibrameCore.Authentication.Senders
{
    /// <summary>
    /// 邮箱发送器。
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly AuthenticationOptions _options;

        /// <summary>
        /// 构造一个邮箱发送器实例。
        /// </summary>
        /// <param name="options">给定的认证选项。</param>
        public EmailSender(IOptions<AuthenticationOptions> options)
        {
            _options = options.NotNull(nameof(options)).Value;
        }


        /// <summary>
        /// 异步发送。
        /// </summary>
        /// <param name="subject">给定的主题。</param>
        /// <param name="message">给定的消息。</param>
        /// <param name="toName">给定的收件人名称。</param>
        /// <param name="toAddress">给定的收件人地址。</param>
        /// <param name="fromAction">给定的发件人动作方法（可选）。</param>
        /// <returns>返回异步操作。</returns>
        public async Task SendAsync(string subject, string message,
            string toName, string toAddress,
            Action<InternetAddressList> fromAction = null)
        {
            Action<InternetAddressList> toAction
                = t => t.Add(new MailboxAddress(toName, toAddress));

            await SendAsync(subject, message, toAction, fromAction);
        }

        /// <summary>
        /// 异步发送。
        /// </summary>
        /// <param name="subject">给定的主题。</param>
        /// <param name="message">给定的消息。</param>
        /// <param name="toAction">给定的收件人动作方法。</param>
        /// <param name="fromAction">给定的发件人动作方法（可选）。</param>
        /// <returns>返回异步操作。</returns>
        public async Task SendAsync(string subject, string message,
            Action<InternetAddressList> toAction,
            Action<InternetAddressList> fromAction = null)
        {
            if (fromAction == null)
            {
                fromAction = f => f.Add(new MailboxAddress(_options.Smtp.Nickname,
                    _options.Smtp.Username));
            }

            var mime = new MimeMessage();

            fromAction.Invoke(mime.From);
            toAction.Invoke(mime.To);

            mime.Subject = subject;
            mime.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_options.Smtp.Server,
                    _options.Smtp.Port,
                    _options.Smtp.SecureSocket).ConfigureAwait(false);

                await client.AuthenticateAsync(_options.Smtp.Username,
                    _options.Smtp.Password);

                await client.SendAsync(mime).ConfigureAwait(false);

                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }

    }
}
