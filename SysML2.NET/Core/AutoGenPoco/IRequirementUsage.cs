﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IRequirementUsage.cs" company="Starion Group S.A.">
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
    /// A RequirementUsage is a Usage of a RequirementDefinition.actorParameter = featureMembership->   
    /// selectByKind(ActorMembership).    ownedActorParameterassumedConstraint = ownedFeatureMembership->   
    /// selectByKind(RequirementConstraintMembership)->    select(kind =
    /// RequirementConstraintKind::assumption).    ownedConstraintframedConcern = featureMembership->   
    /// selectByKind(FramedConcernMembership).    ownedConcernrequiredConstraint = ownedFeatureMembership-> 
    ///   selectByKind(RequirementConstraintMembership)->    select(kind =
    /// RequirementConstraintKind::requirement).    ownedConstraintstakeholderParameter =
    /// featureMembership->    selectByKind(AStakholderMembership).   
    /// ownedStakeholderParametersubjectParameter =    let subjects : OrderedSet(SubjectMembership) =       
    ///  featureMembership->selectByKind(SubjectMembership) in    if subjects->isEmpty() then null    else
    /// subjects->first().ownedSubjectParameter    endiftext = documentation.bodyfeatureMembership->   
    /// selectByKind(SubjectMembership)->    size() <= 1input->notEmpty() and input->first() =
    /// subjectParameterspecializesFromLibrary('Requirements::requirementChecks')isComposite and owningType
    /// <> null and    (owningType.oclIsKindOf(RequirementDefinition) or    
    /// owningType.oclIsKindOf(RequirementUsage)) implies   
    /// specializesFromLibrary('Requirements::RequirementCheck::subrequirements')owningfeatureMembership <>
    /// null andowningfeatureMembership.oclIsKindOf(ObjectiveMembership) implies   
    /// owningType.ownedSpecialization.general->forAll(gen |        (gen.oclIsKindOf(CaseDefinition) implies
    ///            redefines(gen.oclAsType(CaseDefinition).objectiveRequirement)) and       
    /// (gen.oclIsKindOf(CaseUsage) implies           
    /// redefines(gen.oclAsType(CaseUsage).objectiveRequirement))owningFeatureMembership <> null
    /// andowningFeatureMembership.oclIsKindOf(RequirementVerificationMembership) implies   
    /// specializesFromLibrary('VerificationCases::VerificationCase::obj::requirementVerifications')
    /// </summary>
    public partial interface IRequirementUsage : IConstraintUsage
    {
        /// <summary>
        /// Queries the derived property ActorParameter
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<PartUsage> QueryActorParameter();

        /// <summary>
        /// Queries the derived property AssumedConstraint
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ConstraintUsage> QueryAssumedConstraint();

        /// <summary>
        /// Queries the derived property FramedConcern
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ConcernUsage> QueryFramedConcern();

        /// <summary>
        /// An optional modeler-specified identifier for this RequirementUsage (used, e.g., to link it to an
        /// original requirement text in some source document), which is the declaredShortName for the
        /// RequirementUsage.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        string ReqId { get; set; }

        /// <summary>
        /// Queries the derived property RequiredConstraint
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ConstraintUsage> QueryRequiredConstraint();

        /// <summary>
        /// Queries the derived property RequirementDefinition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        RequirementDefinition QueryRequirementDefinition();

        /// <summary>
        /// Queries the derived property StakeholderParameter
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<PartUsage> QueryStakeholderParameter();

        /// <summary>
        /// Queries the derived property SubjectParameter
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Usage QuerySubjectParameter();

        /// <summary>
        /// Queries the derived property Text
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<string> QueryText();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
