// -------------------------------------------------------------------------------------------------
// <copyright file="IActionUsage.cs" company="RHEA System S.A.">
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

    /// <summary>
    /// An ActionUsage is a Usage that is also a Step, and, so, is typed by a Behavior. Nominally, if the
    /// type is an ActionDefinition, an ActionUsage is a Usage of that ActionDefinition within a system.
    /// However, other kinds of kernel Behaviors are also allowed, to permit use of Behavior from the Kernel
    /// Model Libraries.specializesFromLibrary('Actions::actions')isComposite and owningType <> null
    /// and(owningType.oclIsKindOf(ActionDefinition) or owningType.oclIsKindOf(ActionUsage)) implies   
    /// specializesFromLibrary('Actions::Action::subactions')isComposite and owningType <> null
    /// and(owningType.oclIsKindOf(PartDefinition) or owningType.oclIsKindOf(PartUsage)) implies   
    /// specializesFromLibrary('Parts::Part::ownedActions')
    /// </summary>
    public partial interface IActionUsage : IOccurrenceUsage, IStep
    {
        /// <summary>
        /// Queries the derived property ActionDefinition
        /// </summary>
        List<Behavior> QueryActionDefinition();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
