// ------------------------------------------------------------------------------------------------
// <copyright file="IImpliedSpecializationReducer.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied
{
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Core.Types;

    /// <summary>
    /// Drops the implied Specializations that KerML 8.4.2 considers redundant for a Type.
    /// </summary>
    /// <remarks>
    /// Two rules apply: an implied Specialization is dropped when the Type already has an ownedSpecialization
    /// with the same general Type, or when any owned or implied Specialization has a general Type that is a
    /// strict subtype of it; and only one of several implied Specializations sharing a general Type is kept.
    /// Neither rule applies to Redefinitions, whose semantics go beyond basic Specialization.
    /// </remarks>
    public interface IImpliedSpecializationReducer
    {
        /// <summary>
        /// Reduces the candidate implied Specializations of a Type to the non-redundant set.
        /// </summary>
        /// <param name="type">The Type the candidates were computed for.</param>
        /// <param name="candidates">The implied Specializations to reduce.</param>
        /// <returns>The retained Specializations, in the order the candidates were supplied.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when either argument is null.</exception>
        IReadOnlyList<ISpecialization> Reduce(IType type, IReadOnlyList<ISpecialization> candidates);
    }
}
