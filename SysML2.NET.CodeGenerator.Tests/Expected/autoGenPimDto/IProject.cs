﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IProject.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Common;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ServerRecord object representing a single project within a Workspace. Can contain, Branch's,
    /// Revision's, and Tag's
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1577820468973_232920_65982", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("sysml2.net", "latest")]
    public partial interface IProject : IRecord
    {
        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469733_493524_66075", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public List<Guid> Commits { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597396233220_492135_901", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public List<Guid> Queries { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1614694201382_729306_245", aggregation: AggregationKind.Composite, lowerValue: 1, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_58901f1_1628779129336_898482_712")]
        public List<Guid> Branches { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1614694296040_474053_271", aggregation: AggregationKind.Composite, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_58901f1_1614694201382_729306_245")]
        public Guid DefaultBranch { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628779129336_898482_712", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public List<Guid> CommitReferences { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628779424605_974596_740", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_58901f1_1628779129336_898482_712")]
        public List<Guid> Tags { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1671293441918_865671_7736", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_f940371_1694730728649_158871_419", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public DateTime Created { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
