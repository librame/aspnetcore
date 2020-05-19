#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Web.Services
{
    using AspNetCore.Web.Builders;
    using Extensions;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class UserPortraitService : IUserPortraitService
    {
        private readonly WebBuilderOptions _options;


        public UserPortraitService(IOptions<WebBuilderOptions> options)
        {
            _options = options.NotNull(nameof(options)).Value;
        }


        public Task<string> GetPortraitPathAsync(dynamic user, CancellationToken cancellationToken = default)
            => Task.FromResult(_options.DefaultUserPortraitPath);

    }
}
