// -------------------------------------------------------------------------------------------------
// <copyright file="IItemUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Items
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Systems.Occurrences;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An ItemUsage is a ItemUsage whose definition is a Structure. Nominally, if the definition is an
    /// ItemDefinition, an ItemUsage is a ItemUsage of that ItemDefinition within a system. However, other
    /// kinds of Kernel Structures are also allowed, to permit use of Structures from the Kernel Model
    /// Libraries.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1565480460114_846184_24270", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IItemUsage : IOccurrenceUsage
    {
        /// <summary>
        /// The Structures that are the definitions of this ItemUsage. Nominally, these are ItemDefinitions, but
        /// other kinds of Kernel Structures are also allowed, to permit use of Structures from the Kernel
        /// Library.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565471361757_649736_20796", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943843466_158863_236")]
        List<Guid> ItemDefinition { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
