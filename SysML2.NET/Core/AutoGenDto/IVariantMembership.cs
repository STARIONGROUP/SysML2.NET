// -------------------------------------------------------------------------------------------------
// <copyright file="IVariantMembership.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Core.DTO.Systems.DefinitionAndUsage
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A VariantMembership is a Membership between a variation point Definition or Usage and a Usage that
    /// represents a variant in the context of that variation. The membershipOwningNamespace for the
    /// VariantMembership must be either a Definition or a Usage with isVariation = true.
    /// </summary>
    [Class(xmiId: "_19_0_2_59601fc_1590331535985_437424_487", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IVariantMembership : IOwningMembership
    {
        /// <summary>
        /// The Usage that represents a variant in the context of the owningVariationDefinition or
        /// owningVariationUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1590978683452_645414_775", aggregation: AggregationKind.Composite, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674965_501750_43196")]
        Guid OwnedVariantUsage { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
