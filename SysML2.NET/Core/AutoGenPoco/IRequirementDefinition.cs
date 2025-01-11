// -------------------------------------------------------------------------------------------------
// <copyright file="IRequirementDefinition.cs" company="Starion Group S.A.">
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
    /// A RequirementDefinition is a ConstraintDefinition that defines a requirement used in the context of
    /// a specification as a constraint that a valid solution must satisfy. The specification is relative to
    /// a specified subject, possibly in collaboration with one or more external actors.text =
    /// documentation.bodyassumedConstraint = ownedFeatureMembership->   
    /// selectByKind(RequirementConstraintMembership)->    select(kind =
    /// RequirementConstraintKind::assumption).    ownedConstraintrequiredConstraint =
    /// ownedFeatureMembership->    selectByKind(RequirementConstraintMembership)->    select(kind =
    /// RequirementConstraintKind::requirement).    ownedConstraintsubjectParameter =    let subjects :
    /// OrderedSet(SubjectMembership) =         featureMembership->selectByKind(SubjectMembership) in    if
    /// subjects->isEmpty() then null    else subjects->first().ownedSubjectParameter    endifframedConcern
    /// = featureMembership->    selectByKind(FramedConcernMembership).    ownedConcernactorParameter =
    /// featureMembership->    selectByKind(ActorMembership).    ownedActorParameterstakeholderParameter =
    /// featureMembership->    selectByKind(StakholderMembership).   
    /// ownedStakeholderParameterfeatureMembership->	    selectByKind(SubjectMembership)->    size() <=
    /// 1input->notEmpty() and input->first() =
    /// subjectParameterspecializesFromLibrary('Requirements::RequirementCheck')
    /// </summary>
    public partial interface IRequirementDefinition : IConstraintDefinition
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
        /// An optional modeler-specified identifier for this RequirementDefinition (used, e.g., to link it to
        /// an original requirement text in some source document), which is the declaredShortName for the
        /// RequirementDefinition.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        string ReqId { get; set; }

        /// <summary>
        /// Queries the derived property RequiredConstraint
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ConstraintUsage> QueryRequiredConstraint();

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
