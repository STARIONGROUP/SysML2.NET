﻿// -------------------------------------------------------------------------------------------------
// <copyright file="CompositeConstraint.cs" company="Starion Group S.A.">
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
    /// </summary>
    [Class(xmiId: "_19_0_4_58901f1_1597377170613_382290_372", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("sysml2.net", "latest")]
    public partial class CompositeConstraint : ICompositeConstraint
    {
        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377207847_92212_477", aggregation: AggregationKind.Composite, lowerValue: 2, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "ICompositeConstraint.Constraint")]
        public List<Guid> Constraint { get; set; } = [];

        /// <summary>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377244778_333596_491", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "ICompositeConstraint.Operator")]
        public JoinOperator Operator { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
