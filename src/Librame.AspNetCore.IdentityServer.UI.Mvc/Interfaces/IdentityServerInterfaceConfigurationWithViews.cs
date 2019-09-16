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

    class IdentityServerInterfaceConfigurationWithViews : InterfaceConfiguration
    {
        public IdentityServerInterfaceConfigurationWithViews(IApplicationContext context)
            : base(context, nameof(IdentityServer))
        {
        }

    }
}
