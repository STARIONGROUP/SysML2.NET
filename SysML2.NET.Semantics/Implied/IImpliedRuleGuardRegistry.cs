// -------------------------------------------------------------------------------------------------
// <copyright file="IImpliedRuleGuardRegistry.cs" company="Starion Group S.A.">
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
