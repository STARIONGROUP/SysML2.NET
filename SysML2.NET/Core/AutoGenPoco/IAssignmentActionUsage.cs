﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IAssignmentActionUsage.cs" company="Starion Group S.A.">
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
    /// An AssignmentActionUsage is an ActionUsage that is defined, directly or indirectly, by the
    /// ActionDefinition AssignmentAction from the Systems Model Library. It specifies that the value of the
    /// referent Feature, relative to the target given by the result of the targetArgument Expression,
    /// should be set to the result of the
    /// valueExpression.specializesFromLibrary('Actions::assignmentActions')let targetParameter : Feature =
    /// inputParameter(1) intargetParameter <> null andtargetParameter.ownedFeature->notEmpty()
    /// andtargetParameter.ownedFeature->first().   
    /// redefines('AssignmentAction::target::startingAt')valueExpression = argument(2)targetArgument =
    /// argument(1)isSubactionUsage() implies    specializesFromLibrary('Actions::Action::assignments')let
    /// targetParameter : Feature = inputParameter(1) intargetParameter <> null
    /// andtargetParameter.ownedFeature->notEmpty() andtargetParameter->first().ownedFeature->notEmpty()
    /// andtargetParameter->first().ownedFeature->first().   
    /// redefines('AssigmentAction::target::startingAt::accessedFeature')let targetParameter : Feature =
    /// inputParameter(1) intargetParameter <> null andtargetParameter.ownedFeature->notEmpty()
    /// andtargetParameter->first().ownedFeature->notEmpty()
    /// andtargetParameter->first().ownedFeature->first().redefines(referent)referent =    let
    /// unownedFeatures : Sequence(Feature) = ownedMembership->       
    /// reject(oclIsKindOf(FeatureMembership)).memberElement->        selectByKind(Feature) in    if
    /// unownedFeatures->isEmpty() then null    else unownedFeatures->first().oclAsType(Feature)   
    /// endifownedMembership->exists(    not oclIsKindOf(OwningMembership) and    
    /// memberElement.oclIsKindOf(Feature))
    /// </summary>
    public partial interface IAssignmentActionUsage : IActionUsage
    {
        /// <summary>
        /// Queries the derived property Referent
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Feature QueryReferent();

        /// <summary>
        /// Queries the derived property TargetArgument
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Expression QueryTargetArgument();

        /// <summary>
        /// Queries the derived property ValueExpression
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Expression QueryValueExpression();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
