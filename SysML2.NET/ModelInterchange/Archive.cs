// -------------------------------------------------------------------------------------------------
// <copyright file="Archive.cs" company="Starion Group S.A.">
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
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the logical contents of a <c>.kpar</c> archive.
    /// </summary>
    public class Archive
    {
        /// <summary>
        /// Gets or sets the interchange project descriptor (serialized to <c>.project.json</c>).
        /// </summary>
        public InterchangeProject Project { get; set; }

        /// <summary>
        /// Gets or sets the interchange metadata (serialized to <c>.meta.json</c>).
        /// </summary>
        public InterchangeProjectMetadata Metadata { get; set; }
        
        /// <summary>
        /// Gets the Path of the kpar that has been read
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets the model interchange files contained in the archive.
        /// </summary>
        /// <remarks>
        /// Each entry uses an archive-relative path (forward slashes).
        /// </remarks>
        public IReadOnlyList<ModelEntry> Models { get; set; } = Array.Empty<ModelEntry>();
        
        /// <summary>
        /// Gets the checksum validation mismatches detected while reading a <c>.kpar</c> archive.
        /// </summary>
        public IReadOnlyList<ChecksumMismatch> ChecksumMismatches { get; set; } = Array.Empty<ChecksumMismatch>();
    }
}
