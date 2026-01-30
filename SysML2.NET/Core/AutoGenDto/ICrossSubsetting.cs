// -------------------------------------------------------------------------------------------------
// <copyright file="ICrossSubsetting.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Decorators;

    /// <summary>
    /// CrossSubsetting is a kind of Subsetting for end Features, as identified by crossingFeature, to
    /// subset a chained Feature, identified by crossedFeature. It navigates to instances of the end
    /// Feature’s type from instances of other end Feature types on the same owningType (at least two end
    /// Features are required for any of them to have a CrossSubsetting).  The crossedFeature of a
    /// CrossSubsetting must have a feature chain of exactly two Features. The second Feature in the chain
    /// is the crossFeature of the crossingFeature (end Feature), which has the same type as the
    /// crossingFeature. When the owningType of the crossingFeature has exactly two end Features, the first
    /// Feature in the chain of the crossedFeature is the other end Feature. The crossFeature’s
    /// featuringType in this case is the other end Feature. When the owningType has more than two end
    /// Features, the first Feature in the chain is a Feature that CrossMultiplies all the other end
    /// Features, which is also the featuringType of the crossFeature.  A crossFeature must be owned by its
    /// featureCrossing (end Feature) when the featureCrossing owningType has more than two end Features.
    /// Otherwise, for exactly two end Features, the crossFeatures of each the ends can instead optionally
    /// be inherited by the other end from one of its types or a subsetted Feature.
    /// </summary>
    [Class(xmiId: "Core-Features-CrossSubsetting", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface ICrossSubsetting : ISubsetting
    {
        /// <summary>
        /// The chained Feature that is cross subset by the crossingFeature of this CrossSubsetting.
        /// </summary>
        [Property(xmiId: "Core-Features-CrossSubsetting-crossedFeature", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Features-Subsetting-subsettedFeature")]
        Guid CrossedFeature { get; set; }

        /// <summary>
        /// The end Feature that owns this CrossSubsetting relationship and is also its subsettingFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-CrossSubsetting-crossingFeature", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Features-Subsetting-owningFeature")]
        [RedefinedProperty(propertyName: "Core-Features-Subsetting-subsettingFeature")]
        Guid crossingFeature { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
