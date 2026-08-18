// -------------------------------------------------------------------------------------------------
// <copyright file="IImpliedRelationshipProvider.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied
{
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Computes the implied Relationships that KerML 8.4.2 semantic constraints require of a model,
    /// without adding them to the model.
    /// </summary>
    /// <remarks>
    /// Element.isImpliedIncluded is all-or-nothing: an Element whose ownedRelationship contains an implied
    /// Relationship must declare isImpliedIncluded, and while <see cref="NotCoveredConstraints" /> is
    /// non-empty no complete closure can be produced. Implementations therefore never mutate the model and
    /// never set that flag.
    /// </remarks>
    public interface IImpliedRelationshipProvider
    {
        /// <summary>
        /// Gets the names of the semantic constraints this provider cannot yet compute.
        /// </summary>
        IReadOnlyList<string> NotCoveredConstraints { get; }

        /// <summary>
        /// Returns the implied Relationships required of the supplied Element.
        /// </summary>
        /// <param name="element">The Element to compute implied Relationships for.</param>
        /// <returns>The detached implied Relationships; empty when none are required.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        IReadOnlyList<IRelationship> GetImpliedRelationships(IElement element);

        /// <summary>
        /// Returns the implied Specializations required of the supplied Type, after 8.4.2 redundancy reduction.
        /// </summary>
        /// <param name="type">The Type to compute implied Specializations for.</param>
        /// <returns>The detached implied Specializations; empty when none are required.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="type" /> is null.</exception>
        IReadOnlyList<ISpecialization> GetImpliedSpecializations(IType type);

        /// <summary>
        /// Returns the implied Redefinitions required of the supplied Feature.
        /// </summary>
        /// <param name="feature">The Feature to compute implied Redefinitions for.</param>
        /// <returns>The detached implied Redefinitions; empty when none are required.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="feature" /> is null.</exception>
        IReadOnlyList<IRedefinition> GetImpliedRedefinitions(IFeature feature);

        /// <summary>
        /// Asserts whether the named semantic constraint is computed by this provider.
        /// </summary>
        /// <param name="constraintName">The constraint name, for example checkPortUsageSpecialization.</param>
        /// <returns>True when the constraint is computed, false when it is listed as not covered.</returns>
        bool IsConstraintCovered(string constraintName);
    }
}
