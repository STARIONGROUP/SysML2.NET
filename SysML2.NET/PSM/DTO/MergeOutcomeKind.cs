// -------------------------------------------------------------------------------------------------
// <copyright file="MergeOutcomeKind.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PSM.DTO
{
    /// <summary>
    /// Enumeration of the alternatives that a <see cref="MergeOutcome"/> may hold
    /// </summary>
    public enum MergeOutcomeKind
    {
        /// <summary>
        /// The merge succeeded and the outcome holds the resulting commit
        /// </summary>
        Merged,

        /// <summary>
        /// The merge failed and the outcome holds the data identities that conflict
        /// </summary>
        Conflicted
    }
}
