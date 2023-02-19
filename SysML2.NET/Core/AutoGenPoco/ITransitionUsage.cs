// -------------------------------------------------------------------------------------------------
// <copyright file="ITransitionUsage.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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
    /// A TransitionUsage is an ActionUsage that is a behavioral Step representing a transition between
    /// ActionUsages or StateUsages.A TransitionUsage must subset, directly or indirectly, the base
    /// TransitionUsage transitionActions, if it is not a composite feature, or the TransitionUsage
    /// subtransitions inherited from its owner, if it is a composite feature.A TransitionUsage may by
    /// related to some of its ownedFeatures using TransitionFeatureMembership Relationships, corresponding
    /// to the triggers, guards and effects of the TransitionUsage.isComposite and owningType <> null
    /// and(owningType.oclIsKindOf(ActionDefinition) or  owningType.oclIsKindOf(ActionUsage)) andnot
    /// (owningType.oclIsKindOf(StateDefinition) or     owningType.oclIsKindOf(StateUsage)) implies   
    /// specializesFromLibrary("Actions::Action::decisionTransitionActions")specializesFromLibrary("Actions::actions::transitionActions")isComposite
    /// and owningType <> null and(owningType.oclIsKindOf(StateDefinition) or
    /// owningType.oclIsKindOf(StateUsage)) implies   
    /// specializesFromLibrary("States::State::stateTransitions")
    /// </summary>
    public partial interface ITransitionUsage : IActionUsage
    {
        /// <summary>
        /// Queries the derived property EffectAction
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ActionUsage> QueryEffectAction();

        /// <summary>
        /// Queries the derived property GuardExpression
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Expression> QueryGuardExpression();

        /// <summary>
        /// Queries the derived property Source
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        ActionUsage QuerySource();

        /// <summary>
        /// Queries the derived property Succession
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Succession QuerySuccession();

        /// <summary>
        /// Queries the derived property Target
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        ActionUsage QueryTarget();

        /// <summary>
        /// Queries the derived property TriggerAction
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<AcceptActionUsage> QueryTriggerAction();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
