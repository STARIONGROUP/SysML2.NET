// -------------------------------------------------------------------------------------------------
// <copyright file="Commit.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM.DTO
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A record of changes made to ElementRecord's within a particular Branch of a Resource
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1577820468971_790808_65979", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("sysml2.net", "latest")]
    public partial class Commit : ICommit
    {
        /// <summary>
        /// Set of ID's that are assigned using legacy or tool specific implementations that don't conform to
        /// Kernel Object Lifecycle (KOL) or RFC 4122. For example git's SHA1 hashing id mechanism or historical
        /// SysML 1.X ID's
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469740_197983_66089", aggregation: AggregationKind.None, lowerValue: 1, upperValue: int.MaxValue, isOrdered: false, isReadOnly: true, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "IRecord.Alias")]
        public List<string> Alias { get; set; } = [];

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469724_226337_66058", aggregation: AggregationKind.None, lowerValue: 1, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "ICommit.Change")]
        public List<Guid> Change { get; set; } = [];

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469727_54677_66063", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "ICommit.Created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1629741832147_14565_28133", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "IRecord.Description")]
        public string Description { get; set; }

        /// <summary>
        /// Uniqueness constraints?
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469741_716007_66090", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: true, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1577820469740_197983_66089")]
        [Implements(implementation: "IRecord.HumanIdentifier")]
        public string HumanIdentifier { get; set; }

        /// <summary>
        /// Every Record shall have a calculated globally unique identifier. This identifier shall conform to
        /// RFC 4122.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469739_885136_66087", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: true, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "IRecord.Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469728_610810_66065", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "ICommit.OwningProject")]
        public Guid OwningProject { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469727_74119_66064", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "ICommit.PreviousCommits")]
        public List<Guid> PreviousCommits { get; set; } = [];

        /// <summary>
        /// The context specific URI from which the data represented in LinkedDataRecord's can be retrieved
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469740_90190_66088", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "IRecord.ResourceIdentifier")]
        public Uri ResourceIdentifier { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1629496059367_814933_41", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "ICommit.VersionedData")]
        public List<Guid> VersionedData { get; } = [];

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
