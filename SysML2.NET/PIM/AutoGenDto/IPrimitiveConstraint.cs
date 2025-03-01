﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IPrimitiveConstraint.cs" company="Starion Group S.A.">
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
    [Class(xmiId: "_19_0_4_58901f1_1597377157087_303630_371", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("sysml2.net", "latest")]
    public partial interface IPrimitiveConstraint : IConstraint
    {
        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377338703_200864_497", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public bool Inverse { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377425985_159367_499", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public string Property { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377427373_325175_501", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public Operator Operator { get; set; }

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377431228_229452_503", aggregation: AggregationKind.None, lowerValue: 1, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        public List<string> Value { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
