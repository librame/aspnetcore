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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Protectors
{
    using Extensions;
    using Extensions.Core.Identifiers;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityLookupProtectorKeyRing : ILookupProtectorKeyRing
    {
        private readonly ISecurityIdentifierKeyRing _keyRing;


        public IdentityLookupProtectorKeyRing(ISecurityIdentifierKeyRing keyRing)
        {
            _keyRing = keyRing.NotNull(nameof(keyRing));
        }


        public string this[string keyId]
        {
            get => _keyRing[keyId];
        }

        public string CurrentKeyId
        {
            get => _keyRing.CurrentIndex;
        }


        public IEnumerable<string> GetAllKeyIds()
            => _keyRing.GetAllIndexes();

    }
}
