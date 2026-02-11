// -------------------------------------------------------------------------------------------------
// <copyright file="ReadOptions.cs" company="Starion Group S.A.">
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
    using System;

    /// <summary>
    /// Options controlling <c>.kpar</c> reading behavior.
    /// </summary>
    public sealed class ReadOptions
    {
        /// <summary>
        /// If true, the reader validates that <c>.project.json</c> and <c>.meta.json</c> exist at archive root and are unique.
        /// </summary>
        public bool ValidateRequiredDescriptors { get; set; } = true;

        /// <summary>
        /// If true, the reader ensures the <c>index</c> entries in metadata resolve to existing model files.
        /// </summary>
        public bool ValidateIndexPaths { get; set; } = true;
        
        /// <summary>
        /// If true, model file checksums declared in <c>.meta.json</c>
        /// are computed and validated.
        /// </summary>
        public bool ValidateChecksums { get; set; } = false;
        
        /// <summary>
        /// Controls how checksum mismatches are handled when
        /// <see cref="ValidateChecksums"/> is enabled.
        /// </summary>
        /// <remarks>
        /// Default behavior is <see cref="ChecksumFailureBehavior.Throw"/>,
        /// causing archive opening to fail immediately on integrity violation.
        /// </remarks>
        public ChecksumFailureBehavior ChecksumFailureBehavior { get; set; } = ChecksumFailureBehavior.Throw;
    }
}
