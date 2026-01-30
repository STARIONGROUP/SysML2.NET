// -------------------------------------------------------------------------------------------------
// <copyright file="ICalculationUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Calculations
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Kernel.Functions;
    using SysML2.NET.Core.DTO.Systems.Actions;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A CalculationUsage is an ActionUsage that is also an Expression, and, so, is typed by a Function.
    /// Nominally, if the type is a CalculationDefinition, a CalculationUsage is a Usage of that
    /// CalculationDefinition within a system. However, other kinds of kernel Functions are also allowed, to
    /// permit use of Functions from the Kernel Model Libraries.
    /// </summary>
    [Class(xmiId: "Systems-Calculations-CalculationUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface ICalculationUsage : IExpression, IActionUsage
    {
        /// <summary>
        /// The <ode>Function that is the type of this CalculationUsage. Nominally, this would be a
        /// CalculationDefinition, but a kernel Function is also allowed, to permit use of Functions from the
        /// Kernel Model Libraries.</ode>
        /// </summary>
        [Property(xmiId: "Systems-Calculations-CalculationUsage-calculationDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Kernel-Functions-Expression-function")]
        [RedefinedProperty(propertyName: "Systems-Actions-ActionUsage-actionDefinition")]
        Guid? calculationDefinition { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
