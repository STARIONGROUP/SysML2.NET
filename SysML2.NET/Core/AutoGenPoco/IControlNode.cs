// -------------------------------------------------------------------------------------------------
// <copyright file="IControlNode.cs" company="RHEA System S.A.">
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
    /// A ControlNode is an ActionUsage that does not have any inherent behavior but provides constraints on
    /// incoming and outgoing Succession Connectors that are used to control other Actions.A ControlNode
    /// must be a composite owned feature of an ActionDefinition or ActionUsage, subsetting, directly or
    /// indirectly, the ActionUsage Action::controls. This implies that the ControlNode is typed by
    /// ControlAction from the Systems model library, or a subtype of it.All outgoing Successions from a
    /// ControlNode must have source multiplicity of 1..1. All incoming Succession must have target
    /// multiplicity of 1..1.
    /// </summary>
    public partial interface IControlNode : IActionUsage
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
