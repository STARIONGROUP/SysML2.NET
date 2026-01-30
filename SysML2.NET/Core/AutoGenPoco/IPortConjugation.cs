// -------------------------------------------------------------------------------------------------
// <copyright file="IPortConjugation.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Ports
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A PortConjugation is a Conjugation Relationship between a PortDefinition and its corresponding
    /// ConjugatedPortDefinition. As a result of this Relationship, the ConjugatedPortDefinition inherits
    /// all the features of the original PortDefinition, but input flows of the original PortDefinition
    /// become outputs on the ConjugatedPortDefinition and output flows of the original PortDefinition
    /// become inputs on the ConjugatedPortDefinition.
    /// </summary>
    [Class(xmiId: "Systems-Ports-PortConjugation", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IPortConjugation : IConjugation
    {
        /// <summary>
        /// The ConjugatedPortDefinition that is conjugate to the originalPortDefinition.
        /// </summary>
        [Property(xmiId: "Systems-Ports-PortConjugation-conjugatedPortDefinition", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Types-Conjugation-owningType")]
        IConjugatedPortDefinition conjugatedPortDefinition { get; }

        /// <summary>
        /// The PortDefinition being conjugated.
        /// </summary>
        [Property(xmiId: "Systems-Ports-PortConjugation-originalPortDefinition", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Types-Conjugation-originalType")]
        IPortDefinition OriginalPortDefinition { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
