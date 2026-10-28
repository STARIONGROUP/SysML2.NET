// ------------------------------------------------------------------------------------------------
// <copyright file="IImpliedRelationshipRule.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
