// -------------------------------------------------------------------------------------------------
// <copyright file="IConjugation.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// Conjugation is a Relationship between two types in which the conjugatedType inherits all the
    /// Features of the originalType, but with all input and output Features reversed. That is, any Features
    /// with a direction in relative to the originalType are considered to have an effective direction of
    /// out relative to the conjugatedType and, similarly, Features with direction out in the originalType
    /// are considered to have an effective direction of in in the conjugatedType. Features with direction
    /// inout, or with no direction, in the originalType, are inherited without change.A Type may
    /// participate as a conjugatedType in at most one Conjugation relationship, and such a Type may not
    /// also be the specific Type in any Specialization relationship.
    /// </summary>
    public partial interface IConjugation : IRelationship
    {
        /// <summary>
        /// The Type that is the result of applying Conjugation to the originalType.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Guid ConjugatedType { get; set; }

        /// <summary>
        /// The Type to be conjugated.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Guid OriginalType { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
