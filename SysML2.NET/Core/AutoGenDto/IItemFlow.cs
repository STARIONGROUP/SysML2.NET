// -------------------------------------------------------------------------------------------------
// <copyright file="IItemFlow.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
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
    /// An ItemFlow is a Step that represents the transfer of objects or data values from one Feature to
    /// another. ItemFlows can take non-zero time to complete.if itemFlowEnds->isEmpty() then   
    /// specializesFromLibrary('Transfers::transfers')else   
    /// specializesFromLibrary('Transfers::flowTransfers')endifitemType =    if itemFeature = null then
    /// Sequence{}    else itemFeature.type    endifsourceOutputFeature =    if connectorEnd->isEmpty() or  
    ///       connectorEnd.ownedFeature->isEmpty()    then null    else connectorEnd.ownedFeature->first()  
    ///  endiftargetInputFeature =    if connectorEnd->size() < 2 or        
    /// connectorEnd->at(2).ownedFeature->isEmpty()    then null    else
    /// connectorEnd->at(2).ownedFeature->first()    endifitemFlowEnd =
    /// connectorEnd->selectByKind(ItemFlowEnd)itemFeature =    let itemFeatures : Sequence(ItemFeature) =  
    ///       ownedFeature->selectByKind(ItemFeature) in    if itemFeatures->isEmpty() then null    else
    /// itemFeatures->first()    endifownedFeature->selectByKind(ItemFeature)->size() <= 1
    /// </summary>
    public partial interface IItemFlow : IConnector, IStep
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
