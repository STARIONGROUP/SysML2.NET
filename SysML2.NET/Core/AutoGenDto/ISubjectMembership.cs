// -------------------------------------------------------------------------------------------------
// <copyright file="ISubjectMembership.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Kernel.Behaviors;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A SubjectMembership is a ParameterMembership that indicates that its ownedSubjectParameter is the
    /// subject of its owningType. The owningType of a SubjectMembership must be a RequirementDefinition,
    /// RequirementUsage, CaseDefinition, or CaseUsage.
    /// </summary>
    [Class(xmiId: "Systems-Requirements-SubjectMembership", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface ISubjectMembership : IParameterMembership
    {
        /// <summary>
        /// The UsageownedMemberParameter of this SubjectMembership.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-SubjectMembership-ownedSubjectParameter", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Kernel-Behaviors-ParameterMembership-ownedMemberParameter")]
        Guid ownedSubjectParameter { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
