// -------------------------------------------------------------------------------------------------
// <copyright file="InterchangeProject.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;

    /// <summary>
    /// KerML interchange project information stored in <c>.project.json</c>.
    /// </summary>
    /// <remarks>
    /// The <c>.project.json</c> file contains the InterchangeProject information (Figure 42), serialized as a single JSON object
    /// according to the normative schema referenced by the specification.
    /// </remarks>
    public class InterchangeProject : ProjectBase
    {
        /// <summary>
        /// The version of the project being interchanged.
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// The license by which project content may be used.
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// A list of names of maintainers of the project.
        /// </summary>
        public List<string> Maintainer { get; set; } = [];
        
        /// <summary>
        /// An IRI for a Web site with further information on the project.
        /// </summary>
        public string Website { get; set; }
        
        /// <summary>
        /// A list of topics relevant to the project.
        /// </summary>
        public List<string> Topic { get; set; } = [];

        /// <summary>
        /// A list of project usage entries, one for each project used by the project
        /// being interchanged, with properties as given below.
        /// </summary>
        public List<InterchangeProjectUsage> Usage { get; set; } = [];
    }
}
