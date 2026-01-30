// -------------------------------------------------------------------------------------------------
// <copyright file="IConnectionDefinition.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.DTO.Kernel.Associations;
    using SysML2.NET.Core.DTO.Systems.Parts;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ConnectionDefinition is a PartDefinition that is also an AssociationStructure. The end Features of
    /// a ConnectionDefinition must be Usages.
    /// </summary>
    [Class(xmiId: "Systems-Connections-ConnectionDefinition", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IConnectionDefinition : IAssociationStructure, IPartDefinition
    {
        /// <summary>
        /// The Usages that define the things related by the ConnectionDefinition.
        /// </summary>
        [Property(xmiId: "Systems-Connections-ConnectionDefinition-connectionEnd", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Kernel-Associations-Association-associationEnd")]
        List<Guid> connectionEnd { get; }

        /// <summary>
        /// A ConnectionDefinition always has isSufficient = true.
        /// </summary>
        [Property(xmiId: "Systems-Connections-ConnectionDefinition-isSufficient", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "true")]
        [RedefinedProperty(propertyName: "Core-Types-Type-isSufficient")]
        new bool IsSufficient { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
