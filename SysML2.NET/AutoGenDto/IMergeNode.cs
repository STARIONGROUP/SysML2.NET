// -------------------------------------------------------------------------------------------------
// <copyright file="IMergeNode.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A MergeNode is a ControlNode that asserts the merging of its incoming Successions. All incoming
    /// Successions must have a source multiplicity of 0..1 and subset the Feature
    /// MergeAction::incomingHBLink. A MergeNode may have at most one outgoing Succession.A MergeNode must
    /// subset, directly or indirectly, the ActionUsage Action::merges, implying that it is typed by
    /// MergeAction from the Systems model library (or a subtype of it).
    /// </summary>
    public partial interface IMergeNode : IControlNode
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
