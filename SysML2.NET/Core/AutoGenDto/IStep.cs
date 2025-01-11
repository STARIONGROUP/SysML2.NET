// -------------------------------------------------------------------------------------------------
// <copyright file="IStep.cs" company="Starion Group S.A.">
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
    /// A Step is a Feature that is typed by one or more Behaviors. Steps may be used by one Behavior to
    /// coordinate the performance of other Behaviors, supporting a steady refinement of behavioral
    /// descriptions. Steps can be ordered in time and can be connected using ItemFlows to specify things
    /// flowing between their parameters.specializesFromLibrary('Performances::performances')owningType <>
    /// null and    (owningType.oclIsKindOf(Behavior) or     owningType.oclIsKindOf(Step)) implies   
    /// specializesFromLibrary('Performances::Performance::enclosedPerformance')isComposite and owningType
    /// <> null and(owningType.oclIsKindOf(Structure) or owningType.oclIsKindOf(Feature) and
    /// owningType.oclAsType(Feature).type->    exists(oclIsKindOf(Structure)) implies   
    /// specializesFromLibrary('Objects::Object::ownedPerformance')owningType <> null and   
    /// (owningType.oclIsKindOf(Behavior) or     owningType.oclIsKindOf(Step)) and    self.isComposite
    /// implies    specializesFromLibrary('Performances::Performance::subperformance')behavior =
    /// type->selectByKind(Behavior)
    /// </summary>
    public partial interface IStep : IFeature
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
