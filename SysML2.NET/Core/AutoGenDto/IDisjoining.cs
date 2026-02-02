// -------------------------------------------------------------------------------------------------
// <copyright file="IDisjoining.cs" company="Starion Group S.A.">
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
    /// A Disjoining is a Relationship between Types asserted to have interpretations that are not shared
    /// (disjoint) between them, identified as typeDisjoined and disjoiningType. For example, a Classifier
    /// for mammals is disjoint from a Classifier for minerals, and a Feature for people&#39;s parents is
    /// disjoint from a Feature for their children.
    /// </summary>
    [Class(xmiId: "_19_0_4_b9102da_1623182941809_239395_557", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IDisjoining : IRelationship
    {
        /// <summary>
        /// Type asserted to be disjoint with the typeDisjoined.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1623183201866_537160_629", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_138197_43179")]
        Guid DisjoiningType { get; set; }

        /// <summary>
        /// A typeDisjoined that is also an owningRelatedElement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1627447519614_499771_371", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_693018_16749")]
        [SubsettedProperty(propertyName: "_19_0_4_b9102da_1623183194914_955906_617")]
        Guid? owningType { get; }

        /// <summary>
        /// Type asserted to be disjoint with the disjoiningType.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1623183194914_955906_617", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_696758_43228")]
        Guid TypeDisjoined { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
