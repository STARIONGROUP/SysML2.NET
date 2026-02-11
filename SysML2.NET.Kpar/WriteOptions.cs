// -------------------------------------------------------------------------------------------------
// <copyright file="WriteOptions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Kpar
{
    /// <summary>
    /// Options controlling <c>.kpar</c> writing behavior.
    /// </summary>
    public sealed class WriteOptions
    {
        /// <summary>
        /// If true, the writer normalizes paths to use forward slashes and strips leading slashes.
        /// </summary>
        public bool NormalizePaths { get; set; } = true;

        /// <summary>
        /// If true, the writer fails if any descriptor would be written outside the archive root.
        /// </summary>
        public bool EnforceDescriptorsAtRoot { get; set; } = true;

        /// <summary>
        /// Optional: compute and populate metadata checksums for some or all model files.
        /// </summary>
        public bool ComputeChecksums { get; set; } = false;

        /// <summary>
        /// Optional checksum algorithm name (e.g., <c>SHA256</c>) if <see cref="ComputeChecksums"/> is enabled.
        /// </summary>
        public string ChecksumAlgorithm { get; set; }
    }
}
