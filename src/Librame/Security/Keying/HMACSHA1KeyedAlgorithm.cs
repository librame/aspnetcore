// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;
using System.Text;

namespace Librame.Security.Keying
{
    class HMACSHA1KeyedAlgorithm : KeyedAlgorithmBase, IKeyedAlgorithm
    {
        public HMACSHA1KeyedAlgorithm(byte[] key, Encoding encoding = null)
            : base(key, encoding)
        {
        }
        public HMACSHA1KeyedAlgorithm(UniqueIdentity unique, Encoding encoding = null)
            : this(new KeyIdentity(unique, BitSize._160), encoding)
        {
        }
        HMACSHA1KeyedAlgorithm(KeyIdentity keyId, Encoding encoding = null)
            : base(keyId, encoding)
        {
            // KeySize = 160
        }


        protected override KeyedHashAlgorithm CreateAlgorithm(byte[] key)
        {
            return new HMACSHA1(key);
        }

    }
}