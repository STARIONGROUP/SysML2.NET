// -------------------------------------------------------------------------------------------------
// <copyright file="Record.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PIM.POCO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Common;
    using SysML2.NET.Decorators;
    using SysML2.NET.PIM;

    /// <summary>
    /// A subclass of <see cref="Record"/> that represents a container for other <see cref="Record"/>s and
    /// an entry point for version management and data navigation
    /// </summary>
    public class Project : Record
    {
        /// <summary>
        /// Gets or sets a human-friendly identifier for a Project
        /// </summary>
        [Property(lowerValue: 1, upperValue: 1)]
        [RedefinedProperty(propertyName: "Record.Name")]
        public new string Name { get; set; }

        /// <summary>
        /// Gets or sets the timestamp at which the <see cref="Project"/> was created
        /// </summary>
        [Property(lowerValue: 1, upperValue: 1)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets the set of <see cref="DataIdentity"/> records corresponding to the <see cref="IData"/>
        /// contained in the <see cref="Project"/>
        /// </summary>
        /// <remarks>
        /// this is a derived attribute
        /// </remarks>
        [Property(lowerValue: 0, upperValue: int.MaxValue, isDerived: true)]
        public IEnumerable<DataIdentity> IdentifiedData => throw new NotImplementedException();

        /// <summary>
        /// Gets all the <see cref="Commit"/>s in the <see cref="Project"/>
        /// </summary>
        [Property(lowerValue: 0, upperValue: int.MaxValue)]
        public List<Commit> Commits { get; set; } = [];

        /// <summary>
        /// Gets or sets all <see cref="CommitReference"/>s in the <see cref="Project"/>
        /// </summary>
        [Property(lowerValue: 0, upperValue: int.MaxValue)]
        public List<CommitReference> CommitReferences { get; set; } = [];

        /// <summary>
        /// Gets all the branches in the <see cref="Project"/> which is a subset of <see cref="CommitReferences"/>
        /// </summary>
        [Property(lowerValue: 1, upperValue: int.MaxValue, isDerived: true)]
        [SubsettedProperty(propertyName: "Project.CommitReferences")]
        public IEnumerable<Branch> Branches => this.CommitReferences.OfType<Branch>();

        /// <summary>
        /// Gets or sets the default <see cref="Branch"/> in the <see cref="Project"/> which is a subset of <see cref="Branch"/>
        /// </summary>
        [Property(lowerValue: 1, upperValue: 1)]
        [SubsettedProperty(propertyName: "Project.Branches")]
        public Branch DefaultBranch { get; set; }

        /// <summary>
        /// Gets all the <see cref="Tag"/>s in the <see cref="Project"/> which is a subset of <see cref="CommitReferences"/>
        /// </summary>
        [Property(lowerValue: 0, upperValue: int.MaxValue, isDerived: true)]
        [SubsettedProperty(propertyName: "Project.CommitReferences")]
        public IEnumerable<Tag> Tags => this.CommitReferences.OfType<Tag>();

        /// <summary>
        /// Gets or sets the <see cref="Query"/> records owned by the <see cref="Project"/>. Each <see cref="Query"/> record represents a
        /// saved <see cref="Query"/> for the given <see cref="Project"/>.
        /// </summary>
        [Property(lowerValue: 0, upperValue: int.MaxValue)]
        public List<Query> Queries { get; set; } = [];
    }
}
