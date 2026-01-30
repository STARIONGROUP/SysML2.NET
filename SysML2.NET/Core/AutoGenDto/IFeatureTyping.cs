// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureTyping.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Core.Features
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Core.Types;
    using SysML2.NET.Decorators;

    /// <summary>
    /// FeatureTyping is Specialization in which the specific Type is a Feature. This means the set of
    /// instances of the (specific) typedFeature is a subset of the set of instances of the (general) type.
    /// In the simplest case, the type is a Classifier, whereupon the typedFeature has values that are
    /// instances of the Classifier.
    /// </summary>
    [Class(xmiId: "Core-Features-FeatureTyping", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFeatureTyping : ISpecialization
    {
        /// <summary>
        /// A typedFeature that is also the owningRelatedElement of this FeatureTyping.
        /// </summary>
        [Property(xmiId: "Core-Features-FeatureTyping-owningFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-FeatureTyping-typedFeature")]
        [RedefinedProperty(propertyName: "Core-Types-Specialization-owningType")]
        Guid? owningFeature { get; }

        /// <summary>
        /// The Type that is being applied by this FeatureTyping.
        /// </summary>
        [Property(xmiId: "Core-Features-FeatureTyping-type", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Types-Specialization-general")]
        Guid Type { get; set; }

        /// <summary>
        /// The Feature that has a type determined by this FeatureTyping.
        /// </summary>
        [Property(xmiId: "Core-Features-FeatureTyping-typedFeature", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Types-Specialization-specific")]
        Guid TypedFeature { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
