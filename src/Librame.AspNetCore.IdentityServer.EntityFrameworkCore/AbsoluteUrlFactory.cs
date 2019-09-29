// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System;

namespace Librame.AspNetCore.IdentityServer
{
    using Extensions;

    class AbsoluteUrlFactory : IAbsoluteUrlFactory
    {
        public AbsoluteUrlFactory(IHttpContextAccessor httpContextAccessor)
        {
            // We need the context accessor here in order to produce an absolute url from a potentially relative url.
            ContextAccessor = httpContextAccessor;
        }


        public IHttpContextAccessor ContextAccessor { get; }


        // Call this method when you are overriding a service that doesn't have an HttpContext instance available.
        public string GetAbsoluteUrl(string path)
        {
            if (ContextAccessor.HttpContext?.Request == null)
                throw new InvalidOperationException("The request is not currently available. This service can only be used within the context of an existing HTTP request.");

            return GetAbsoluteUrl(ContextAccessor.HttpContext, path);
        }

        // Call this method when you are implementing a service that has an HttpContext instance available.
        public string GetAbsoluteUrl(HttpContext context, string path)
        {
            if (!ShouldProcessPath(path))
                return path;

            var request = context.Request;
            return $"{request.Scheme}://{request.Host.ToUriComponent()}{request.PathBase.ToUriComponent()}{path}";
        }

        private bool ShouldProcessPath(string path)
        {
            if (path.IsNullOrEmpty() || !Uri.IsWellFormedUriString(path, UriKind.RelativeOrAbsolute))
                return false;

            if (Uri.IsWellFormedUriString(path, UriKind.Absolute))
                return false;

            return true;
        }
    }
}
