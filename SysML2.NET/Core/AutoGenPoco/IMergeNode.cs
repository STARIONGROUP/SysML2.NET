// -------------------------------------------------------------------------------------------------
// <copyright file="IMergeNode.cs" company="Starion Group S.A.">
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
    /// A MergeNode is a ControlNode that asserts the merging of its incoming Successions. A MergeNode may
    /// have at most one outgoing Successions.sourceConnector->selectAsKind(Succession)->size() <=
    /// 1targetConnector->selectByKind(Succession)->    collect(connectorEnd->at(1))->    forAll(sourceMult
    /// |        multiplicityHasBounds(sourceMult, 0, 1))targetConnector->selectByKind(Succession)->   
    /// forAll(subsetsChain(self,        
    /// resolveGlobal('ControlPerformances::MergePerformance::incomingHBLink')))specializesFromLibrary('Actions::Action::merges')
    /// </summary>
    public partial interface IMergeNode : IControlNode
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
