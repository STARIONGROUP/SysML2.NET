// ------------------------------------------------------------------------------------------------
// <copyright file="IImpliedRuleGuardRegistry.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied
{
    /// <summary>
    /// Resolves the <see cref="IImpliedRuleGuard" /> registered for a conditional semantic constraint.
    /// </summary>
    public interface IImpliedRuleGuardRegistry
    {
        /// <summary>
        /// Returns the guard registered for the named constraint.
        /// </summary>
        /// <param name="constraintName">The constraint name to resolve a guard for.</param>
        /// <returns>The registered guard.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="constraintName" /> is null.</exception>
        /// <exception cref="MissingImpliedRuleGuardException">Thrown when no guard is registered for the constraint.</exception>
        IImpliedRuleGuard GetGuard(string constraintName);

        /// <summary>
        /// Asserts whether a guard is registered for the named constraint.
        /// </summary>
        /// <param name="constraintName">The constraint name to test.</param>
        /// <returns>True when a guard is registered.</returns>
        bool HasGuard(string constraintName);
    }
}
