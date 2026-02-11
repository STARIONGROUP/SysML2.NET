// -------------------------------------------------------------------------------------------------
// <copyright file="InterchangeProjectUsage.cs" company="Starion Group S.A.">
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

    /// <summary>
    /// Describes a used project entry in an <see cref="InterchangeProject"/> usage list.
    /// </summary>
    public class InterchangeProjectUsage : ProjectUsageBase
    {
        /// <summary>
        /// Gets or sets the IRI identifying the project being used.
        /// </summary>
        /// <remarks>
        /// Mandatory (within a usage).
        /// <para/>
        /// If the IRI is dereferenceable, it SHOULD resolve to a project interchange file
        /// for the used project.
        /// </remarks>
        public Uri Resource { get; set; } = new Uri("about:blank");
        
        /// <summary>
        /// A constraint on the allowable versions of a used project.
        /// </summary>
        public string VersionConstraint { get; set; }
    }
}
