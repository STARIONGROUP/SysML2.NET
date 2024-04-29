// -------------------------------------------------------------------------------------------------
// <copyright file="Record.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2024 Starion Group S.A.
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

namespace SysML2.NET.PIM
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Common;

    /// <summary>
    /// A Record represents any data that is consumed (input) or produced (output) by the Systems Modeling API
    /// and Services.A Record is an abstract concept from which other concrete concepts inherit.
    /// </summary>
    public abstract class Record : IData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Record"/> class.
        /// </summary>
        protected Record()
        {
            Alias = new List<string>();
        }

        /// <summary>
        /// Gets or sets the unique identifier of the <see cref="Record"/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets string representation of the IRI for the <see cref="Record"/>
        /// </summary>
        public string ResourceIdentifier { get; set; }

        /// <summary>
        /// Gets or sets a collection of other identifiers for this record, especially if the record was created or represented
        /// in other software applications and systems
        /// </summary>
        public List<string> Alias { get; set; }

        /// <summary>
        /// Gets a human-friendly unique identifier for this <see cref="Record"/>
        /// </summary>
        public string QueryHumanIdentifier()
        {
            throw new NotImplementedException("Derived property HumanIdentifier not yet supported");
        }

        /// <summary>
        /// Gets or sets a statement that provides details about the <see cref="Record"/>.
        /// </summary>
        public string Description { get; set; }
    }
}
