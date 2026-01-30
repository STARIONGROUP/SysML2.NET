// -------------------------------------------------------------------------------------------------
// <copyright file="IConnector.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Kernel.Connectors
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Connector is a usage of Associations, with links restricted according to instances of the Type in
    /// which they are used (domain of the Connector). The associations of the Connector restrict what kinds
    /// of things might be linked. The Connector further restricts these links to be between values of
    /// Features on instances of its domain.
    /// </summary>
    [Class(xmiId: "Kernel-Connectors-Connector", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IConnector : IFeature, IRelationship
    {
        /// <summary>
        /// The Associations that type the Connector.
        /// </summary>
        [Property(xmiId: "Kernel-Connectors-Connector-association", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Features-Feature-type")]
        List<Guid> association { get; }

        /// <summary>
        /// The endFeatures of a Connector, which redefine the endFeatures of the associations of the Connector.
        /// The connectorEnds determine via ReferenceSubsetting Relationships which Features are related by the
        /// Connector.
        /// </summary>
        [Property(xmiId: "Kernel-Connectors-Connector-connectorEnd", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Types-Type-endFeature")]
        List<Guid> connectorEnd { get; }

        /// <summary>
        /// The innermost Type that is a common direct or indirect featuringType of the relatedFeatures, such
        /// that, if it exists and was the featuringType of this Connector, the Connector would satisfy the
        /// checkConnectorTypeFeaturing constraint.
        /// </summary>
        [Property(xmiId: "Kernel-Connectors-Connector-defaultFeaturingType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? defaultFeaturingType { get; }

        /// <summary>
        /// The Features that are related by this Connector considered as a Relationship and that restrict the
        /// links it identifies, given by the referenced Features of the connectorEnds of the Connector.
        /// </summary>
        [Property(xmiId: "Kernel-Connectors-Connector-relatedFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-relatedElement")]
        List<Guid> relatedFeature { get; }

        /// <summary>
        /// The source relatedFeature for this Connector. It is the first relatedFeature.
        /// </summary>
        [Property(xmiId: "Kernel-Connectors-Connector-sourceFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Connectors-Connector-relatedFeature")]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-source")]
        Guid? sourceFeature { get; }

        /// <summary>
        /// The target relatedFeatures for this Connector. This includes all the relatedFeatures other than the
        /// sourceFeature.
        /// </summary>
        [Property(xmiId: "Kernel-Connectors-Connector-targetFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Connectors-Connector-relatedFeature")]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-target")]
        List<Guid> targetFeature { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
