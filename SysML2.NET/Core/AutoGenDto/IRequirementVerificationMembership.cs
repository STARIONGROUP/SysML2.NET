// -------------------------------------------------------------------------------------------------
// <copyright file="IRequirementVerificationMembership.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.VerificationCases
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.Systems.Requirements;
    using SysML2.NET.Core.DTO.Systems.Requirements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A RequirementVerificationMembership is a RequirementConstraintMembership  used in the objective of a
    /// VerificationCase to identify a RequirementUsage that is verified by the VerificationCase.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1603921138449_428307_72", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IRequirementVerificationMembership : IRequirementConstraintMembership
    {
        /// <summary>
        /// The kind of a RequirementVerificationMembership must be requirement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1603921465292_637146_187", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "requirement")]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1584048161309_821854_390")]
        new RequirementConstraintKind Kind { get; set; }

        /// <summary>
        /// The owned RequirementUsage that acts as the ownedConstraint for this
        /// RequirementVerificationMembership. This will either be the verifiedRequirement, or it will subset
        /// the verifiedRequirement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1603921329650_612380_147", aggregation: AggregationKind.Composite, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1584048366950_985767_426")]
        Guid OwnedRequirement { get; }

        /// <summary>
        /// The RequirementUsage that is identified as being verified. It is the referencedConstraint of the
        /// RequirementVerificationMembership considered as a RequirementConstraintMembership, which must be a
        /// RequirementUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1603921870169_98378_309", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_4_12e503d9_1617118807597_77864_3544")]
        Guid VerifiedRequirement { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
