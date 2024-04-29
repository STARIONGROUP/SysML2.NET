// -------------------------------------------------------------------------------------------------
// <copyright file="ICaseUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A CaseUsage is a Usage of a CaseDefinition.objectiveRequirement =     let objectives:
    /// OrderedSet(RequirementUsage) =         featureMembership->           
    /// selectByKind(ObjectiveMembership).            ownedRequirement in    if objectives->isEmpty() then
    /// null    else objectives->first().ownedObjectiveRequirement    endiffeatureMembership->   
    /// selectByKind(ObjectiveMembership)->    size() <=
    /// 1featureMembership->	selectByKind(SubjectMembership)->	size() <= 1actorParameter =
    /// featureMembership->    selectByKind(ActorMembership).    ownedActorParametersubjectParameter =   
    /// let subjects : OrderedSet(SubjectMembership) =        
    /// featureMembership->selectByKind(SubjectMembership) in    if subjects->isEmpty() then null    else
    /// subjects->first().ownedSubjectParameter    endifinput->notEmpty() and input->first() =
    /// subjectParameterspecializesFromLibrary('Cases::cases')isComposite and owningType <> null and    
    /// (owningType.oclIsKindOf(CaseDefinition) or     owningType.oclIsKindOf(CaseUsage)) implies   
    /// specializesFromLibrary('Cases::Case::subcases')
    /// </summary>
    public partial interface ICaseUsage : ICalculationUsage
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
