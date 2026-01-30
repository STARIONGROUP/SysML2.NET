// -------------------------------------------------------------------------------------------------
// <copyright file="IDifferencing.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Core.Types
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// Differencing is a Relationship that makes its differencingType one of the differencingTypes of its
    /// typeDifferenced.
    /// </summary>
    [Class(xmiId: "Core-Types-Differencing", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IDifferencing : IRelationship
    {
        /// <summary>
        /// Type that partly determines interpretations of typeDifferenced, as described in
        /// Type::differencingType.
        /// </summary>
        [Property(xmiId: "Core-Types-Differencing-differencingType", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-target")]
        Guid DifferencingType { get; set; }

        /// <summary>
        /// Type with interpretations partly determined by differencingType, as described in
        /// Type::differencingType.
        /// </summary>
        [Property(xmiId: "Core-Types-Differencing-typeDifferenced", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-owningRelatedElement")]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-source")]
        Guid typeDifferenced { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
