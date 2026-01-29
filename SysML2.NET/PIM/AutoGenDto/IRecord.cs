// -------------------------------------------------------------------------------------------------
// <copyright file="IRecord.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PIM.DTO.API_Model
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    /// <summary>
    /// The most common form of data in the Semantic Web
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1577820468975_133215_65988", isAbstract: true, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IRecord
    {
        /// <summary>
        /// A collection of other identifiers for this record, especially if the record was created or
        /// represented in other software applications and systems.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469740_197983_66089", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<string> Alias { get; set; }

        /// <summary>
        /// A statement that provides details about the record.
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1629741832147_14565_28133", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string Description { get; set; }

        /// <summary>
        /// The UUID assigned to th Record. This identifier shall conform to RFC 4122.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469739_885136_66087", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: true, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid Id { get; }

        /// <summary>
        /// An optional human-friendly identifier for a record. The value assigned to the name for a given
        /// record must be in the set of values assigned to alias for that record.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469741_716007_66090", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1577820469740_197983_66089")]
        string Name { get; set; }

        /// <summary>
        /// An IRI assigned to the record (for linked data access).
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577820469740_90190_66088", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Uri? ResourceIdentifier { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
