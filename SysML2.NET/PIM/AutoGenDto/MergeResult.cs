﻿// -------------------------------------------------------------------------------------------------
// <copyright file="MergeResult.cs" company="Starion Group S.A.">
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
    [Class(xmiId: "_19_0_4_58901f1_1667600196881_725157_160", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("sysml2.net", "latest")]
    public partial class MergeResult : IMergeResult
    {
        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1667600196883_169912_162", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "IMergeResult.Conflict")]
        public List<Guid> Conflict { get; set; } = [];

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1667600196883_581172_161", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "IMergeResult.MergeCommit")]
        public Guid MergeCommit { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
