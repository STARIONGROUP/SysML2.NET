// -------------------------------------------------------------------------------------------------
// <copyright file="IPortDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A PortDefinition defines a point at which external entities can connect to and interact with a
    /// system or part of a system. Any ownedUsages of a PortDefinition, other than PortUsages, must not be
    /// composite.conjugatedPortDefinition = let conjugatedPortDefinitions :
    /// OrderedSet(ConjugatedPortDefinition) =    ownedMember->selectByKind(ConjugatedPortDefinition) inif
    /// conjugatedPortDefinitions->isEmpty() then nullelse
    /// conjugatedPortDefinitions->first()endifownedUsage->    reject(oclIsKindOf(PortUsage))->   
    /// forAll(not isComposite)not oclIsKindOf(ConjugatedPortDefinition) implies    ownedMember->       
    /// selectByKind(ConjugatedPortDefinition)->        size() = 1specializesFromLibrary('Ports::Port')
    /// </summary>
    public partial interface IPortDefinition : IOccurrenceDefinition, IStructure
    {
        /// <summary>
        /// Queries the derived property ConjugatedPortDefinition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        ConjugatedPortDefinition QueryConjugatedPortDefinition();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
