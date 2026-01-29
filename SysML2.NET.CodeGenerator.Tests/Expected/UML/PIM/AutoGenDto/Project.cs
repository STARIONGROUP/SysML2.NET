// -------------------------------------------------------------------------------------------------
// <copyright file="Project.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PIM.DTO.API_Model
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    /// <summary>
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1577820468973_232920_65982", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class Project : IProject
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        [Property(xmiId: "sysml2.net", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IData.Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// A collection of other identifiers for this record, especially if the record was created or
        /// represented in other software applications and systems.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469740_197983_66089", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IRecord.Alias")]
        public List<string> Alias { get; set; } = [];

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1614694201382_729306_245", aggregation: AggregationKind.Composite, lowerValue: 1, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_58901f1_1628779129336_898482_712")]
        [Implements(implementation: "IProject.Branches")]
        public List<Guid> Branches { get; set; } = [];

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628779129336_898482_712", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IProject.CommitReferences")]
        public List<Guid> CommitReferences { get; set; } = [];

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469733_493524_66075", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IProject.Commits")]
        public List<Guid> Commits { get; set; } = [];

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_f940371_1694730728649_158871_419", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IProject.Created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1614694296040_474053_271", aggregation: AggregationKind.Composite, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_58901f1_1614694201382_729306_245")]
        [Implements(implementation: "IProject.DefaultBranch")]
        public Guid DefaultBranch { get; set; }

        /// <summary>
        /// A statement that provides details about the record.
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1629741832147_14565_28133", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IRecord.Description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1671293441918_865671_7736", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1577820469741_716007_66090")]
        [Implements(implementation: "IProject.Name")]
        public string Name { get; set; }

        /// <summary>
        /// An optional human-friendly identifier for a record. The value assigned to the name for a given
        /// record must be in the set of values assigned to alias for that record.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469741_716007_66090", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1577820469740_197983_66089")]
        [RedefinedByProperty("IProject.Name")]
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
        [Property(xmiId: "_19_0_4_58901f1_1597396233220_492135_901", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IProject.Queries")]
        public List<Guid> Queries { get; set; } = [];

        /// <summary>
        /// An IRI assigned to the record (for linked data access).
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469740_90190_66088", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IRecord.ResourceIdentifier")]
        public Uri? ResourceIdentifier { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628779424605_974596_740", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_58901f1_1628779129336_898482_712")]
        [Implements(implementation: "IProject.Tags")]
        public List<Guid> Tags { get; set; } = [];

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
