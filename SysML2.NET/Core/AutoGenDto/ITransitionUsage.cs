// -------------------------------------------------------------------------------------------------
// <copyright file="ITransitionUsage.cs" company="Starion Group S.A.">
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
    /// A TransitionUsage is an ActionUsage representing a triggered transition between ActionUsages or
    /// StateUsages. When triggered by a triggerAction, when its guardExpression is true, the
    /// TransitionUsage asserts that its source is exited, then its effectAction (if any) is performed, and
    /// then its target is entered.A TransitionUsage can be related to some of its ownedFeatures using
    /// TransitionFeatureMembership Relationships, corresponding to the triggerAction, guardExpression and
    /// effectAction of the TransitionUsage.isComposite and owningType <> null
    /// and(owningType.oclIsKindOf(ActionDefinition) or  owningType.oclIsKindOf(ActionUsage)) andnot
    /// (owningType.oclIsKindOf(StateDefinition) or     owningType.oclIsKindOf(StateUsage)) implies   
    /// specializesFromLibrary('Actions::Action::decisionTransitions')isComposite and owningType <> null
    /// and(owningType.oclIsKindOf(StateDefinition) or owningType.oclIsKindOf(StateUsage)) implies   
    /// specializesFromLibrary('States::State::stateTransitions')specializesFromLibrary('Actions::transitionActions')source
    /// =    let sourceFeature : Feature = sourceFeature() in    if sourceFeature = null then null    else
    /// sourceFeature.featureTarget.oclAsType(ActionUsage)target =    if succession.targetFeature->isEmpty()
    /// then null    else        let targetFeature : Feature =           
    /// succession.targetFeature->first().featureTarget in        if not
    /// targetFeature.oclIsKindOf(ActionUsage) then null        else targetFeature.oclAsType(ActionUsage)   
    ///     endif    endiftriggerAction = ownedFeatureMembership->   
    /// selectByKind(TransitionFeatureMembership)->    select(kind =
    /// TransitionFeatureKind::trigger).transitionFeature->    selectByKind(AcceptActionUsage)let
    /// successions : Sequence(Successions) =     ownedMember->selectByKind(Succession)
    /// insuccessions->notEmpty() andsuccessions->at(1).targetFeature->   
    /// forAll(oclIsKindOf(ActionUsage))guardExpression = ownedFeatureMembership->   
    /// selectByKind(TransitionFeatureMembership)->    select(kind =
    /// TransitionFeatureKind::trigger).transitionFeature->   
    /// selectByKind(Expression)triggerAction->forAll(specializesFromLibrary('Actions::TransitionAction::accepter')
    /// andguardExpression->forAll(specializesFromLibrary('Actions::TransitionAction::guard')
    /// andeffectAction->forAll(specializesFromLibrary('Actions::TransitionAction::effect'))triggerAction =
    /// ownedFeatureMembership->    selectByKind(TransitionFeatureMembership)->    select(kind =
    /// TransitionFeatureKind::trigger).transitionFeatures->   
    /// selectByKind(AcceptActionUsage)succession.sourceFeature =
    /// sourceownedMember->selectByKind(BindingConnector)->exists(b |    b.relatedFeatures->includes(source)
    /// and    b.relatedFeatures->includes(inputParameter(1)))triggerAction->notEmpty() implies    let
    /// payloadParameter : Feature = inputParameter(2) in    payloadParameter <> null and   
    /// payloadParameter.subsetsChain(triggerAction->at(1),
    /// triggerPayloadParameter())ownedMember->selectByKind(BindingConnector)->exists(b |   
    /// b.relatedFeatures->includes(succession) and    b.relatedFeatures->includes(resolveGlobal(       
    /// 'TransitionPerformances::TransitionPerformance::transitionLink')))if triggerAction->isEmpty() then  
    ///  inputParameters()->size() >= 1else    inputParameters()->size() >= 2endif    succession =
    /// ownedMember->selectByKind(Succession)->at(1)
    /// </summary>
    public partial interface ITransitionUsage : IActionUsage
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
