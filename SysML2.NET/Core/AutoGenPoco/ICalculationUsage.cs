// -------------------------------------------------------------------------------------------------
// <copyright file="ICalculationUsage.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A CalculationUsage is an ActionUsage that is also an Expression, and, so, is typed by a Function.
    /// Nominally, if the type is a CalculationDefinition, a CalculationUsage is a Usage of that
    /// CalculationDefinition within a system. However, other kinds of kernel Functions are also allowed, to
    /// permit use of Functions from the Kernel Model
    /// Libraries.specializesFromLibrary('Calculations::calculations')owningType <> null
    /// and(owningType.oclIsKindOf(CalculationDefinition) or owningType.oclIsKindOf(CalculationUsage))
    /// implies    specializesFromLibrary('Calculations::Calculation::subcalculations')
    /// </summary>
    public partial interface ICalculationUsage : IActionUsage, IExpression
    {
        /// <summary>
        /// Queries the derived property CalculationDefinition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Function QueryCalculationDefinition();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
