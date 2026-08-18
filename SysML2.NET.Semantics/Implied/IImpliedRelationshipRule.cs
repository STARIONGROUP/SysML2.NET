// -------------------------------------------------------------------------------------------------
// <copyright file="IImpliedRelationshipRule.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Computes the implied Relationships of a single semantic constraint that the generated table cannot
    /// express.
    /// </summary>
    /// <remarks>
    /// The generated table only covers constraints whose OCL is a specializesFromLibrary call. Constraints
    /// that relate two elements of the USER model — every Redefinition constraint, and the variation
    /// Specialization constraints among others — are hand-coded as rules and registered explicitly. A
    /// registered rule removes its constraint from the provider's not-covered manifest.
    /// </remarks>
    public interface IImpliedRelationshipRule
    {
        /// <summary>
        /// Gets the name of the semantic constraint this rule implements, for example
        /// checkUsageVariationUsageSpecialization.
        /// </summary>
        string ConstraintName { get; }

        /// <summary>
        /// Computes the implied Relationships the constraint requires of the supplied Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The detached implied Relationships; empty when the constraint does not apply.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        IReadOnlyList<IRelationship> Apply(IElement element);
    }
}
