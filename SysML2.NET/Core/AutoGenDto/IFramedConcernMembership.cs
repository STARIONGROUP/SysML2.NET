// -------------------------------------------------------------------------------------------------
// <copyright file="IFramedConcernMembership.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Requirements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.Systems.Requirements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A FramedConcernMembership is a RequirementConstraintMembership for a framed ConcernUsage of a
    /// RequirementDefinition or RequirementUsage.
    /// </summary>
    [Class(xmiId: "Systems-Requirements-FramedConcernMembership", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFramedConcernMembership : IRequirementConstraintMembership
    {
        /// <summary>
        /// The kind of an FramedConcernMembership must be requirement.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-FramedConcernMembership-kind", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "requirement")]
        [RedefinedProperty(propertyName: "Systems-Requirements-RequirementConstraintMembership-kind")]
        new RequirementConstraintKind Kind { get; set; }

        /// <summary>
        /// The ConcernUsage that is the ownedConstraint of this FramedConcernMembership.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-FramedConcernMembership-ownedConcern", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Systems-Requirements-RequirementConstraintMembership-ownedConstraint")]
        Guid ownedConcern { get; }

        /// <summary>
        /// The ConcernUsage that is referenced through this FramedConcernMembership. It is the
        /// referencedConstraint of the FramedConcernMembership considered as a RequirementConstraintMembership,
        /// which must be a ConcernUsage.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-FramedConcernMembership-referencedConcern", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Systems-Requirements-RequirementConstraintMembership-referencedConstraint")]
        Guid referencedConcern { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
