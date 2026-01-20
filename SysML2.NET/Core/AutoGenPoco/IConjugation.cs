// -------------------------------------------------------------------------------------------------
// <copyright file="IConjugation.cs" company="Starion Group S.A.">
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
    /// Conjugation is a Relationship between two types in which the conjugatedType inherits all the
    /// Features of the originalType, but with all input and output Features reversed. That is, any Features
    /// with a direction in relative to the originalType are considered to have an effective direction of
    /// out relative to the conjugatedType and, similarly, Features with direction out in the originalType
    /// are considered to have an effective direction of in in the conjugatedType. Features with direction
    /// inout, or with no direction, in the originalType, are inherited without change.A Type may
    /// participate as a conjugatedType in at most one Conjugation relationship, and such a Type may not
    /// also be the specific Type in any Specialization relationship.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1575482328287_696279_181", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IConjugation : IRelationship
    {
        /// <summary>
        /// The Type that is the result of applying Conjugation to the originalType.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575482490143_721644_299", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_696758_43228")]
        IType ConjugatedType { get; set; }

        /// <summary>
        /// The Type to be conjugated.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575482354187_108424_237", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_138197_43179")]
        IType OriginalType { get; set; }

        /// <summary>
        /// The conjugatedType of this Conjugation that is also its owningRelatedElement.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575482646809_778895_441", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1575482490143_721644_299")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_693018_16749")]
        IType owningType { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
