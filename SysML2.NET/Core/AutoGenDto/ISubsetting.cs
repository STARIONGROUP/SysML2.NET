// -------------------------------------------------------------------------------------------------
// <copyright file="ISubsetting.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2024 Starion Group S.A.
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
    /// Subsetting is Specialization in which the specific and general Types are Features. This means all
    /// values of the subsettingFeature (on instances of its domain, i.e., the intersection of its
    /// featuringTypes) are values of the subsettedFeature on instances of its domain. To support this the
    /// domain of the subsettingFeature must be the same or specialize (at least indirectly) the domain of
    /// the subsettedFeature (via Specialization), and the co-domain (intersection of the types) of the
    /// subsettingFeature must specialize the co-domain of the subsettedFeature.let
    /// subsettingFeaturingTypes: OrderedSet(Type) =    subsettingFeature.featuringTypes inlet
    /// subsettedFeaturingTypes: OrderedSet(Type) =    subsettedFeature.featuringTypes inlet anythingType:
    /// Element =    subsettingFeature.resolveGlobal('Base::Anything').memberElement in
    /// subsettedFeaturingTypes->forAll(t |    subsettingFeaturingTypes->isEmpty() and t = anythingType or  
    ///  subsettingFeaturingTypes->exists(specializes(t))subsettedFeature.isUnique implies
    /// subsettingFeature.isUnique
    /// </summary>
    public partial interface ISubsetting : ISpecialization
    {
        /// <summary>
        /// The Feature that is subsetted by the subsettingFeature of this Subsetting.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Guid SubsettedFeature { get; set; }

        /// <summary>
        /// The Feature that is a subset of the subsettedFeature of this Subsetting.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Guid SubsettingFeature { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
