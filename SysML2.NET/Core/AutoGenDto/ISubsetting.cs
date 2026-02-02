// -------------------------------------------------------------------------------------------------
// <copyright file="ISubsetting.cs" company="Starion Group S.A.">
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
    /// Subsetting is Specialization in which the specific and general Types are Features. This means all
    /// values of the subsettingFeature (on instances of its domain, i.e., the intersection of its
    /// featuringTypes) are values of the subsettedFeature on instances of its domain. To support this the
    /// domain of the subsettingFeature must be the same or specialize (at least indirectly) the domain of
    /// the subsettedFeature (via Specialization), and the co-domain (intersection of the types) of the
    /// subsettingFeature must specialize the co-domain of the subsettedFeature.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1533160651710_980688_42209", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface ISubsetting : ISpecialization
    {
        /// <summary>
        /// A subsettingFeature that is also the owningRelatedElement of this Subsetting.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674987_236250_43311", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674967_140305_43206")]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_573157_43226")]
        Guid? owningFeature { get; }

        /// <summary>
        /// The Feature that is subsetted by the subsettingFeature of this Subsetting.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674961_393191_43181", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674980_563969_43273")]
        Guid SubsettedFeature { get; set; }

        /// <summary>
        /// The Feature that is a subset of the subsettedFeature of this Subsetting.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674967_140305_43206", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674982_253967_43281")]
        Guid SubsettingFeature { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
