// -------------------------------------------------------------------------------------------------
// <copyright file="IConjugatedPortTyping.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Ports
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ConjugatedPortTyping is a FeatureTyping whose type is a ConjugatedPortDefinition. (This
    /// relationship is intended to be an abstract-syntax marker for a special surface notation for
    /// conjugated typing of ports.)
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1577914899997_653496_45", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IConjugatedPortTyping : IFeatureTyping
    {
        /// <summary>
        /// The type of this ConjugatedPortTyping considered as a FeatureTyping, which must be a
        /// ConjugatedPortDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577915017970_186033_146", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1543180520185_480887_21131")]
        Guid ConjugatedPortDefinition { get; set; }

        /// <summary>
        /// The originalPortDefinition of the conjugatedPortDefinition of this ConjugatedPortTyping.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1577915013583_787601_133", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid portDefinition { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
