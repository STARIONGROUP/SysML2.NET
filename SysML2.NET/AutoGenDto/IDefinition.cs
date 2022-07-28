// -------------------------------------------------------------------------------------------------
// <copyright file="IDefinition.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A Definition is a Classifier of Usages. The actual kinds of Definitions that may appear in a model
    /// are given by the subclasses of Definition (possibly as extended with user-defined
    /// SemanticMetadata).Normally, a Definition has owned Usages that model features of the thing being
    /// defined. A Definition may also have other Definitions nested in it, but this has no semantic
    /// significance, other than the nested scoping resulting from the Definition being considered as a
    /// Namespace for any nested Definitions.However, if a Definition has isVariation = true, then it
    /// represents a variation point Definition. In this case, all of its members must be variant Usages,
    /// related to the Definition by VariantMembership Relationships. Rather than being features of the
    /// Definition, variant Usages model different concrete alternatives that can be chosen to fill in for
    /// an abstract Usage of the variation point Definition.isVariation implies variantMembership =
    /// ownedMembershipvariant = variantMembership.ownedVariantUsagevariantMembership =
    /// ownedMembership->selectByKind(VariantMembership)not isVariation implies variantMembership->isEmpty()
    /// </summary>
    public interface IDefinition : IClassifier
    {
        /// <summary>
        /// Whether this Definition is for a variation point or not. If true, then all the memberships of the
        /// Definition must be VariantMemberships.
        /// </summary>
        bool IsVariation { get; set; }

    }
}
