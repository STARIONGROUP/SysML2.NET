// -------------------------------------------------------------------------------------------------
// <copyright file="IRenderingUsage.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2024 Starion Group S.A.
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
    /// A RenderingUsage is the usage of a RenderingDefinition to specify the rendering of a specific model
    /// view to produce a physical view artifact.specializesFromLibrary('Views::renderings')owningType <>
    /// null and(owningType.oclIsKindOf(RenderingDefinition) or owningType.oclIsKindOf(RenderingUsage))
    /// implies    specializesFromLibrary('Views::Rendering::subrenderings')owningFeatureMembership <> null
    /// andowningFeatureMembership.oclIsKindOf(ViewRenderingMembership) implies   
    /// redefinesFromLibrary('Views::View::viewRendering')
    /// </summary>
    public partial interface IRenderingUsage : IPartUsage
    {
        /// <summary>
        /// Queries the derived property RenderingDefinition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        RenderingDefinition QueryRenderingDefinition();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
