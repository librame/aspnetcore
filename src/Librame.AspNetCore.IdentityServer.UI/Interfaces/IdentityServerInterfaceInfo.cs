#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.IdentityServer.UI
{
    using AspNetCore.UI;
    using Extensions.Core;

    /// <summary>
    /// 身份服务器界面信息。
    /// </summary>
    public class IdentityServerInterfaceInfo : AbstractInterfaceInfo
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityServerInterfaceInfo"/>。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactoryDelegate"/>。</param>
        public IdentityServerInterfaceInfo(ServiceFactoryDelegate serviceFactory)
            : base(serviceFactory)
        {
        }


        /// <summary>
        /// 名称。
        /// </summary>
        public override string Name
            => nameof(IdentityServer);

        /// <summary>
        /// 标题。
        /// </summary>
        public override string Title
            => "身份服务器";
    }
}
