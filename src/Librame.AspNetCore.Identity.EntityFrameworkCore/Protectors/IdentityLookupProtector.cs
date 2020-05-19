#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Protectors
{
    using Extensions;
    using Extensions.Core.Builders;
    using Extensions.Core.Identifiers;
    using Extensions.Encryption.Services;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityLookupProtector : ILookupProtector
    {
        private readonly ILookupProtectorKeyRing _keyRing;
        private readonly ISymmetricService _symmetric;
        private readonly CoreBuilderOptions _coreOptions;


        public IdentityLookupProtector(ILookupProtectorKeyRing keyRing,
            ISymmetricService symmetric, IOptions<CoreBuilderOptions> coreOptions)
        {
            _keyRing = keyRing.NotNull(nameof(keyRing));
            _symmetric = symmetric.NotNull(nameof(symmetric));
            _coreOptions = coreOptions.NotNull(nameof(coreOptions)).Value;
        }


        public string Protect(string keyId, string data)
        {
            SecurityIdentifier.TryGetIdentifier(_keyRing[keyId], out var identifier);

            var buffer = data.FromEncodingString(_coreOptions.Encoding);
            buffer = _symmetric.EncryptAes(buffer, identifier);

            return buffer.AsBase64String();
        }

        public string Unprotect(string keyId, string data)
        {
            SecurityIdentifier.TryGetIdentifier(_keyRing[keyId], out var identifier);

            var buffer = data.FromBase64String();
            buffer = _symmetric.DecryptAes(buffer, identifier);

            return buffer.AsEncodingString(_coreOptions.Encoding);
        }

    }
}
