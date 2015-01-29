// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;
using System.Text;

namespace Librame.Security.Keying
{
    class HMACSHA384KeyedAlgorithm : KeyedAlgorithmBase, IKeyedAlgorithm
    {
        public HMACSHA384KeyedAlgorithm(byte[] key, Encoding encoding = null)
            : base(key, encoding)
        {
        }
        public HMACSHA384KeyedAlgorithm(UniqueIdentity unique, Encoding encoding = null)
            : this(new KeyIdentity(unique, BitSize._384), encoding)
        {
        }
        HMACSHA384KeyedAlgorithm(KeyIdentity keyId, Encoding encoding = null)
            : base(keyId, encoding)
        {
            // KeySize = 384
        }


        protected override KeyedHashAlgorithm CreateAlgorithm(byte[] key)
        {
            return new HMACSHA384(key);
        }

    }
}