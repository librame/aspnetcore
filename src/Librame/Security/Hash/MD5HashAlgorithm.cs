// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;
using System.Text;

namespace Librame.Security.Hash
{
    class MD5HashAlgorithm : HashAlgorithmBase, IAlgorithm
    {
        public MD5HashAlgorithm(Encoding encoding = null)
            : base(encoding)
        {
        }
        

        protected override HashAlgorithm CreateAlgorithm()
        {
            return MD5.Create();
        }

    }
}