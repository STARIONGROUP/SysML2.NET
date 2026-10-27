// ------------------------------------------------------------------------------------------------
// <copyright file="ChecksumKind.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.ModelInterchange
{
    /// <summary>
    /// Identifies the algorithm used to compute a checksum value in an interchange project.
    /// </summary>
    public enum ChecksumKind
    {
        /// <summary>
        /// SHA-1 hash algorithm.
        /// </summary>
        SHA1,

        /// <summary>
        /// SHA-224 hash algorithm.
        /// </summary>
        SHA224,

        /// <summary>
        /// SHA-256 hash algorithm.
        /// </summary>
        SHA256,

        /// <summary>
        /// SHA-384 hash algorithm.
        /// </summary>
        SHA384,

        /// <summary>
        /// SHA3-256 hash algorithm.
        /// </summary>
        SHA3256,

        /// <summary>
        /// SHA3-384 hash algorithm.
        /// </summary>
        SHA3384,

        /// <summary>
        /// SHA3-512 hash algorithm.
        /// </summary>
        SHA3512,

        /// <summary>
        /// BLAKE2b-256 hash algorithm.
        /// </summary>
        BLAKE2b256,

        /// <summary>
        /// BLAKE2b-384 hash algorithm.
        /// </summary>
        BLAKE2b384,

        /// <summary>
        /// BLAKE2b-512 hash algorithm.
        /// </summary>
        BLAKE2b512,

        /// <summary>
        /// BLAKE3 hash algorithm.
        /// </summary>
        BLAKE3,

        /// <summary>
        /// MD2 hash algorithm.
        /// </summary>
        MD2,

        /// <summary>
        /// MD4 hash algorithm.
        /// </summary>
        MD4,

        /// <summary>
        /// MD5 hash algorithm.
        /// </summary>
        MD5,

        /// <summary>
        /// MD6 hash algorithm.
        /// </summary>
        MD6,

        /// <summary>
        /// ADLER32 checksum algorithm.
        /// </summary>
        ADLER32
    }
}
