// -------------------------------------------------------------------------------------------------
// <copyright file="ITypeFeaturing.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Core.Features
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
    /// A TypeFeaturing is a Featuring Relationship in which the featureOfType is the source and the
    /// featuringType is the target.
    /// </summary>
    [Class(xmiId: "Core-Features-TypeFeaturing", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface ITypeFeaturing : IRelationship
    {
        /// <summary>
        /// The Feature that is featured by the featuringType. It is the source of the TypeFeaturing.
        /// </summary>
        [Property(xmiId: "Core-Features-TypeFeaturing-featureOfType", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-source")]
        IFeature FeatureOfType { get; set; }

        /// <summary>
        /// The Type that features the featureOfType. It is the target of the TypeFeaturing.
        /// </summary>
        [Property(xmiId: "Core-Features-TypeFeaturing-featuringType", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-target")]
        IType FeaturingType { get; set; }

        /// <summary>
        /// A featureOfType that is also the owningRelatedElement of this TypeFeaturing.
        /// </summary>
        [Property(xmiId: "Core-Features-TypeFeaturing-owningFeatureOfType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-owningRelatedElement")]
        [SubsettedProperty(propertyName: "Core-Features-TypeFeaturing-featureOfType")]
        IFeature owningFeatureOfType { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
