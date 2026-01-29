// -------------------------------------------------------------------------------------------------
// <copyright file="Tag.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM.POCO.API_Model
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Decorators;

    /// <summary>
    /// </summary>
    [Class(xmiId: "_19_0_4_58901f1_1628778623137_473540_604", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class Tag : ITag
    {
        /// <summary>
        /// A collection of other identifiers for this record, especially if the record was created or
        /// represented in other software applications and systems.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469740_197983_66089", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IRecord.Alias")]
        public List<string> Alias { get; set; } = [];

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628778313797_665973_538", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "ICommitReference.Created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_2022x_2_f940371_1740179401841_624617_283", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "ICommitReference.Deleted")]
        public DateTime Deleted { get; set; }

        /// <summary>
        /// A statement that provides details about the record.
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1629741832147_14565_28133", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IRecord.Description")]
        public string Description { get; set; }

        /// <summary>
        /// The UUID assigned to th Record. This identifier shall conform to RFC 4122.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469739_885136_66087", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: true, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IRecord.Id")]
        public Guid Id { get; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1671293488580_668503_7742", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1577820469741_716007_66090")]
        [Implements(implementation: "ICommitReference.Name")]
        public string Name { get; set; }

        /// <summary>
        /// An optional human-friendly identifier for a record. The value assigned to the name for a given
        /// record must be in the set of values assigned to alias for that record.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469741_716007_66090", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1577820469740_197983_66089")]
        [RedefinedByProperty("ICommitReference.Name")]
        [Implements(implementation: "IRecord.Name")]
        string IRecord.Name
        {
            get => this.Name;
            set
            {
                this.Name = value;
            }
        }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628779424605_795551_741", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_4_58901f1_1628779129336_65342_713")]
        [Implements(implementation: "ITag.OwningProject")]
        public IProject OwningProject { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628779129336_65342_713", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedByProperty("ITag.OwningProject")]
        [Implements(implementation: "ICommitReference.OwningProject")]
        IProject ICommitReference.OwningProject
        {
            get => this.OwningProject;
            set
            {
                this.OwningProject = value;
            }
        }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628778313797_801027_537", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedByProperty("ITag.TaggedCommit")]
        [Implements(implementation: "ICommitReference.ReferencedCommit")]
        ICommit ICommitReference.ReferencedCommit
        {
            get => this.TaggedCommit;
            set
            {
                this.TaggedCommit = value;
            }
        }

        /// <summary>
        /// An IRI assigned to the record (for linked data access).
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469740_90190_66088", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IRecord.ResourceIdentifier")]
        public Uri? ResourceIdentifier { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628778728157_220677_662", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_4_58901f1_1628778313797_801027_537")]
        [Implements(implementation: "ITag.TaggedCommit")]
        public ICommit TaggedCommit { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
