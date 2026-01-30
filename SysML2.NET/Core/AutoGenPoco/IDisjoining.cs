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

namespace SysML2.NET.Core.POCO.Core.Types
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Disjoining is a Relationship between Types asserted to have interpretations that are not shared
    /// (disjoint) between them, identified as typeDisjoined and disjoiningType. For example, a Classifier
    /// for mammals is disjoint from a Classifier for minerals, and a Feature for people&#39;s parents is
    /// disjoint from a Feature for their children.
    /// </summary>
    [Class(xmiId: "Core-Types-Disjoining", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IDisjoining : IRelationship
    {
        /// <summary>
        /// Type asserted to be disjoint with the typeDisjoined.
        /// </summary>
        [Property(xmiId: "Core-Types-Disjoining-disjoiningType", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-target")]
        IType DisjoiningType { get; set; }

        /// <summary>
        /// A typeDisjoined that is also an owningRelatedElement.
        /// </summary>
        [Property(xmiId: "Core-Types-Disjoining-owningType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-owningRelatedElement")]
        [SubsettedProperty(propertyName: "Core-Types-Disjoining-typeDisjoined")]
        IType owningType { get; }

        /// <summary>
        /// Type asserted to be disjoint with the disjoiningType.
        /// </summary>
        [Property(xmiId: "Core-Types-Disjoining-typeDisjoined", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-source")]
        IType TypeDisjoined { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
