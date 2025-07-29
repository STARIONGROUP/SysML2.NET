// -------------------------------------------------------------------------------------------------
// <copyright file="IFlow.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An Flow is a Step that represents the transfer of values from one Feature to another. Flows can take
    /// non-zero time to complete.specializesFromLibrary('Transfers::transfers')payloadType =    if
    /// payloadFeature = null then Sequence{}    else payloadFeature.type    endifsourceOutputFeature =   
    /// if connectorEnd->isEmpty() or         connectorEnd.ownedFeature->isEmpty()    then null    else
    /// connectorEnd.ownedFeature->first()    endiftargetInputFeature =    if connectorEnd->size() < 2 or   
    ///      connectorEnd->at(2).ownedFeature->isEmpty()    then null    else
    /// connectorEnd->at(2).ownedFeature->first()    endifflowEnd =
    /// connectorEnd->selectByKind(FlowEnd)payloadFeature =    let payloadFeatures :
    /// Sequence(PayloadFeature) =        ownedFeature->selectByKind(PayloadFeature) in    if
    /// payloadFeatures->isEmpty() then null    else payloadFeatures->first()   
    /// endifownedFeature->selectByKind(PayloadFeature)->size() <= 1ownedEndFeatures->notEmpty() implies   
    /// specializesFromLibrary('Transfers::flowTransfers')
    /// </summary>
    public partial interface IFlow : IConnector, IStep
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
