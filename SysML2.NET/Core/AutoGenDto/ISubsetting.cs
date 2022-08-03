// -------------------------------------------------------------------------------------------------
// <copyright file="ISubsetting.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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

    /// <summary>
    /// Subsetting is Generalization in which the specific and general Types that are Features. This means
    /// all values of the subsettingFeature (on instances of its domain, i.e., the intersection of its
    /// featuringTypes) are values of the subsettedFeature on instances of its domain.  To support
    /// this, the domain of the subsettingFeature must be the same or specialize (at least
    /// indirectly) the domain of the subsettedFeature (via Generalization), and the range
    /// (intersection of a Feature&#39;s types) of the subsettingFeature must specialize the range of the
    /// subsettedFeature. The subsettedFeature is imported into the owningNamespace of the
    /// subsettingFeature (if it is not already in that namespace), requiring the names of the
    /// subsettingFeature and subsettedFeature to be different.
    /// </summary>
    public partial interface ISubsetting : ISpecialization
    {
        /// <summary>
        /// </summary>
        Guid SubsettedFeature { get; set; }

        /// <summary>
        /// </summary>
        Guid SubsettingFeature { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
