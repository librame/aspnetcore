// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;
using System.Text;

namespace Librame.Security.Symmetry
{
    class AesSymmetryAlgorithm : SymmetryAlgorithmBase, ISymmetryAlgorithm
    {
        public AesSymmetryAlgorithm(byte[] key, byte[] iv, Encoding encoding = null)
            : base(key, iv, encoding)
        {
        }
        public AesSymmetryAlgorithm(UniqueIdentity unique, Encoding encoding = null)
            : this(new KeyIdentity(unique, BitSize._256), new IvIdentity(unique, BitSize._128), encoding)
        {
        }
        AesSymmetryAlgorithm(KeyIdentity keyId, IvIdentity ivId, Encoding encoding = null)
            : base(keyId, ivId, encoding)
        {
            // KeySize = 256
            // IvSize = 128
        }
        

        protected override SymmetricAlgorithm CreateAlgorithm()
        {
            return new AesManaged();
        }

    }
}