﻿// -------------------------------------------------------------------------------------------------
// <copyright file="InterchangeProjectMetadata.cs" company="Starion Group S.A.">
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
    /// KerML interchange project metadata stored in <c>.meta.json</c>.
    /// </summary>
    /// <remarks>
    /// The <c>.meta.json</c> file contains additional metadata for the project interchange archive, serialized as a single JSON object.
    /// Table 13 in Section 10.3 lists the fields.
    /// </remarks>
    public class InterchangeProjectMetadata
    {
        /// <summary>
        /// Gets or sets the index of the global scope of the project.
        /// </summary>
        /// <remarks>
        /// Mandatory. A JSON object with a key for each name, mapping to the path of the model interchange file containing the
        /// root namespace for the named element.
        /// </remarks>
        public Dictionary<string, string> Index { get; set; } = new(StringComparer.Ordinal);
        
        /// <summary>
        /// Gets or sets the creation timestamp of the project interchange file.
        /// </summary>
        /// <remarks>
        /// Mandatory. Stored as an ISO 8601 date-time string in the serialized form.
        /// </remarks>
        public DateTimeOffset Created { get; set; }
        
        /// <summary>
        /// Gets or sets an optional IRI identifying the metamodel (language) of the models in the project interchange file.
        /// </summary>
        /// <remarks>
        /// Optional. For OMG-standardized languages, this is the version-specific URI specified by OMG; for KerML it has the form
        /// <c>https://www.omg.org/spec/KerML/yyyymmxx</c>. If not given and the archive uses <c>.kpar</c>, the default is KerML.
        /// </remarks>
        public Uri Metamodel { get; set; }
        
        /// <summary>
        /// Gets or sets whether derived property values are included in the project’s XMI/JSON model interchange files.
        /// </summary>
        /// <remarks>
        /// Optional. If true, all derived properties must be included; if false, none may be included; if omitted, inclusion may vary.
        /// </remarks>
        public bool IncludesDerived { get; set; }

        /// <summary>
        /// Gets or sets whether implied relationships are included in the project’s XMI/JSON model interchange files.
        /// </summary>
        /// <remarks>
        /// Optional. If true, implied relationships must be included; if false, none may be included; if omitted, inclusion may vary.
        /// </remarks>
        public bool IncludesImplied { get; set; }
        
        /// <summary>
        /// Gets or sets the checksum dictionary for model interchange files.
        /// </summary>
        /// <remarks>
        /// Key = relative file path.
        /// Value = checksum information for that file.
        /// </remarks>
        public Dictionary<string, InterchangeChecksum> Checksum { get; set; } = new(StringComparer.Ordinal);
    }
}
