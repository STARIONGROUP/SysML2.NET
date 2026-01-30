// -------------------------------------------------------------------------------------------------
// <copyright file="IViewUsage.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Systems.Parts;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ViewUsage is a usage of a ViewDefinition to specify the generation of a view of the members of a
    /// collection of exposedNamespaces. The ViewUsage can satisfy more viewpoints than its definition, and
    /// it can specialize the viewRendering specified by its definition.
    /// </summary>
    [Class(xmiId: "Systems-Views-ViewUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IViewUsage : IPartUsage
    {
        /// <summary>
        /// The Elements that are exposed by this ViewUsage, which are those memberElements of the imported
        /// Memberships from all the Expose Relationships that meet all the owned and inherited viewConditions.
        /// </summary>
        [Property(xmiId: "Systems-Views-ViewUsage-exposedElement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-member")]
        List<Guid> exposedElement { get; }

        /// <summary>
        /// The nestedRequirements of this ViewUsage that are ViewpointUsages for (additional) viewpoints
        /// satisfied by the ViewUsage.
        /// </summary>
        [Property(xmiId: "Systems-Views-ViewUsage-satisfiedViewpoint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedRequirement")]
        List<Guid> satisfiedViewpoint { get; }

        /// <summary>
        /// The Expressions related to this ViewUsage by ElementFilterMemberships, which specify conditions on
        /// Elements to be rendered in a view.
        /// </summary>
        [Property(xmiId: "Systems-Views-ViewUsage-viewCondition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMember")]
        List<Guid> viewCondition { get; }

        /// <summary>
        /// The ViewDefinition that is the definition of this ViewUsage.
        /// </summary>
        [Property(xmiId: "Systems-Views-ViewUsage-viewDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Systems-Parts-PartUsage-partDefinition")]
        Guid? viewDefinition { get; }

        /// <summary>
        /// The RenderingUsage to be used to render views defined by this ViewUsage, which is the
        /// referencedRendering of the ViewRenderingMembership of the ViewUsage.
        /// </summary>
        [Property(xmiId: "Systems-Views-ViewUsage-viewRendering", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? viewRendering { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
