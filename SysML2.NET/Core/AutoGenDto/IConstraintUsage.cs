// -------------------------------------------------------------------------------------------------
// <copyright file="IConstraintUsage.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Core.DTO.Systems.Constraints
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Kernel.Functions;
    using SysML2.NET.Core.DTO.Systems.Occurrences;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ConstraintUsage is an OccurrenceUsage that is also a BooleanExpression, and, so, is typed by a
    /// Predicate. Nominally, if the type is a ConstraintDefinition, a ConstraintUsage is a Usage of that
    /// ConstraintDefinition. However, other kinds of kernel Predicates are also allowed, to permit use of
    /// Predicates from the Kernel Model Libraries.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1578067096274_745288_1478", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IConstraintUsage : IBooleanExpression, IOccurrenceUsage
    {
        /// <summary>
        /// The (single) Predicate that is the type of this ConstraintUsage. Nominally, this will be a
        /// ConstraintDefinition, but other kinds of Predicates are also allowed, to permit use of Predicates
        /// from the Kernel Model Libraries.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1578067546711_751168_1745", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1578025035149_386_969")]
        Guid? ConstraintDefinition { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
