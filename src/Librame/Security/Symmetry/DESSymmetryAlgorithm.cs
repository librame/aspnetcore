// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;
using System.Text;

namespace Librame.Security.Symmetry
{
    class DESSymmetryAlgorithm : SymmetryAlgorithmBase, ISymmetryAlgorithm
    {
        public DESSymmetryAlgorithm(byte[] key, byte[] iv, Encoding encoding = null)
            : base(key, iv, encoding)
        {
        }
        public DESSymmetryAlgorithm(UniqueIdentity unique, Encoding encoding = null)
            : this(new KeyIdentity(unique, BitSize._64), new IvIdentity(unique, BitSize._64), encoding)
        {
        }
        DESSymmetryAlgorithm(KeyIdentity keyId, IvIdentity ivId, Encoding encoding = null)
            : base(keyId, ivId, encoding)
        {
            // KeySize = 64
            // IvSize = 64
        }
        

        protected override SymmetricAlgorithm CreateAlgorithm()
        {
            return new DESCryptoServiceProvider();
        }

    }
}