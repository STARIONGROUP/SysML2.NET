// -------------------------------------------------------------------------------------------------
// <copyright file="IAcceptActionUsage.cs" company="Starion Group S.A.">
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
    /// An AcceptActionUsage is an ActionUsage that specifies the acceptance of an incomingTransfer from the
    /// Occurrence given by the result of its receiverArgument Expression. (If no receiverArgument is
    /// provided, the default is the this context of the AcceptActionUsage.) The payload of the accepted
    /// Transfer is output on its payloadParameter. Which Transfers may be accepted is determined by
    /// conformance to the typing and (potentially) binding of the
    /// payloadParameter.inputParameters()->size() >= 2receiverArgument = argument(2)payloadArgument =
    /// argument(1)payloadParameter =  if parameter->isEmpty() then null else parameter->first() endifnot
    /// isTriggerAction() implies    specializesFromLibrary('Actions::acceptActions')isSubactionUsage() and
    /// not isTriggerAction() implies   
    /// specializesFromLibrary('Actions::Action::acceptSubactions')isTriggerAction() implies   
    /// specializesFromLibrary('Actions::TransitionAction::accepter')payloadArgument <> null
    /// andpayloadArgument.oclIsKindOf(TriggerInvocationExpression) implies    let invocation : Expression =
    ///        payloadArgument.oclAsType(Expression) in    parameter->size() >= 2 and   
    /// invocation.parameter->size() >= 2 and           
    /// ownedFeature->selectByKind(BindingConnector)->exists(b |       
    /// b.relatedFeatures->includes(parameter->at(2)) and       
    /// b.relatedFeatures->includes(invocation.parameter->at(2)))
    /// </summary>
    public partial interface IAcceptActionUsage : IActionUsage
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
