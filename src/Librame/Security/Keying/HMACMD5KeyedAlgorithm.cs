// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;
using System.Text;

namespace Librame.Security.Keying
{
    class HMACMD5KeyedAlgorithm : KeyedAlgorithmBase, IKeyedAlgorithm
    {
        public HMACMD5KeyedAlgorithm(byte[] key, Encoding encoding = null)
            : base(key, encoding)
        {
        }
        public HMACMD5KeyedAlgorithm(UniqueIdentity unique, Encoding encoding = null)
            : this(new KeyIdentity(unique, BitSize._128), encoding)
        {
        }
        HMACMD5KeyedAlgorithm(KeyIdentity keyId, Encoding encoding = null)
            : base(keyId, encoding)
        {
            // KeySize = 128
        }


        protected override KeyedHashAlgorithm CreateAlgorithm(byte[] key)
        {
            return new HMACMD5(key);
        }

    }
}