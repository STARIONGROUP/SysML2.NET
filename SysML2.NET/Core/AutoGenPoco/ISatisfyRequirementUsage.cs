// -------------------------------------------------------------------------------------------------
// <copyright file="ISatisfyRequirementUsage.cs" company="Starion Group S.A.">
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
    /// A SatisfyRequirementUsage is an AssertConstraintUsage that asserts, by default, that a satisfied
    /// RequirementUsage is true for a specific satisfyingFeature, or, if isNegated = true, that the
    /// RequirementUsage is false. The satisfied RequirementUsage is related to the SatisfyRequirementUsage
    /// by a ReferenceSubsetting Relationship.satisfyingFeature =    let bindings: BindingConnector =
    /// ownedMember->        selectByKind(BindingConnector)->        select(b |
    /// b.relatedElement->includes(subjectParameter)) in    if bindings->isEmpty() or       
    /// bindings->first().relatedElement->exits(r | r <> subjectParameter)     then null    else
    /// bindings->first().relatedElement->any(r | r <> subjectParameter)   
    /// endifownedMember->selectByKind(BindingConnector)->    select(b |       
    /// b.relatedElement->includes(subjectParameter) and        b.relatedElement->exists(r | r <>
    /// subjectParameter))->    size() = 1ownedReferenceSubsetting <> null implies   
    /// ownedReferenceSubsetting.referencedFeature.oclIsKindOf(RequirementUsage)if isNegated then   
    /// specializesFromLibrary('Requirements::notSatisfiedRequirementChecks')else   
    /// specializesFromLibrary('Requirements::satisfiedRequirementChecks')endif
    /// </summary>
    public partial interface ISatisfyRequirementUsage : IRequirementUsage, IAssertConstraintUsage
    {
        /// <summary>
        /// Queries the derived property SatisfiedRequirement
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        RequirementUsage QuerySatisfiedRequirement();

        /// <summary>
        /// Queries the derived property SatisfyingFeature
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Feature QuerySatisfyingFeature();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
