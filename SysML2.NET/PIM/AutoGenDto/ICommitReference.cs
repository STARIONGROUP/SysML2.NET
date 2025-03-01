﻿// -------------------------------------------------------------------------------------------------
// <copyright file="ICommitReference.cs" company="Starion Group S.A.">
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
    /// </summary>
    [Class(xmiId: "_19_0_4_58901f1_1628778313792_224131_535", isAbstract: true, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("sysml2.net", "latest")]
    public partial interface ICommitReference : IRecord
    {
        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628778313797_801027_537", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public Guid ReferencedCommit { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628778313797_665973_538", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public DateTime Created { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628779129336_65342_713", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public Guid OwningProject { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1671293488580_668503_7742", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public string Name { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
