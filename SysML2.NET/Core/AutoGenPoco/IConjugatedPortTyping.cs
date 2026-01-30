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

namespace SysML2.NET.Core.POCO.Systems.Ports
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ConjugatedPortTyping is a FeatureTyping whose type is a ConjugatedPortDefinition. (This
    /// relationship is intended to be an abstract-syntax marker for a special surface notation for
    /// conjugated typing of ports.)
    /// </summary>
    [Class(xmiId: "Systems-Ports-ConjugatedPortTyping", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IConjugatedPortTyping : IFeatureTyping
    {
        /// <summary>
        /// The type of this ConjugatedPortTyping considered as a FeatureTyping, which must be a
        /// ConjugatedPortDefinition.
        /// </summary>
        [Property(xmiId: "Systems-Ports-ConjugatedPortTyping-conjugatedPortDefinition", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Features-FeatureTyping-type")]
        IConjugatedPortDefinition ConjugatedPortDefinition { get; set; }

        /// <summary>
        /// The originalPortDefinition of the conjugatedPortDefinition of this ConjugatedPortTyping.
        /// </summary>
        [Property(xmiId: "Systems-Ports-ConjugatedPortTyping-portDefinition", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        IPortDefinition portDefinition { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
