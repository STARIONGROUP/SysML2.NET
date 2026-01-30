// -------------------------------------------------------------------------------------------------
// <copyright file="IViewRenderingMembership.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Core.DTO.Systems.Views
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.DTO.Core.Types;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ViewRenderingMembership is a <coed>FeatureMembership that identifies the viewRendering of a
    /// ViewDefinition or ViewUsage.</coed>
    /// </summary>
    [Class(xmiId: "Systems-Views-ViewRenderingMembership", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IViewRenderingMembership : IFeatureMembership
    {
        /// <summary>
        /// The owned RenderingUsage that is either itself the referencedRendering or subsets the
        /// referencedRendering.
        /// </summary>
        [Property(xmiId: "Systems-Views-ViewRenderingMembership-ownedRendering", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Types-FeatureMembership-ownedMemberFeature")]
        Guid ownedRendering { get; }

        /// <summary>
        /// The RenderingUsage that is referenced through this ViewRenderingMembership. It is the
        /// referencedFeature of the ownedReferenceSubsetting for the ownedRendering, if there is one, and,
        /// otherwise, the ownedRendering itself.
        /// </summary>
        [Property(xmiId: "Systems-Views-ViewRenderingMembership-referencedRendering", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid referencedRendering { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
