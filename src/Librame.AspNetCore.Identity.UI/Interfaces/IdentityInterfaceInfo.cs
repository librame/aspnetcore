#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;
    using Extensions.Core;

    /// <summary>
    /// 身份界面信息。
    /// </summary>
    public class IdentityInterfaceInfo : AbstractInterfaceInfo
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityInterfaceInfo"/>。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactoryDelegate"/>。</param>
        public IdentityInterfaceInfo(ServiceFactoryDelegate serviceFactory)
            : base(serviceFactory)
        {
        }


        /// <summary>
        /// 名称。
        /// </summary>
        public override string Name
            => nameof(Identity);

        /// <summary>
        /// 标题。
        /// </summary>
        public override string Title
            => "身份";
    }
}
