﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IReferenceSubsetting.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
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
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// ReferenceSubsetting is a kind of Subsetting in which the referencedFeature is syntactically
    /// distinguished from other Features subsetted by the referencingFeature. ReferenceSubsetting has the
    /// same semantics as Subsetting, but the referencedFeature may have a special purpose relative to the
    /// referencingFeature. For instance, ReferenceSubsetting is used to identify the relatedFeatures of a
    /// Connector.ReferenceSubsetting is always an ownedRelationship of its referencingFeature. A Feature
    /// can have at most one ownedReferenceSubsetting.
    /// </summary>
    public partial interface IReferenceSubsetting : ISubsetting
    {
        /// <summary>
        /// The Feature that is referenced by the referencingFeature of this ReferenceSubsetting.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Guid ReferencedFeature { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
