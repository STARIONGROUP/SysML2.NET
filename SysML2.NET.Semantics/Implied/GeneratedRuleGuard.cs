// -------------------------------------------------------------------------------------------------
// <copyright file="GeneratedRuleGuard.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Adapts a predicate translated from a constraint's guard OCL to the <see cref="IImpliedRuleGuard" />
    /// contract.
    /// </summary>
    /// <remarks>
    /// This exists so the mechanically translatable guards are emitted as data — one line each — rather than
    /// as a class file each, while still reaching the provider through the same interface as a hand-written
    /// guard.
    /// </remarks>
    public class GeneratedRuleGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// The translated guard expression.
        /// </summary>
        private readonly Func<IElement, bool> predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratedRuleGuard" /> class.
        /// </summary>
        /// <param name="constraintName">The constraint the guard decides.</param>
        /// <param name="predicate">The translated guard expression.</param>
        /// <exception cref="ArgumentNullException">Thrown when either argument is null.</exception>
        public GeneratedRuleGuard(string constraintName, Func<IElement, bool> predicate)
        {
            this.ConstraintName = constraintName ?? throw new ArgumentNullException(nameof(constraintName));
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName { get; }

        /// <summary>
        /// Asserts whether the constraint applies to the supplied Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when the constraint applies.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public bool Applies(IElement element)
        {
            return element == null
                ? throw new ArgumentNullException(nameof(element))
                : this.predicate(element);
        }
    }
}
