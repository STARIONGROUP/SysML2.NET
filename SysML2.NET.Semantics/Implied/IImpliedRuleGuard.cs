// -------------------------------------------------------------------------------------------------
// <copyright file="IImpliedRuleGuard.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Decides whether a conditional semantic constraint applies to a given Element.
    /// </summary>
    /// <remarks>
    /// The generated rule table flags a row as requiring a guard when its OCL is a conditional
    /// specializesFromLibrary call. Such a row must never be applied unconditionally, so a missing guard is
    /// an error rather than an implicit yes.
    /// </remarks>
    public interface IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides, for example
        /// checkPortUsageSubportSpecialization.
        /// </summary>
        string ConstraintName { get; }

        /// <summary>
        /// Asserts whether the constraint applies to the supplied Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when the constraint applies and its implied Relationship is required.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        bool Applies(IElement element);
    }
}
