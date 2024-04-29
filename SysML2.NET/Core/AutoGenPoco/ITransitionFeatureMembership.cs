// -------------------------------------------------------------------------------------------------
// <copyright file="ITransitionFeatureMembership.cs" company="Starion Group S.A.">
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
    /// A TransitionFeatureMembership is a FeatureMembership for a trigger, guard or effect of a
    /// TransitionUsage, whose transitionFeature is a AcceptActionUsage, Boolean-valued Expression or
    /// ActionUsage, depending on its kind. kind = TransitionFeatureKind::trigger implies   
    /// transitionFeature.oclIsKindOf(AcceptActionUsage)owningType.oclIsKindOf(TransitionUsage)kind =
    /// TransitionFeatureKind::guard implies    transitionFeature.oclIsKindOf(Expression) and    let guard :
    /// Expression = transitionFeature.oclIsKindOf(Expression) in   
    /// guard.result.specializesFromLibrary('ScalarValues::Boolean') and    guard.result.multiplicity <>
    /// null and    guard.result.multiplicity.hasBounds(1,1)kind = TransitionFeatureKind::effect implies   
    /// transitionFeature.oclIsKindOf(ActionUsage)
    /// </summary>
    public partial interface ITransitionFeatureMembership : IFeatureMembership
    {
        /// <summary>
        /// Whether this TransitionFeatureMembership  is for a trigger, guard or effect.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        TransitionFeatureKind Kind { get; set; }

        /// <summary>
        /// Queries the derived property TransitionFeature
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Step QueryTransitionFeature();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
