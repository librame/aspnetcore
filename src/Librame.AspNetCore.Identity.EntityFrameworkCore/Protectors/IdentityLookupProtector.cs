#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Librame.AspNetCore.Identity.Protectors
{
    using Extensions;
    using Extensions.Core.Tokens;
    using Extensions.Encryption.Services;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityLookupProtector : ILookupProtector
    {
        private readonly ILookupProtectorKeyRing _keyRing;
        private readonly ISymmetricService _symmetric;

        private readonly Encoding _encoding;


        public IdentityLookupProtector(ILookupProtectorKeyRing keyRing,
            ISymmetricService symmetric)
        {
            _keyRing = keyRing.NotNull(nameof(keyRing));
            _symmetric = symmetric.NotNull(nameof(symmetric));

            _encoding = ExtensionSettings.Preference.DefaultEncoding;
        }


        public string Protect(string keyId, string data)
        {
            SecurityToken.TryGetToken(_keyRing[keyId], out var token);

            var buffer = data.FromEncodingString(_encoding);
            buffer = _symmetric.EncryptAes(buffer, token);

            return buffer.AsBase64String();
        }

        public string Unprotect(string keyId, string data)
        {
            SecurityToken.TryGetToken(_keyRing[keyId], out var token);

            var buffer = data.FromBase64String();
            buffer = _symmetric.DecryptAes(buffer, token);

            return buffer.AsEncodingString(_encoding);
        }

    }
}
