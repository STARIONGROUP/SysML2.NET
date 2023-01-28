// -------------------------------------------------------------------------------------------------
// <copyright file="IUsage.cs" company="RHEA System S.A.">
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
    /// A Usage is a usage of a Definition. A Usage may only be an ownedFeature of a Definition or another
    /// Usage.A Usage may have nestedUsages that model features that apply in the context of the
    /// owningUsage. A Usage may also have Definitions nested in it, but this has no semantic significance,
    /// other than the nested scoping resulting from the Usage being considered as a Namespace for any
    /// nested Definitions.However, if a Usage has isVariation = true, then it represents a variation point
    /// Usage. In this case, all of its members must be variant Usages, related to the Usage by
    /// VariantMembership Relationships. Rather than being features of the Usage, variant Usages model
    /// different concrete alternatives that can be chosen to fill in for the variation point Usage.variant
    /// = variantMembership.ownedVariantUsagevariantMembership =
    /// ownedMembership->selectByKind(VariantMembership)not isVariation implies
    /// variantMembership->isEmpty()isVariation implies variantMembership = ownedMembershipisReference = not
    /// isCompositeowningVariationDefinition <> null implies   
    /// specializes(owningVariationDefinition)owningVariationUsage <> null implies   
    /// specializes(owningVariationUsage)isVariation implies    not
    /// ownedSpecialization.specific->exists(isVariation)
    /// </summary>
    public partial interface IUsage : IFeature
    {
        /// <summary>
        /// Whether this Usage is for a variation point or not. If true, then all the memberships of the Usage
        /// must be VariantMemberships.
        /// </summary>
        bool IsVariation { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
