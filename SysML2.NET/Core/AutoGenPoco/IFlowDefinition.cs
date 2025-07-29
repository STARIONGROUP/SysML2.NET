// -------------------------------------------------------------------------------------------------
// <copyright file="IFlowDefinition.cs" company="Starion Group S.A.">
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
    /// A FlowDefinition is an ActionDefinition that is also an Interaction (which is both a KerML Behavior
    /// and Association), representing flows between
    /// Usages.specializesFromLibrary('Flows::MessageAction')flowEnd->size() = 2 implies   
    /// specializesFromLibrary('Flows::Message')flowEnd->size() <= 2
    /// </summary>
    public partial interface IFlowDefinition : IActionDefinition, IInteraction
    {
        /// <summary>
        /// Queries the derived property FlowEnd
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Usage> QueryFlowEnd();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
