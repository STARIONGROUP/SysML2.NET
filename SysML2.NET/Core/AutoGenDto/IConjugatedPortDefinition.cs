// -------------------------------------------------------------------------------------------------
// <copyright file="IConjugatedPortDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Ports
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    /// <summary>
    /// A ConjugatedPortDefinition is a PortDefinition that is a PortDefinition of its original
    /// PortDefinition. That is, a ConjugatedPortDefinition inherits all the features of the original
    /// PortDefinition, but input flows of the original PortDefinition become outputs on the
    /// ConjugatedPortDefinition and output flows of the original PortDefinition become inputs on the
    /// ConjugatedPortDefinition. Every PortDefinition (that is not itself a ConjugatedPortDefinition) has
    /// exactly one corresponding ConjugatedPortDefinition, whose effective name is the name of the
    /// originalPortDefinition, with the character ~ prepended.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1575484160733_882684_674", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IConjugatedPortDefinition : IPortDefinition
    {
        /// <summary>
        /// The original PortDefinition for this ConjugatedPortDefinition, which is the owningNamespace of the
        /// ConjugatedPortDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575484364017_387810_990", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674986_474739_43306")]
        Guid originalPortDefinition { get; }

        /// <summary>
        /// The PortConjugation that is the ownedConjugator of this ConjugatedPortDefinition, linking it to its
        /// originalPortDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575484344901_850046_947", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1575482646809_280165_440")]
        Guid ownedPortConjugator { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
