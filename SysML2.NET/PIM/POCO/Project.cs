// -------------------------------------------------------------------------------------------------
// <copyright file="Record.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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

namespace SysML2.NET.PIM.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.PIM;

    /// <summary>
    /// A subclass of <see cref="Record"/> that represents a container for other <see cref="Record"/>s and
    /// an entry point for version management and data navigation
    /// </summary>
    public class Project : Record
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        public Project()
        {
            this.Branch = new List<Branch>();
            this.Commit = new List<Commit>();
            this.CommitReference = new List<CommitReference>();
            this.Tag = new List<Tag>();
            this.Usage = new List<ProjectUsage>();
            this.Queries = new List<Query>();
        }

        /// <summary>
        /// Gets the set of <see cref="DataIdentity"/> records corresponding to the <see cref="IData"/>
        /// contained in the <see cref="Project"/>
        /// </summary>
        /// <remarks>
        /// this is a derived attribute
        /// </remarks>
        public IEnumerable<DataIdentity> IdentifiedData => throw new NotImplementedException();
        
        /// <summary>
        /// Gets or sets the set of all <see cref="Commit"/>s in the <see cref="Project"/>
        /// </summary>
        public List<Commit> Commit { get; set; }

        /// <summary>
        /// Gets or sets all <see cref="CommitReference"/>s in the <see cref="Project"/>
        /// </summary>
        public List<CommitReference> CommitReference { get; set; }

        /// <summary>
        /// Gets or sets all the branches in the Project which is a subset of <see cref="CommitReference"/>
        /// </summary>
        public List<Branch> Branch { get; set; }

        /// <summary>
        /// Gets or sets the default <see cref="Branch"/> in the <see cref="Project"/> which is a subset of <see cref="Branch"/>
        /// </summary>
        public Branch DefaultBranch { get; set; }

        /// <summary>
        /// Gets or sets the all the <see cref="Tag"/>s in the <see cref="Project"/> which is a subset of <see cref="CommitReference"/>
        /// </summary>
        public List<Tag> Tag { get; set; }

        /// <summary>
        /// Gets or sets <see cref="ProjectUsage"/> records representing all other <see cref="Project"/>s being used by the given <see cref="Project"/>
        /// </summary>
        public List<ProjectUsage> Usage { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Query"/> records owned by the <see cref="Project"/>. Each <see cref="Query"/> record represents a
        /// saved <see cref="Query"/> for the given <see cref="Project"/>.
        /// </summary>
        public List<Query> Queries { get; set; }
    }
}
