// -------------------------------------------------------------------------------------------------
// <copyright file="IVerificationCaseUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.VerificationCases
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Systems.Cases;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A VerificationCaseUsage is a Usage of a VerificationCaseDefinition.
    /// </summary>
    [Class(xmiId: "Systems-VerificationCases-VerificationCaseUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IVerificationCaseUsage : ICaseUsage
    {
        /// <summary>
        /// The VerificationCase that is the definition of this VerificationCaseUsage.
        /// </summary>
        [Property(xmiId: "Systems-VerificationCases-VerificationCaseUsage-verificationCaseDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-Cases-CaseUsage-caseDefinition")]
        Guid? verificationCaseDefinition { get; }

        /// <summary>
        /// The RequirementUsages verified by this VerificationCaseUsage, which are the verifiedRequirements of
        /// all RequirementVerificationMemberships of the objectiveRequirement.
        /// </summary>
        [Property(xmiId: "Systems-VerificationCases-VerificationCaseUsage-verifiedRequirement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<Guid> verifiedRequirement { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
