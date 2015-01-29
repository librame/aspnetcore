// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;
using System.Text;

namespace Librame.Security.Keying
{
    class MACTripleDESKeyedAlgorithm : KeyedAlgorithmBase, IKeyedAlgorithm
    {
        public MACTripleDESKeyedAlgorithm(byte[] key, Encoding encoding = null)
            : base(key, encoding)
        {
        }
        public MACTripleDESKeyedAlgorithm(UniqueIdentity unique, Encoding encoding = null)
            : this(new KeyIdentity(unique, BitSize._192), encoding)
        {
        }
        MACTripleDESKeyedAlgorithm(KeyIdentity keyId, Encoding encoding = null)
            : base(keyId, encoding)
        {
            // KeySize = 192
        }
        

        protected override KeyedHashAlgorithm CreateAlgorithm(byte[] key)
        {
            return new MACTripleDES(key);
        }

    }
}