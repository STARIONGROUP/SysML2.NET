// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureInverting.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A FeatureInverting is a Relationship between Features asserting that their interpretations
    /// (sequences) are the reverse of each other, identified as featureInverted and invertingFeature. For
    /// example, a Feature identifying each person&#39;s parents is the inverse of a Feature identifying
    /// each person&#39;s children. A person identified as a parent of another will identify that other as
    /// one of their children.
    /// </summary>
    [Class(xmiId: "Core-Features-FeatureInverting", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFeatureInverting : IRelationship
    {
        /// <summary>
        /// The Feature that is an inverse of the invertingFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-FeatureInverting-featureInverted", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-source")]
        IFeature FeatureInverted { get; set; }

        /// <summary>
        /// The Feature that is an inverse of the invertedFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-FeatureInverting-invertingFeature", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-target")]
        IFeature InvertingFeature { get; set; }

        /// <summary>
        /// A featureInverted that is also the owningRelatedElement of this FeatureInverting.
        /// </summary>
        [Property(xmiId: "Core-Features-FeatureInverting-owningFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-FeatureInverting-featureInverted")]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-owningRelatedElement")]
        IFeature owningFeature { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
