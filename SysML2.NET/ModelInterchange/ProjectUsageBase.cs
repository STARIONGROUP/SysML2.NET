// -------------------------------------------------------------------------------------------------
// <copyright file="ProjectUsageBase.cs" company="Starion Group S.A.">
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
    /// Base type for representing a “project usage” entry (a used project) in an interchange project.
    /// </summary>
    /// <remarks>
    /// Each usage entry identifies a used project via an IRI and may constrain which versions of that project are acceptable.
    /// </remarks>
    public abstract class ProjectUsageBase
    {
        /// <summary>
        /// TBD from Kerml Spec
        /// </summary>
        public ProjectBase UsingProject { get; set; }
        
        /// <summary>
        /// TBD from Kerml Spec
        /// </summary>
        public ProjectBase UsedProject { get; set; }
    }
}

