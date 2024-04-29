// -------------------------------------------------------------------------------------------------
// <copyright file="IPortUsage.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2024 Starion Group S.A.
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
    /// A PortUsage is a usage of a PortDefinition. A PortUsage itself as well as all its nestedUsages must
    /// be referential (non-composite).nestedUsage->    reject(oclIsKindOf(PortUsage))->    forAll(not
    /// isComposite)specializesFromLibrary('Ports::ports')isComposite and owningType <> null
    /// and(owningType.oclIsKindOf(PortDefinition) or owningType.oclIsKindOf(PortUsage)) implies   
    /// specializesFromLibrary('Ports::Port::subports')owningType = null ornot
    /// owningType.oclIsKindOf(PortDefinition) andnot owningType.oclIsKindOf(PortUsage) implies   
    /// isReference
    /// </summary>
    public partial interface IPortUsage : IOccurrenceUsage
    {
        /// <summary>
        /// Queries the derived property PortDefinition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<PortDefinition> QueryPortDefinition();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
