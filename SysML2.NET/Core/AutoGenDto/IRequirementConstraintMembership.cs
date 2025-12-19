// -------------------------------------------------------------------------------------------------
// <copyright file="IRequirementConstraintMembership.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Requirements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.Systems.Requirements;
    using SysML2.NET.Core.DTO.Core.Types;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A RequirementConstraintMembership is a FeatureMembership for an assumed or required ConstraintUsage
    /// of a RequirementDefinition or RequirementUsage.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1584048032876_657748_336", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IRequirementConstraintMembership : IFeatureMembership
    {
        /// <summary>
        /// Whether the RequirementConstraintMembership is for an assumed or required ConstraintUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1584048161309_821854_390", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        RequirementConstraintKind Kind { get; set; }

        /// <summary>
        /// The ConstraintUsage that is the ownedMemberFeature of this RequirementConstraintMembership.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1584048366950_985767_426", aggregation: AggregationKind.Composite, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674993_898044_43344")]
        Guid OwnedConstraint { get; }

        /// <summary>
        /// The ConstraintUsage that is referenced through this RequirementConstraintMembership. It is the
        /// referencedFeature of the ownedReferenceSubsetting of the ownedConstraint, if there is one, and,
        /// otherwise, the ownedConstraint itself.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617118807597_77864_3544", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid ReferencedConstraint { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
