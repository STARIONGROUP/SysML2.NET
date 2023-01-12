// -------------------------------------------------------------------------------------------------
// <copyright file="IMultiplicity.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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
    /// A Multiplicity is a Feature whose co-domain is a set of natural numbers that includes the
    /// number of sequences determined below, based on the kind
    /// of typeWithMultiplicity:<ul>	<li>Classifiers: minimal sequences (the single length sequences of
    /// the Classifier).</li>	<li>Features: sequences with the same feature-pair head.  In the case of
    /// Features with Classifiers as domain and co-domain, these sequences are pairs, with the first element
    /// in a single-length sequence of the domain Classifier (head of the pair), and the number of
    /// pairs with the same first element being among the Multiplicity co-domain
    /// numbers.</li></ul>Multiplicity co-domains (in models) can be specified by Expression that might vary
    /// in their results. If the typeWithMultiplicity is a Classifier, the domain of the Multiplicity shall
    /// be Anything.  If the typeWithMultiplicity is a Feature,  the Multiplicity shall have the same domain
    /// as the typeWithMultiplicity.
    /// </summary>
    public partial interface IMultiplicity : IFeature
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
