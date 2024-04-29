// -------------------------------------------------------------------------------------------------
// <copyright file="IAssertConstraintUsage.cs" company="Starion Group S.A.">
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
    /// An AssertConstraintUsage is a ConstraintUsage that is also an Invariant and, so, is asserted to be
    /// true (by default). Unless it is the AssertConstraintUsage itself, the asserted ConstraintUsage is
    /// related to the AssertConstraintUsage by a ReferenceSubsetting Relationship.assertedConstraint =   
    /// if ownedReferenceSubsetting = null then self    else
    /// ownedReferenceSubsetting.referencedFeature.oclAsType(ConstraintUsage)    endifif isNegated then   
    /// specializesFromLibrary('Constraints::negatedConstraints')else   
    /// specializesFromLibrary('Constraints::assertedConstraints')endifownedReferenceSubsetting <> null
    /// implies    ownedReferenceSubsetting.referencedFeature.oclIsKindOf(ConstraintUsage)
    /// </summary>
    public partial interface IAssertConstraintUsage : IConstraintUsage, IInvariant
    {
        /// <summary>
        /// Queries the derived property AssertedConstraint
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        ConstraintUsage QueryAssertedConstraint();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
