// -------------------------------------------------------------------------------------------------
// <copyright file="IConnectionUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Connections
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Systems.Parts;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ConnectionUsage is a ConnectorAsUsage that is also a PartUsage. Nominally, if its type is a
    /// ConnectionDefinition, then a ConnectionUsage is a Usage of that ConnectionDefinition, representing a
    /// connection between parts of a system. However, other kinds of kernel AssociationStructures are also
    /// allowed, to permit use of AssociationStructures from the Kernel Model Libraries.
    /// </summary>
    [Class(xmiId: "Systems-Connections-ConnectionUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IConnectionUsage : IPartUsage, IConnectorAsUsage
    {
        /// <summary>
        /// The AssociationStructures that are the types of this ConnectionUsage. Nominally, these are , but
        /// other kinds of Kernel AssociationStructures are also allowed, to permit use of AssociationStructures
        /// from the Kernel Model Libraries
        /// </summary>
        [Property(xmiId: "Systems-Connections-ConnectionUsage-connectionDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-Items-ItemUsage-itemDefinition")]
        [RedefinedProperty(propertyName: "Kernel-Connectors-Connector-association")]
        List<Guid> connectionDefinition { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
