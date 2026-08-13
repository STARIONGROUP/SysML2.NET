// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedRuleGuardRegistry.cs" company="Starion Group S.A.">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Resolves guards by constraint name from an explicitly supplied set.
    /// </summary>
    /// <remarks>
    /// Guards are registered explicitly rather than discovered by assembly scanning, so the assembly stays
    /// trimmable and the registered set is visible in source.
    /// </remarks>
    public class ImpliedRuleGuardRegistry : IImpliedRuleGuardRegistry
    {
        /// <summary>
        /// The registered guards, keyed by the constraint each decides.
        /// </summary>
        private readonly Dictionary<string, IImpliedRuleGuard> guards;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImpliedRuleGuardRegistry" /> class.
        /// </summary>
        /// <param name="guards">The guards to register.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="guards" /> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when two guards declare the same constraint name.</exception>
        public ImpliedRuleGuardRegistry(IEnumerable<IImpliedRuleGuard> guards)
        {
            if (guards == null)
            {
                throw new ArgumentNullException(nameof(guards));
            }

            var materialised = guards.ToList();

            var duplicate = materialised
                .GroupBy(guard => guard.ConstraintName, StringComparer.Ordinal)
                .FirstOrDefault(group => group.Count() > 1);

            if (duplicate != null)
            {
                throw new ArgumentException($"More than one IImpliedRuleGuard is registered for the constraint '{duplicate.Key}'.", nameof(guards));
            }

            this.guards = materialised.ToDictionary(guard => guard.ConstraintName, StringComparer.Ordinal);
        }

        /// <summary>
        /// Returns the guard registered for the named constraint.
        /// </summary>
        /// <param name="constraintName">The constraint name to resolve a guard for.</param>
        /// <returns>The registered guard.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="constraintName" /> is null.</exception>
        /// <exception cref="MissingImpliedRuleGuardException">Thrown when no guard is registered for the constraint.</exception>
        public IImpliedRuleGuard GetGuard(string constraintName)
        {
            if (constraintName == null)
            {
                throw new ArgumentNullException(nameof(constraintName));
            }

            return this.guards.TryGetValue(constraintName, out var guard)
                ? guard
                : throw new MissingImpliedRuleGuardException(constraintName, "unknown");
        }

        /// <summary>
        /// Asserts whether a guard is registered for the named constraint.
        /// </summary>
        /// <param name="constraintName">The constraint name to test.</param>
        /// <returns>True when a guard is registered.</returns>
        public bool HasGuard(string constraintName) => constraintName != null && this.guards.ContainsKey(constraintName);
    }
}
