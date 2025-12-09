// -------------------------------------------------------------------------------------------------
// <copyright file="IEnumerationDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;


    using SysML2.NET.Decorators;

    /// <summary>
    /// An EnumerationDefinition is an AttributeDefinition all of whose instances are given by an explicit
    /// list of enumeratedValues. This is realized by requiring that the EnumerationDefinition have
    /// isVariation = true, with the enumeratedValues being its variants.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1606946467364_179493_153", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IEnumerationDefinition : IAttributeDefinition
    {
        /// <summary>
        /// EnumerationUsages of this EnumerationDefinitionthat have distinct, fixed values. Each
        /// enumeratedValue specifies one of the allowed instances of the EnumerationDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1606946634788_959145_265", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1590979457191_746167_951")]
        List<Guid> EnumeratedValue { get; }

        /// <summary>
        /// An EnumerationDefinition is considered semantically to be a variation whose allowed variants are its
        /// enumerationValues.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1606946783667_895456_287", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "true")]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1590978283180_265362_419")]
        bool IsVariation { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
