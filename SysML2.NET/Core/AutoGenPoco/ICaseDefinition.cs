// -------------------------------------------------------------------------------------------------
// <copyright file="ICaseDefinition.cs" company="Starion Group S.A.">
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
    /// A CaseDefinition is a CalculationDefinition for a process, often involving collecting evidence or
    /// data, relative to a subject, possibly involving the collaboration of one or more other actors,
    /// producing a result that meets an objective.objectiveRequirement =     let objectives:
    /// OrderedSet(RequirementUsage) =         featureMembership->           
    /// selectByKind(ObjectiveMembership).            ownedRequirement in    if objectives->isEmpty() then
    /// null    else objectives->first().ownedObjectiveRequirement    endiffeatureMembership->   
    /// selectByKind(ObjectiveMembership)->    size() <= 1subjectParameter =    let subjectMems :
    /// OrderedSet(SubjectMembership) =         featureMembership->selectByKind(SubjectMembership) in    if
    /// subjectMems->isEmpty() then null    else subjectMems->first().ownedSubjectParameter   
    /// endifactorParameter = featureMembership->    selectByKind(ActorMembership).   
    /// ownedActorParameterfeatureMembership->selectByKind(SubjectMembership)->size() <= 1input->notEmpty()
    /// and input->first() = subjectParameterspecializesFromLibrary('Cases::Case')
    /// </summary>
    public partial interface ICaseDefinition : ICalculationDefinition
    {
        /// <summary>
        /// Queries the derived property ActorParameter
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<PartUsage> QueryActorParameter();

        /// <summary>
        /// Queries the derived property ObjectiveRequirement
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        RequirementUsage QueryObjectiveRequirement();

        /// <summary>
        /// Queries the derived property SubjectParameter
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Usage QuerySubjectParameter();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
