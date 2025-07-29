﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IControlNode.cs" company="Starion Group S.A.">
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
    /// A ControlNode is an ActionUsage that does not have any inherent behavior but provides constraints on
    /// incoming and outgoing Successions that are used to control other Actions. A ControlNode must be a
    /// composite owned usage of an ActionDefinition or
    /// ActionUsage.sourceConnector->selectByKind(Succession)->   
    /// collect(connectorEnd->at(1).multiplicity)->    forAll(sourceMult |        
    /// multiplicityHasBounds(sourceMult, 1, 1))owningType <> null and
    /// (owningType.oclIsKindOf(ActionDefinition) or
    /// owningType.oclIsKindOf(ActionUsage))targetConnector->selectByKind(Succession)->   
    /// collect(connectorEnd->at(2).multiplicity)->    forAll(targetMult |        
    /// multiplicityHasBounds(targetMult, 1,
    /// 1))specializesFromLibrary('Action::Action::controls')isComposite
    /// </summary>
    public partial interface IControlNode : IActionUsage
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
