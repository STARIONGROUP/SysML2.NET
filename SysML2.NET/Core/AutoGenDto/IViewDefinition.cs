// -------------------------------------------------------------------------------------------------
// <copyright file="IViewDefinition.cs" company="Starion Group S.A.">
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
    /// A ViewDefinition is a PartDefinition that specifies how a view artifact is constructed to satisfy a
    /// viewpoint. It specifies a viewConditions to define the model content to be presented and a
    /// viewRendering to define how the model content is presented.view =
    /// usage->selectByKind(ViewUsage)satisfiedViewpoint = ownedRequirement->   
    /// selectByKind(ViewpointUsage)->    select(isComposite)viewRendering =    let renderings:
    /// OrderedSet(ViewRenderingMembership) =       
    /// featureMembership->selectByKind(ViewRenderingMembership) in    if renderings->isEmpty() then null   
    /// else renderings->first().referencedRendering    endifviewCondition = ownedMembership->   
    /// selectByKind(ElementFilterMembership).    conditionfeatureMembership->   
    /// selectByKind(ViewRenderingMembership)->    size() <= 1specializesFromLibrary('Views::View')
    /// </summary>
    public partial interface IViewDefinition : IPartDefinition
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
