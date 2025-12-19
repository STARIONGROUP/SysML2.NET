// -------------------------------------------------------------------------------------------------
// <copyright file="IMetadataUsage.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Core.DTO.Systems.Metadata
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Kernel.Metadata;
    using SysML2.NET.Core.DTO.Systems.Items;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A  MetadataUsage is a Usage and a MetadataFeature, used to annotate other Elements in a system model
    /// with metadata. As a MetadataFeature, its type must be a Metaclass, which will nominally be a
    /// MetadataDefinition. However, any kernel Metaclass is also allowed, to permit use of Metaclasses from
    /// the Kernel Model Libraries.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1645121476406_921183_398", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IMetadataUsage : IItemUsage, IMetadataFeature
    {
        /// <summary>
        /// The MetadataDefinition that is the definition of this MetadataUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1647727047674_847094_2563", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1565471361757_649736_20796")]
        [RedefinedProperty(propertyName: "_19_0_4_12e503d9_1606345564958_925589_327")]
        Guid? MetadataDefinition { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
