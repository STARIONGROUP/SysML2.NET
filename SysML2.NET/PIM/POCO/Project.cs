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
        [Property(xmiId: "_19_0_4_58901f1_1671293441918_865671_7736", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Record.Name")]
        public new string Name { get; set; }

        /// <summary>
        /// Gets or sets the timestamp at which the <see cref="Project"/> was created
        /// </summary>
        [Property(xmiId: "_19_0_4_f940371_1694730728649_158871_419", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets the set of <see cref="DataIdentity"/> records corresponding to the <see cref="IData"/>
        /// contained in the <see cref="Project"/>
        /// </summary>
        /// <remarks>
        /// this is a derived attribute
        /// </remarks>
        [Property(xmiId: "sysml2.net", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public IEnumerable<DataIdentity> identifiedData => this.ComputeIdentifiedData();

        /// <summary>
        /// Gets all the <see cref="Commit"/>s in the <see cref="Project"/>
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469733_493524_66075", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<Commit> Commits { get; set; } = [];

        /// <summary>
        /// Gets or sets all <see cref="CommitReference"/>s in the <see cref="Project"/>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628779129336_898482_712", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<CommitReference> CommitReferences { get; set; } = [];

        /// <summary>
        /// Gets all the branches in the <see cref="Project"/> which is a subset of <see cref="CommitReferences"/>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1614694201382_729306_245", aggregation: AggregationKind.None, lowerValue: 1, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Project.CommitReferences")]
        public IEnumerable<Branch> Branches => this.CommitReferences.OfType<Branch>();

        /// <summary>
        /// Gets or sets the default <see cref="Branch"/> in the <see cref="Project"/> which is a subset of <see cref="Branch"/>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1614694296040_474053_271", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Project.Branches")]
        public Branch DefaultBranch { get; set; }

        /// <summary>
        /// Gets all the <see cref="Tag"/>s in the <see cref="Project"/> which is a subset of <see cref="CommitReferences"/>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628779424605_974596_740", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Project.CommitReferences")]
        public IEnumerable<Tag> Tags => this.CommitReferences.OfType<Tag>();

        /// <summary>
        /// Gets or sets the <see cref="Query"/> records owned by the <see cref="Project"/>. Each <see cref="Query"/> record represents a
        /// saved <see cref="Query"/> for the given <see cref="Project"/>.
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597396233220_492135_901", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<Query> Queries { get; set; } = [];

        /// <summary>
        /// Computes the <see cref="DataIdentity"/> records corresponding to the <see cref="IData"/> contained in the <see cref="Project"/>
        /// </summary>
        /// <returns>The set of <see cref="DataIdentity"/> records contained in the <see cref="Project"/>.</returns>
        /// <exception cref="NotSupportedException">Thrown until the derivation is supported.</exception>
        [DerivedProperty(nameof(identifiedData))]
        private IEnumerable<DataIdentity> ComputeIdentifiedData()
        {
            throw new NotSupportedException("Derived property identifiedData not yet supported");
        }
    }
}
