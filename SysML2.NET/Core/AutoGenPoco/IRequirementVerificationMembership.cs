// -------------------------------------------------------------------------------------------------
// <copyright file="IRequirementVerificationMembership.cs" company="Starion Group S.A.">
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
    /// A RequirementVerificationMembership is a RequirementConstraintMembership  used in the objective of a
    /// VerificationCase to identify a RequirementUsage that is verified by the VerificationCase.kind =
    /// RequirementConstraintKind::requirementowningType.oclIsKindOf(RequirementUsage)
    /// andowningType.owningFeatureMembership <> null
    /// andowningType.owningFeatureMembership.oclIsKindOf(ObjectiveMembership)
    /// </summary>
    public partial interface IRequirementVerificationMembership : IRequirementConstraintMembership
    {
        /// <summary>
        /// Queries the derived property OwnedRequirement
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        RequirementUsage QueryOwnedRequirement();

        /// <summary>
        /// Queries the derived property VerifiedRequirement
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        RequirementUsage QueryVerifiedRequirement();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
