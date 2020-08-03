#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Security.Claims;

namespace Librame.AspNetCore.Api
{
    internal class GraphUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
