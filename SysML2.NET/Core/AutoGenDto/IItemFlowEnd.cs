﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IItemFlowEnd.cs" company="Starion Group S.A.">
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
    /// An ItemFlowEnd is a Feature that is one of the connectorEnds giving the source or target of an
    /// ItemFlow. For ItemFlows typed by FlowTransfer or its specializations, ItemFlowEnds must have exactly
    /// one ownedFeature, which redefines Transfer::source::sourceOutput or Transfer::target::targetInput
    /// and redefines the corresponding feature of the relatedElement for its end.owningType <> null and
    /// owningType.oclIsKindOf(ItemFlow)isEndownedFeature->size() = 1
    /// </summary>
    public partial interface IItemFlowEnd : IFeature
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
