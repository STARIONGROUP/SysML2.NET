// -------------------------------------------------------------------------------------------------
// <copyright file="ChecksumKind.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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
