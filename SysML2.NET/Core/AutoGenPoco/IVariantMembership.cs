// -------------------------------------------------------------------------------------------------
// <copyright file="IVariantMembership.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A VariantMembership is a Membership between a variation point Definition or Usage and a Usage that
    /// represents a variant in the context of that variation. The membershipOwningNamespace for the
    /// VariantMembership must be either a Definition or a Usage with isVariation =
    /// true.membershipOwningNamespace.oclIsKindOf(Definition) and   
    /// membershipOwningNamespace.oclAsType(Definition).isVariation
    /// ormembershipOwningNamespace.oclIsKindOf(Usage) and   
    /// membershipOwningNamespace.oclAsType(Usage).isVariation
    /// </summary>
    public partial interface IVariantMembership : IOwningMembership
    {
        /// <summary>
        /// Queries the derived property OwnedVariantUsage
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Usage QueryOwnedVariantUsage();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
