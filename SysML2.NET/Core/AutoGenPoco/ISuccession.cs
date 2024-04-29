// -------------------------------------------------------------------------------------------------
// <copyright file="ISuccession.cs" company="Starion Group S.A.">
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
    /// A Succession is a binary Connector that requires its relatedFeatures to happen separately in
    /// time.specializesFromLibrary('Occurences::happensBeforeLinks')transitionStep =    if
    /// owningNamespace.oclIsKindOf(Step) and         owningNamespace.oclAsType(Step).           
    /// specializesFromLibrary('TransitionPerformances::TransitionPerformance') then       
    /// owningNamespace.oclAsType(Step)    else null    endiftriggerStep =    if transitionStep = null or   
    ///     transitionStep.ownedFeature.size() < 2 or       not
    /// transitionStep.ownedFeature->at(2).oclIsKindOf(Step)     then Set{}    else
    /// Set{transitionStep.ownedFeature->at(2).oclAsType(Step)}    endifeffectStep =    if transitionStep =
    /// null or        transitionStep.ownedFeature.size() < 4 or       not
    /// transitionStep.ownedFeature->at(4).oclIsKindOf(Step)     then Set{}    else
    /// Set{transitionStep.ownedFeature->at(4).oclAsType(Step)}    endifguardExpression =    if
    /// transitionStep = null or        transitionStep.ownedFeature.size() < 3 or       not
    /// transitionStep.ownedFeature->at(3).oclIsKindOf(Expression)     then Set{}    else
    /// Set{transitionStep.ownedFeature->at(3).oclAsType(Expression)}    endif
    /// </summary>
    public partial interface ISuccession : IConnector
    {
        /// <summary>
        /// Queries the derived property EffectStep
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Step> QueryEffectStep();

        /// <summary>
        /// Queries the derived property GuardExpression
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Expression> QueryGuardExpression();

        /// <summary>
        /// Queries the derived property TransitionStep
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Step QueryTransitionStep();

        /// <summary>
        /// Queries the derived property TriggerStep
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Step> QueryTriggerStep();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
