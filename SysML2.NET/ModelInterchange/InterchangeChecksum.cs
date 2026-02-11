// -------------------------------------------------------------------------------------------------
// <copyright file="InterchangeChecksum.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
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
    /// Represents a checksum entry for a model interchange file.
    /// </summary>
    public sealed class InterchangeChecksum
    {
        /// <summary>
        /// Gets or sets the checksum value computed for the file.
        /// </summary>
        /// <remarks>
        /// Mandatory. Hexadecimal-encoded string.
        /// </remarks>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the algorithm used to compute the checksum.
        /// </summary>
        /// <remarks>
        /// Mandatory. Must match one of the supported algorithms defined by <see cref="ChecksumKind"/>.
        /// </remarks>
        public ChecksumKind Algorithm { get; set; }
    }
}
